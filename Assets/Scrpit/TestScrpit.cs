using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;


public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Texture2D texture2D = ResourcesManager.loadData<Texture2D>("PuzzlesPic/Painting/Annunciation");
        JigsawBean jigsawBean=  CreateJigsawDataUtils.createJigsaw(JigsawStyleEnum.Normal,3f,3f, texture2D);
        
        GameObject jigsawObj= JigsawObjBuilder.buildJigsawGameObj(jigsawBean, texture2D);
        jigsawObj.transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
