using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil {

    public static void jumpGameScene()
    {
        sceneChangeAsync("GameScene");
    }
    public static void jumpMainScene()
    {
        sceneChangeAsync("MenuScene");
    }
    public static void sceneChange(string scenenName)
    {
        SceneManager.LoadScene(scenenName);
    }

    public static void sceneChangeAsync(string sceneName)
    {
        SceneChangeData.NextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }


}
