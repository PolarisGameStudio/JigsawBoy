using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;
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

    private Text mTimeSeoundText;
    private Text mTimeMinuteText;
    private Text mTimeHoursText;
    private Text mTimeDayText;

    private Animator mMinuteAnimaotr;
    private Animator mHourAnimaotr;
    private Animator mDayAnimaotr;

    private void Awake()
    {
        timeText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerText");
        timeMinuteHand = CptUtil.getCptFormParentByName<Transform, Image>(transform, "TimeMinuteHand");
        timeSecondHand = CptUtil.getCptFormParentByName<Transform, Image>(transform, "TimeSecondHand");

        mTimeSeoundText= CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerSeoundText");
        mTimeMinuteText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerMinuteText");
        mTimeHoursText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerHoursText");
        mTimeDayText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameTimerDayText");

        mMinuteAnimaotr = mTimeMinuteText.GetComponent<Animator>();
        mHourAnimaotr = mTimeHoursText.GetComponent<Animator>();
        mDayAnimaotr = mTimeDayText.GetComponent<Animator>();

        if (timeMinuteHand!=null)
            timeMinuteAnimaotr= timeMinuteHand.GetComponent<Animator>();
        if(timeSecondHand!=null)
            timeSecondAnimaotr = timeSecondHand.GetComponent<Animator>();
        gameObject.SetActive(false);


    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public void startTimer(TimeBean timeBean)
    {
        gameObject.SetActive(true);
        if (timeMinuteAnimaotr != null)
            timeMinuteAnimaotr.SetBool("isStart", true);
        if (timeSecondAnimaotr != null)
            timeSecondAnimaotr.SetBool("isStart", true);
        isStartTimer = true;
        startTime = TimeUtil.getNow();
        if (timeBean != null) {
            startTime = startTime.AddDays(-timeBean.days);
            startTime = startTime.AddHours(-timeBean.hours);
            startTime = startTime.AddMinutes(-timeBean.minutes);
            startTime = startTime.AddSeconds(-timeBean.seconds);
        }
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

    private int oldDay;
    private int oldHours;
    private int oldMinutes;
    public IEnumerator Timer()
    {
        while (isStartTimer)
        {
            DateTime nowTime = TimeUtil.getNow();
            nowTimeSpan = TimeUtil.getTimeDifference(startTime, nowTime);
            if (mTimeSeoundText != null) {
                mTimeSeoundText.text = nowTimeSpan.Seconds + "S";//CommonData.getText(56);

                //mTimeSeoundText.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.8f).OnComplete(delegate ()
                //{
                //    mTimeSeoundText.transform.DORewind();
                //});
            }
            if (mTimeMinuteText != null) {
                mTimeMinuteText.text = nowTimeSpan.Minutes + "M";// CommonData.getText(57);
                if(mMinuteAnimaotr!=null&&! nowTimeSpan.Minutes.Equals(oldMinutes))
                mMinuteAnimaotr.SetTrigger("Trigger");
            }
            if (mTimeHoursText != null)
            {
                mTimeHoursText.text = nowTimeSpan.Hours + "H";// CommonData.getText(58);
                if (mHourAnimaotr != null && !nowTimeSpan.Hours.Equals(oldHours))
                    mHourAnimaotr.SetTrigger("Trigger");
            }
            if (mTimeDayText != null)
            {
                mTimeDayText.text = nowTimeSpan.Days + "D";// CommonData.getText(59);
                if (mDayAnimaotr != null && !nowTimeSpan.Days.Equals(oldDay))
                    mDayAnimaotr.SetTrigger("Trigger");
            }
            oldDay = nowTimeSpan.Days;
            oldHours = nowTimeSpan.Hours;
            oldMinutes = nowTimeSpan.Minutes;
            yield return new WaitForSeconds(1f);
        }
    }
}

