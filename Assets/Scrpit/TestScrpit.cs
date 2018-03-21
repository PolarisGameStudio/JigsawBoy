using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;


public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        string target = Application.persistentDataPath;
        string pngPath=   FileUtil.OpenFileDialog();
        FileUtil.CreateDirectory(target + "/custom");
        FileUtil.CopyFile(pngPath, target+"/custom/1.png", true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
