using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();
        JigsawUnlockStateBean param = new JigsawUnlockStateBean();
        param.puzzleId = 1;
        param.puzzleType = JigsawResourcesEnum.Animal;
        param.unlockState = JigsawUnlockEnum.UnLock;
        DataStorageManage.getJigsawUnlockStateHandle().saveData(param);

        JigsawUnlockStateBean data= DataStorageManage.getJigsawUnlockStateHandle().getData(param);
      long id=  data.puzzleId;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
