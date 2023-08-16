﻿using System.Collections.Concurrent;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.GameServer.Systems.Shop;
using Weedwacker.GameServer.Systems.Social;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Database;

internal static class DatabaseManager
{
	private static MongoClient DbClient;
	private static IMongoDatabase Database;
	private static IMongoCollection<Player> Players;
	private static IMongoCollection<AvatarManager> Avatars;
	private static IMongoCollection<InventoryManager> Inventories;
	private static IMongoCollection<ProgressManager> Progress;
	private static IMongoCollection<TeamManager> Teams;
	private static IMongoCollection<ShopManager> Shops;
	private static IMongoCollection<SocialManager> Social;
	private static DatabaseProperties Properties;
	private static readonly ReplaceOptions Replace = new() { IsUpsert = true };
	private static readonly BulkWriteOptions BulkWrite = new() { IsOrdered = true };
	private static System.Timers.Timer? UpdateTimer;

	// Aggregate update operations, and bulkWrite periodically
	private static ConcurrentBag<WriteModel<Player>> PlayerWrites = new();
	private static ConcurrentBag<WriteModel<AvatarManager>> AvatarWrites = new();
	private static ConcurrentBag<WriteModel<InventoryManager>> InventoryWrites = new();
	private static ConcurrentBag<WriteModel<ProgressManager>> ProgressWrites = new();
	private static ConcurrentBag<WriteModel<TeamManager>> TeamWrites = new();
	private static ConcurrentBag<WriteModel<ShopManager>> ShopWrites = new();
	private static ConcurrentBag<WriteModel<SocialManager>> SocialWrites = new();

	public static async Task Initialize()
	{
		BsonSerializer.RegisterSerializer(new EnumSerializer<PropType>(BsonType.String));
		BsonSerializer.RegisterSerializer(new EnumSerializer<FightPropType>(BsonType.String));
		BsonSerializer.RegisterSerializer(new EnumSerializer<ItemType>(BsonType.String));
		BsonSerializer.RegisterSerializer(new EnumSerializer<LifeState>(BsonType.String));
		BsonSerializer.RegisterSerializer(new EnumSerializer<OpenStateType>(BsonType.String));

		/* Why is this not default behaviour??? */
		DictionaryInterfaceImplementerSerializer<Dictionary<long, long>>? LongToLongDictionarySerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new Int64Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<long>());
		BsonSerializer.RegisterSerializer(LongToLongDictionarySerializer);

