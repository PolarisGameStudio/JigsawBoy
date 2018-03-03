using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        List<PuzzlesInfoBean> listData = PuzzlesInfoManager.LoadAllPuzzlesDataByType(JigsawResourcesEnum.Painting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
