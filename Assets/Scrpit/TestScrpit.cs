using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        List<PuzzlesCompleteStateBean> listData = new List<PuzzlesCompleteStateBean>();
        PuzzlesCompleteStateBean data = new PuzzlesCompleteStateBean();
        data.puzzleId = 1;
        data.unlockState = JigsawUnlockEnum.Lock;
        TimeBean timeBean = new TimeBean();
        timeBean.days = 30;
        data.completeTime = timeBean;
        listData.Add(data);
        DataStorageManage.getPuzzlesCompleteDSHandle().saveAllData(listData);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
