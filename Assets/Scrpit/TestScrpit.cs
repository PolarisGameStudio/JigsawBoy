using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        List<JigsawUnlockStateBean> listDataTemp = new List<JigsawUnlockStateBean>();
        JigsawUnlockStateBean jigsaw = new JigsawUnlockStateBean();
        jigsaw.puzzleId=2;
        listDataTemp.Add(jigsaw);
        DataStorageManage.getJigsawUnlockStateHandle().saveAllData(listDataTemp);
        List<JigsawUnlockStateBean> listData= DataStorageManage.getJigsawUnlockStateHandle().getAllData();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
