using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour
{

    void Start()
    {
        LeaderBoardUtil.CreateLeaderBoardByInterval(347,358);
    }


}
