using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCpt : BaseMonoBehaviour {

    private void Awake()
    {
        Application.targetFrameRate = 60;//此处限定60帧
    }
}