		DictionaryInterfaceImplementerSerializer<Dictionary<int, int>>? IntToIntDictionarySerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new Int32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<int>());
		BsonSerializer.RegisterSerializer(IntToIntDictionarySerializer);

		DictionaryInterfaceImplementerSerializer<Dictionary<uint, int>>? UIntToIntDictionarySerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new UInt32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<int>());
		BsonSerializer.RegisterSerializer(UIntToIntDictionarySerializer);

		DictionaryInterfaceImplementerSerializer<Dictionary<uint, uint>>? UIntToUIntDictionarySerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new UInt32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<uint>());
		BsonSerializer.RegisterSerializer(UIntToUIntDictionarySerializer);

		DictionaryInterfaceImplementerSerializer<SortedList<int, int>>? IntToIntSortedListSerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new Int32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<int>());
		BsonSerializer.RegisterSerializer(IntToIntSortedListSerializer);

		DictionaryInterfaceImplementerSerializer<SortedList<uint, int>>? UIntToIntSortedListSerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new UInt32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<int>());
		BsonSerializer.RegisterSerializer(UIntToIntSortedListSerializer);

		DictionaryInterfaceImplementerSerializer<SortedList<uint, uint>>? UIntToUIntSortedListSerializer = new(
		dictionaryRepresentation: DictionaryRepresentation.Document,
		keySerializer: new UInt32Serializer(BsonType.String),
		valueSerializer: BsonSerializer.SerializerRegistry.GetSerializer<uint>());
		BsonSerializer.RegisterSerializer(UIntToUIntSortedListSerializer);
		/* Why is this not default behaviour??? */

		DbClient = new MongoClient(GameServer.Configuration.Database.ConnectionUri);
		// Databases and collections are implicitly created
		Database = DbClient.GetDatabase(GameServer.Configuration.Database.Database);
		Players = Database.GetCollection<Player>("players");
		Avatars = Database.GetCollection<AvatarManager>("avatars");
		Inventories = Database.GetCollection<InventoryManager>("inventories");
		Progress = Database.GetCollection<ProgressManager>("progress");
		Teams = Database.GetCollection<TeamManager>("teams");
		Shops = Database.GetCollection<ShopManager>("shops");
		Social = Database.GetCollection<SocialManager>("social");

		if (Database.GetCollection<DatabaseProperties>("dbProperties").Find(w => true).FirstOrDefault() == null)
		{
			Properties = new DatabaseProperties();
			Database.GetCollection<DatabaseProperties>("dbProperties").InsertOne(Properties);
		}
		else
		{
			Properties = Database.GetCollection<DatabaseProperties>("dbProperties").Find(w => true).First();
		}

		Logger.WriteLine("Connected to GameServer database");

		// Create a timer with a two second interval.
		UpdateTimer = new System.Timers.Timer(2000);
		// Hook up the Elapsed event for the timer. 
		UpdateTimer.Elapsed += BulkUpdate;
		UpdateTimer.AutoReset = true;
		UpdateTimer.Enabled = true;
	}

	private static async void BulkUpdate(object? source, ElapsedEventArgs e)
	{
		Task[]? tasks = new Task[] {
			new(async () => {if (PlayerWrites.Any()) { await Players.BulkWriteAsync(PlayerWrites, BulkWrite); PlayerWrites.Clear(); } }),
			new(async () => {if (AvatarWrites.Any()) { await Avatars.BulkWriteAsync(AvatarWrites, BulkWrite); AvatarWrites.Clear(); }}),
			new(async () => {if (InventoryWrites.Any()) { await Inventories.BulkWriteAsync(InventoryWrites, BulkWrite); InventoryWrites.Clear(); }}),
			new(async () => {if (ProgressWrites.Any()) { await Progress.BulkWriteAsync(ProgressWrites, BulkWrite); ProgressWrites.Clear(); }}),
			new(async () => {if (TeamWrites.Any()) { await Teams.BulkWriteAsync(TeamWrites, BulkWrite); TeamWrites.Clear(); }}),
			new(async () => {if (ShopWrites.Any()) { await Shops.BulkWriteAsync(ShopWrites, BulkWrite); ShopWrites.Clear(); }}),
			new(async () => {if (SocialWrites.Any()) { await Social.BulkWriteAsync(SocialWrites, BulkWrite); SocialWrites.Clear(); }}),
		};
		tasks.AsParallel().ForAll(w => w.Start());
		await Task.WhenAll(tasks);
	}

	public static async Task<Player?> CreatePlayerFromAccountUidAsync(string accountUid, string heroName = "", uint gameUid = 0)
	{
		//Make sure there are no name or id collisions
		IAsyncCursor<Player>? queryResult = await Players.FindAsync(w => w.Profile.HeroName == heroName || w.AccountUid == accountUid || w.GameUid == gameUid);
		if (queryResult.ToList().Count > 0)
		{
			return null;
		}

		// Account

		if (gameUid == Properties.NextGameUid || gameUid == 0)
		{
			//Increment the counter
			FilterDefinition<DatabaseProperties>? filter = Builders<DatabaseProperties>.Filter.Eq(x => x.NextGameUid, Properties.NextGameUid); // All the documents with (NextUid = Properties.NextUid)
			UpdateDefinition<DatabaseProperties>? update = Builders<DatabaseProperties>.Update.Inc<uint>(x => x.NextGameUid, 1); // Increment counter by 1
			gameUid = Properties.NextGameUid++;
			await Database.GetCollection<DatabaseProperties>("dbProperties").UpdateOneAsync(filter, update);
		}
		Player player = new(heroName, accountUid, gameUid);
		await Players.InsertOneAsync(player);
		return player;
	}

	public static async Task SavePlayerAsync(Player player)
	{
		// Replaces the player document.
		await Players.ReplaceOneAsync<Player>(w => w.AccountUid == player.AccountUid, player);
	}

	public static Task UpdatePlayerAsync(FilterDefinition<Player> filter, UpdateDefinition<Player> update)
	{
		PlayerWrites.Add(new UpdateOneModel<Player>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	public static async Task<Player?> GetPlayerByAccountUidAsync(string uid)
	{
		IAsyncCursor<Player>? matches = await Players.FindAsync(w => w.AccountUid == uid);
		Player? player = await matches.FirstOrDefaultAsync() ?? await CreatePlayerFromAccountUidAsync(uid);
		//Attach player systems to the player
		player.Avatars = await GetAvatarsByPlayerAsync(player) ?? await SaveAvatarsAsync(new AvatarManager(player)); // Load avatars before inventory, so that we can attach weapons while loading them
		player.Inventory = await GetInventoryByPlayerAsync(player) ?? await SaveInventoryAsync(new InventoryManager(player));
		player.ShopManager = await GetShopsByPlayerAsync(player) ?? await SaveShopsAsync(new ShopManager(player));
		player.ProgressManager = await GetProgressByPlayerAsync(player) ?? await SaveProgressAsync(new ProgressManager(player));
		player.SocialManager = await GetSocialByPlayerAsync(player) ?? await SaveSocialAsync(new SocialManager(player));
		player.TeamManager = await GetTeamsByPlayerAsync(player) ?? await SaveTeamsAsync(new TeamManager(player));
		await player.OnLoadAsync();

		return player;
	}

	// Don't create a player when requested by gameUid
	public static async Task<Player?> GetPlayerByGameUidAsync(uint gameUid)
	{
		Player? player = await Players.Find(w => w.GameUid == gameUid).FirstOrDefaultAsync();
		if (player == null) return null;

		//Attach player systems to the player
		player.Avatars = await GetAvatarsByPlayerAsync(player) ?? await SaveAvatarsAsync(new AvatarManager(player)); // Load avatars before inventory, so that we can attach weapons while loading them
		player.Inventory = await GetInventoryByPlayerAsync(player) ?? await SaveInventoryAsync(new InventoryManager(player));
		player.ShopManager = await GetShopsByPlayerAsync(player) ?? await SaveShopsAsync(new ShopManager(player));
		player.ProgressManager = await GetProgressByPlayerAsync(player) ?? await SaveProgressAsync(new ProgressManager(player));
		player.SocialManager = await GetSocialByPlayerAsync(player) ?? await SaveSocialAsync(new SocialManager(player));
		player.TeamManager = await GetTeamsByPlayerAsync(player) ?? await SaveTeamsAsync(new TeamManager(player));
		await player.OnLoadAsync();

		return player;
	}

	public static async Task<AvatarManager> SaveAvatarsAsync(AvatarManager avatars)
	{
		await Avatars.ReplaceOneAsync<AvatarManager>(w => w.OwnerId == avatars.OwnerId, avatars, Replace);
		return avatars;
	}

	public static Task UpdateAvatarsAsync(FilterDefinition<AvatarManager> filter, UpdateDefinition<AvatarManager> update)
	{
		AvatarWrites.Add(new UpdateOneModel<AvatarManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	private static async Task<AvatarManager?> GetAvatarsByPlayerAsync(Player owner)
	{
		AvatarManager avatars = await Avatars.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (avatars != null) await avatars.OnLoadAsync(owner);
		return avatars;
	}
	public static async Task<InventoryManager> SaveInventoryAsync(InventoryManager inventory)
	{
		await Inventories.ReplaceOneAsync<InventoryManager>(w => w.OwnerId == inventory.OwnerId, inventory, Replace);
		return inventory;
	}
	private static async Task<InventoryManager?> GetInventoryByPlayerAsync(Player owner)
	{
		InventoryManager inventory = await Inventories.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (inventory != null) await inventory.OnLoadAsync(owner);
		return inventory;
	}

	public static Task UpdateInventoryAsync(FilterDefinition<InventoryManager> filter, UpdateDefinition<InventoryManager> update)
	{
		InventoryWrites.Add(new UpdateOneModel<InventoryManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	public static async Task<TeamManager> SaveTeamsAsync(TeamManager teams)
	{
		await Teams.ReplaceOneAsync<TeamManager>(w => w.OwnerId == teams.OwnerId, teams, Replace);
		return teams;
	}

	public static Task UpdateTeamsAsync(FilterDefinition<TeamManager> filter, UpdateDefinition<TeamManager> update)
	{
		TeamWrites.Add(new UpdateOneModel<TeamManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	private static async Task<TeamManager?> GetTeamsByPlayerAsync(Player owner)
	{
		TeamManager teams = await Teams.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (teams != null) await teams.OnLoadAsync(owner);
		return teams;
	}

	public static async Task<ShopManager> SaveShopsAsync(ShopManager shops)
	{
		await Shops.ReplaceOneAsync<ShopManager>(w => w.OwnerId == shops.OwnerId, shops, Replace);
		return shops;
	}

	public static Task UpdateShopsAsync(FilterDefinition<ShopManager> filter, UpdateDefinition<ShopManager> update)
	{
		ShopWrites.Add(new UpdateOneModel<ShopManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	private static async Task<ShopManager?> GetShopsByPlayerAsync(Player owner)
	{
		ShopManager shops = await Shops.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (shops != null) await shops.OnLoadAsync(owner);
		return shops;
	}

	public static async Task<SocialManager> SaveSocialAsync(SocialManager social)
	{
		await Social.ReplaceOneAsync<SocialManager>(w => w.OwnerId == social.OwnerId, social);
		return social;
	}

	public static Task UpdateSocialAsync(FilterDefinition<SocialManager> filter, UpdateDefinition<SocialManager> update)
	{
		SocialWrites.Add(new UpdateOneModel<SocialManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	private static async Task<SocialManager?> GetSocialByPlayerAsync(Player owner)
	{
		SocialManager social = await Social.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (social != null) await social.OnLoadAsync(owner);
		return social;
	}

	public static async Task<ProgressManager> SaveProgressAsync(ProgressManager progress)
	{
		await Progress.ReplaceOneAsync<ProgressManager>(w => w.OwnerId == progress.OwnerId, progress, new ReplaceOptions() { IsUpsert = true });
		return progress;
	}

	public static Task UpdateProgressAsync(FilterDefinition<ProgressManager> filter, UpdateDefinition<ProgressManager> update)
	{
		ProgressWrites.Add(new UpdateOneModel<ProgressManager>(filter, update) { IsUpsert = true });
		return Task.CompletedTask;
	}

	private static async Task<ProgressManager?> GetProgressByPlayerAsync(Player owner)
	{
		ProgressManager progress = await Progress.Find(w => w.OwnerId == owner.GameUid).FirstOrDefaultAsync();
		if (progress != null) await progress.OnLoadAsync(owner);
		return progress;
	}
}
