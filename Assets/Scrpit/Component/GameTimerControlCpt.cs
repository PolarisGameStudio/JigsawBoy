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
    //分针
    private Image timeMinuteHand;
    private Animator timeMinuteAnimaotr;
    //秒针
    private Image timeSecondHand;
    private Animator timeSecondAnimaotr;

    //游戏进行时间
    private TimeSpan nowTimeSpan;

    private void Awake()
    {
        timeText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerText");
        timeMinuteHand = CptUtil.getCptFormParentByName<Transform, Image>(transform, "TimeMinuteHand");
        timeSecondHand = CptUtil.getCptFormParentByName<Transform, Image>(transform, "TimeSecondHand");
        if(timeMinuteHand!=null)
            timeMinuteAnimaotr= timeMinuteHand.GetComponent<Animator>();
        if(timeSecondHand!=null)
            timeSecondAnimaotr = timeSecondHand.GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public void startTimer()
    {
        gameObject.SetActive(true);
        if (timeMinuteAnimaotr != null)
            timeMinuteAnimaotr.SetBool("isStart", true);
        if (timeSecondAnimaotr != null)
            timeSecondAnimaotr.SetBool("isStart", true);
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
        if (timeMinuteAnimaotr != null)
            timeMinuteAnimaotr.SetBool("isStart", false);
        if (timeSecondAnimaotr != null)
            timeSecondAnimaotr.SetBool("isStart", false);
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

