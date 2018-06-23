using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class SteamUserInfoResult 
{
    public SteamUserInfoResponse response;


    [Serializable]
    public class SteamUserInfoResponse
    {
        public List<SteamUserItemInfo> players;
    }

    [Serializable]
    public class SteamUserItemInfo
    {
        public string steamid;//steamID
        public int communityvisibilitystate;
        public int profilestate;
        public string personaname;//steam昵称
        public ulong lastlogoff;
        public int commentpermission;
        public string profileurl;
        public string avatar;//steam头像
        public string avatarmedium;//steam中等头像
        public string avatarfull;//steam大头像
        public int personastate;
        public string primaryclanid;
        public ulong timecreated;
        public int personastateflags;
    }

}