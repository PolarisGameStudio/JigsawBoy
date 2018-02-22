using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameBackgroundUtil : MonoBehaviour
{
    //背景缩放大小
    public static float backgroundScale = 5f;
    //背景位置
    public static Vector3 backgroundVector = new Vector3(0, 0, 1);

    public static void createBackground(float picAllW, float picAllH)
    {
        GameObject backgroundGameObj = Instantiate(Resources.Load("Prefab/Game/BlurBackgroundGameObj") as GameObject);
        backgroundGameObj.transform.position = backgroundVector;
        backgroundGameObj.transform.localScale = new Vector3(picAllW* backgroundScale,picAllH* backgroundScale,1);
        backgroundGameObj.name = "GameBlurBackground";


    }
}
