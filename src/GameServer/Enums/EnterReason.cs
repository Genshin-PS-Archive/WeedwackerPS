﻿namespace Weedwacker.GameServer.Enums
{
    public enum EnterReason
    {
        None = 0,
        Login = 1,
        DungeonReplay = 11,
        DungeonReviveOnWaypoint = 12,
        DungeonEnter = 13,
        DungeonQuit = 14,
        Gm = 21,
        QuestRollback = 31,
        Revival = 32,
        PersonalScene = 41,
        TransPoint = 42,
        ClientTransmit = 43,
        ForceDragBack = 44,
        TeamKick = 51,
        TeamJoin = 52,
        TeamBack = 53,
        Muip = 54,
        DungeonInviteAccept = 55,
        Lua = 56,
        ActivityLoadTerrain = 57,
        HostFromSingleToMp = 58,
        MpPlay = 59,
        AnchorPoint = 60,
        LuaSkipUi = 61,
        ReloadTerrain = 62,
        DraftTransfer = 63,
        EnterHome = 64,
        ExitHome = 65,
        ChangeHomeModule = 66,
        Gallery = 67,
        HomeSceneJump = 68,
        HideAndSeek = 69
    }
}
