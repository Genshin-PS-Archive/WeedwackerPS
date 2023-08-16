using MongoDB.Bson.Serialization.Attributes;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.Shop
{
    internal class ShopGoodInfo
    {
        [BsonElement] public uint GoodsId { get; private set; }
        [BsonIgnore] public ShopGoodsData Data => GameData.ShopGoodsDataMap[GoodsId];
        public int BoughtNum { get; private set; } = 0;

        public ShopGoodInfo(uint goodsId)
        {
            GoodsId = goodsId;
        }

        public ShopGoodInfo(ShopGoodsData data)
        {
            GoodsId = data.goodsId;
        }

        public void ResetBoughtNum()
        {
            BoughtNum = 0;
        }
        public Task OnLoadAsync()
        {
            return Task.CompletedTask;
        }

        public ShopGoods ToProto(int nextRefresh)
        {
            //TODO
            ShopGoods goods = new ShopGoods()
            {
                GoodsId = GoodsId,
                GoodsItem = new ItemParam() { ItemId = Data.itemId, Count = Data.itemCount },
                Scoin = Data.costScoin ?? 0,
                Hcoin = Data.costHcoin ?? 0,
                Mcoin = Data.costMcoin ?? 0,
                BuyLimit = Data.buyLimit ?? 0,
                SecondarySheetId = Data.secondarySheetId ?? 0,
                //TODO
                BeginTime = 0,
                EndTime = 1924992000,
                MinLevel = Data.minPlayerLevel,
                MaxLevel = Data.maxPlayerLevel,

                DisableType = 0, //TODO

            };

            if (Data.costItems != null)
            {
                Data.costItems.AsParallel().ForAll(w => goods.CostItemList.Add(new ItemParam() { ItemId = (uint)w.id, Count = (uint)w.count }));
            }
            if (Data.preconditionParamList != null)
            {
                foreach (string paramList in Data.preconditionParamList.Where(w => !w.Equals("")))
                {
                    int[] ids = (int[])paramList.Split(",").Select(w => int.Parse(w));
                    ids.AsParallel().ForAll(w => goods.PreGoodsIdList.Add((uint)w));
                }
            }
            else if (Data.preconditionParam != null)
                goods.PreGoodsIdList.Add((uint)Data.preconditionParam);

            goods.BoughtNum = (uint)BoughtNum;
            goods.NextRefreshTime = (uint)nextRefresh;

            return goods;
        }
    }
}
