using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSuperImage : MonoBehaviour {

    public int size = 1;

	void Update () {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ScreenCapture.CaptureScreenshot("ScreenShot.png", size);
        }
    }
}
