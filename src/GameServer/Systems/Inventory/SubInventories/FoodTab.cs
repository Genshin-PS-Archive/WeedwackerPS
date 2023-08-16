﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Inventory
{
    internal class FoodTab : MaterialsTab
    {
        [BsonIgnore] public new const int InventoryLimit = 2000;
        private static readonly string mongoPathToItems = $"{nameof(InventoryManager.SubInventories)}.{ItemType.ITEM_MATERIAL}.{nameof(FoodTab)}.{nameof(Items)}";
        public FoodTab(Player.Player owner, InventoryManager inventory) : base(owner, inventory) { }

        public override async Task<GameItem?> AddItemAsync(uint itemId, uint count = 1, uint level = 1, uint refinement = 0)
        {
            if (Items.TryGetValue(itemId, out GameItem? material))
            {
                if ((material as MaterialItem).ItemData.stackLimit >= material.Count + count)
                {
                    material.Count += count;

                    // Update Database
                    FilterDefinition<InventoryManager>? filter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
                    UpdateDefinition<InventoryManager>? update = Builders<InventoryManager>.Update.Set($"{mongoPathToItems}.{itemId}.{nameof(GameItem.Count)}", material.Count);
                    await DatabaseManager.UpdateInventoryAsync(filter, update);

                    //TODO update codex
                    return material;
                }
                else return null;
            }
            else
            {
                MaterialItem? newMaterial = new(Owner.GetNextGameGuid(), itemId, count);
                Items.Add(itemId, newMaterial);

                // Update Database
                FilterDefinition<InventoryManager>? filter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
                UpdateDefinition<InventoryManager>? update = Builders<InventoryManager>.Update.Set($"{mongoPathToItems}.{itemId}", newMaterial);
                await DatabaseManager.UpdateInventoryAsync(filter, update);

                //TODO update codex
                return newMaterial;
            }
        }

        internal override async Task<bool> RemoveItemAsync(GameItem item, uint count = 1)
        {
            if (Items.TryGetValue(item.ItemId, out GameItem? material))
            {
                if (material.Count - count >= 1)
                {
                    material.Count -= count;

                    // Update Database
                    FilterDefinition<InventoryManager>? filter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
                    UpdateDefinition<InventoryManager>? update = Builders<InventoryManager>.Update.Set($"{mongoPathToItems}.{material.ItemId}.{nameof(GameItem.Count)}", material.Count);
                    await DatabaseManager.UpdateInventoryAsync(filter, update);

                    await Owner.SendPacketAsync(new PacketStoreItemChangeNotify(material));
                    return true;
                }
                else if (material.Count - count == 0)
                {
                    // Update Database
                    FilterDefinition<InventoryManager>? filter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
                    UpdateDefinition<InventoryManager>? update = Builders<InventoryManager>.Update.Unset($"{mongoPathToItems}.{material.ItemId}");
                    await DatabaseManager.UpdateInventoryAsync(filter, update);

                    Items.Remove(material.ItemId);
                    material.Count = 0;
                    Inventory.GuidMap.Remove(material.Guid);
                    await Owner.SendPacketAsync(new PacketStoreItemDelNotify(material));
                    return true;
                }
                else
                {
                    Logger.WriteErrorLine("ItemId: " + item.ItemId + ". Tried to remove " + count + " have " + item.Count);
                    return false;
                }
            }
            else
            {
                Logger.WriteErrorLine("Tried to remove inexistent item");
                return false;
            }
        }
    }
}
