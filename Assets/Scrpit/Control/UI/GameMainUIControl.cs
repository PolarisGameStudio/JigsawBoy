using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameMainUIControl : BaseUIControl
{

    public Transform gameTimerTF;
    public GameTimerControlCpt gameTimerControlCpt;
    private new void Awake()
    {
        base.Awake();
        gameTimerTF = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "GameTimer");
        if (gameTimerTF != null)
        {
            gameTimerControlCpt = gameTimerTF.gameObject.AddComponent<GameTimerControlCpt>();
        }
    }

    public void startTimer()
    {
        if (gameTimerControlCpt != null)
            gameTimerControlCpt.startTimer();
    }
    public void endTimer()
    {
        if (gameTimerControlCpt != null)
            gameTimerControlCpt.endTimer();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {

    }



}

