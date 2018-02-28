using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerControlCpt : BaseMonoBehaviour
{
    //是否开始计时
    private bool isStartTimer;
    //开始计时时间
    private DateTime startTime;
    private Text timeText;
    void Start()
    {
        timeText = GetComponent<Text>();
    }


    void Update()
    {

    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public void startTimer()
    {
        isStartTimer = true;
        startTime = TimeUtil.getNow();
        StartCoroutine(Timer());
    }

    /// <summary>
    /// 结束计时
    /// </summary>
    public void endTimer()
    {
        isStartTimer = false;
    }

    public IEnumerator Timer()
    {
        while (isStartTimer)
        {
            yield return new WaitForSeconds(1f);
            DateTime nowTime = TimeUtil.getNow();
            TimeSpan nowTimeSpan = TimeUtil.getTimeDifference(startTime, nowTime);
            if (timeText != null)
            {
                string gameTime = "时间:"
                    + nowTimeSpan.Days + "天"
                    + nowTimeSpan.Hours + "小时"
                    + nowTimeSpan.Minutes + "分"
                    + nowTimeSpan.Seconds + "秒";
                timeText.text = gameTime;
            }

        }
    }
}

