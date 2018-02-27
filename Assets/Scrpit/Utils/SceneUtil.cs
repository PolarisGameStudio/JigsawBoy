using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil {

    public static void jumpGameScene()
    {
        sceneChange("GameScene");
    }
    public static void jumpMainScene()
    {
        sceneChange("MenuScene");
    }
    public static void sceneChange(string scenenName)
    {
        SceneManager.LoadScene(scenenName);
    }


}
