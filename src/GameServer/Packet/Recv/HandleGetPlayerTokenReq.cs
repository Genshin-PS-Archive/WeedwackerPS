using System.Buffers.Binary;
using System.Security.Cryptography;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetPlayerTokenReq)]
	private async Task HandleGetPlayerTokenReq(byte[] header, byte[] payload)
	{
		GetPlayerTokenReq req = GetPlayerTokenReq.Parser.ParseFrom(payload);

		// Authenticate
		if (!await GameServer.VerifyToken(req.AccountUid, req.AccountToken)) return;

		// If the account doesn't have a player for this server, creates a new one
		Player = await DatabaseManager.GetPlayerByAccountUidAsync(req.AccountUid);
		Player.Token = req.AccountToken;

		// Check if player is already online
		if (GameServer.OnlinePlayers.ContainsKey(Player.GameUid))
		{
			// Kill the previous session, and replace it with the new one. Similar to Official behaviour
			GameServer.OnlinePlayers[Player.GameUid].Stop();
			GameServer.OnlinePlayers[Player.GameUid] = this;
			Player = await DatabaseManager.GetPlayerByAccountUidAsync(req.AccountUid);
		}
		else if (GameServer.OnlinePlayers.Count >= GameServer.Configuration.Server.MaxOnlinePlayers)
		{
			Stop();
			return;
		}
		else
			GameServer.OnlinePlayers.Add(Player.GameUid, this);

		// Update the player's session pointer
		Player.Session = this;


		ulong randSeed = (ulong)Random.Shared.NextInt64();
		await SetSecretKey(randSeed);
		State = SessionState.WAITING_FOR_LOGIN;

		// Only Game Version >= 2.7.50 has this
		if (req.KeyId > 0)
		{
			try
			{
				RSA signer = Crypto.CurSigner;

				byte[] clientSeed = signer.Decrypt(Convert.FromBase64String(req.ClientRandKey), RSAEncryptionPadding.Pkcs1);
				byte[] encryptSeed = BitConverter.GetBytes(BinaryPrimitives.ReverseEndianness(randSeed));
				Crypto.Xor(clientSeed, encryptSeed);
				byte[] seedBytes = clientSeed;

				//Kind of a hack, but whatever
				RSA encryptor = Crypto.GetDispatchEncryptionKey((int)req.KeyId);

				byte[] encryptedSeed = encryptor.Encrypt(seedBytes, RSAEncryptionPadding.Pkcs1);
				byte[] seedSign = signer.SignData(seedBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

				await SendPacketAsync(new PacketGetPlayerTokenRsp(Player, randSeed, Convert.ToBase64String(encryptedSeed), Convert.ToBase64String(seedSign), req.AccountToken));
				// Set session state
				UseSecretKey = true;
			}
			catch (Exception ignore)
			{
				// Only UA Patch users will have exception
				byte[] clientBytes = Convert.FromBase64String(req.ClientRandKey);
				byte[] seed = BitConverter.GetBytes(randSeed);
				Crypto.Xor(clientBytes, seed);

				string base64str = Convert.ToBase64String(clientBytes);

				await SendPacketAsync(new PacketGetPlayerTokenRsp(Player, randSeed, base64str, "bm90aGluZyBoZXJl", req.AccountToken));
				// Set session state
				UseSecretKey = true;
			}
		}
		else
		{
			// Send packet
			await SendPacketAsync(new PacketGetPlayerTokenRsp(Player, req.AccountToken));
			// Set session state
			UseSecretKey = true;
		}

		static byte[] RsaDecrypt(RSA key, byte[] data)
		{
			if (data.Length < key.KeySize / 8)
				return key.Decrypt(data, RSAEncryptionPadding.Pkcs1);
			else
			{
				List<byte> decrypted = new();

				foreach (var chunk in data.Chunk(key.KeySize / 8))
				{
					var decryptedChunk = key.Decrypt(chunk, RSAEncryptionPadding.Pkcs1);
					decrypted.AddRange(decryptedChunk);
				}
				return decrypted.ToArray();
			}
		}

		static byte[] RsaEncrypt(RSA key, byte[] data)
		{
			if (data.Length < key.KeySize / 8)
				return key.Encrypt(data, RSAEncryptionPadding.Pkcs1);
			else
			{
				List<byte> encrypted = new();

				foreach (var chunk in data.Chunk(key.KeySize / 8))
				{
					var decryptedChunk = key.Encrypt(chunk, RSAEncryptionPadding.Pkcs1);
					encrypted.AddRange(decryptedChunk);
				}
				return encrypted.ToArray();
			}
		}
	}
}
