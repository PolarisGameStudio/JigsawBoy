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
    //游戏进行时间
    private TimeSpan nowTimeSpan;
    void Start()
    {
        timeText = CptUtil.getCptFormParentByName<Transform, Text>(transform,"GameTimerText");
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

    /// <summary>
    /// 获取时间
    /// </summary>
    public TimeBean getGameTimer()
    {
        if (nowTimeSpan == null)
            return null;
        TimeBean timeBean = new TimeBean();
        timeBean.days = nowTimeSpan.Days;
        timeBean.hours = nowTimeSpan.Hours;
        timeBean.minutes = nowTimeSpan.Minutes;
        timeBean.seconds = nowTimeSpan.Seconds;
        timeBean.totalSeconds =(int)nowTimeSpan.TotalSeconds;
        return timeBean;
    }

    public IEnumerator Timer()
    {
        while (isStartTimer)
        {
            DateTime nowTime = TimeUtil.getNow();
            nowTimeSpan = TimeUtil.getTimeDifference(startTime, nowTime);
            if (timeText != null)
            {
                string gameTime = ""
                    + nowTimeSpan.Days + "天"
                    + nowTimeSpan.Hours + "小时"
                    + nowTimeSpan.Minutes + "分"
                    + nowTimeSpan.Seconds + "秒";
                timeText.text = gameTime;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}

