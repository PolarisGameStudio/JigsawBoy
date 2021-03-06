﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : BaseMonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip aduioClip;
    //音乐播放方式
    public AudioPlayWayEnum playWay;
    //音乐列表
    public List<BGMInfoBean> listBGMInfo;
    //是否开启音乐播放
    public EnabledEnum isOpenAudio;
    //音乐播放点
    public int musicPlayPosition;

    private void Awake()
    {
        playWay = AudioPlayWayEnum.Random_Play;
        listBGMInfo = BGMInfoManager.LoadAllBGMInfo();
        isOpenAudio = CommonConfigure.IsOpenBGM;
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    void Update()
    {
        if (!isPlayBGMClip() && isOpenAudio.Equals(EnabledEnum.ON) && listBGMInfo != null)
        {
            if (playWay.Equals(AudioPlayWayEnum.Single_Cycle))
            {
                playBeforeBGMClip();
            }
            else if (playWay.Equals(AudioPlayWayEnum.Random_Play))
            {
                int randomPosition = DevUtil.getRandomInt(0, listBGMInfo.Count - 1);
                playBGMClip(listBGMInfo[randomPosition]);
            }

        }
    }

    public void changeVolume(float size)
    {
        if (audioSource != null)
            audioSource.volume = size;
    }
    /// <summary>
    /// 播放按钮点击音效
    /// </summary>
    /// <param name="onClickEnum"></param>
    public void playSoundClip(AudioButtonOnClickEnum onClickEnum)
    {
        string soundPath = "Sound/Clip/";
        switch (onClickEnum)
        {
            case AudioButtonOnClickEnum.btn_sound_1:
                soundPath += "btn_sound_1";
                break;
            case AudioButtonOnClickEnum.btn_sound_2:
                soundPath += "btn_sound_2";
                break;
            case AudioButtonOnClickEnum.btn_sound_3:
                soundPath += "btn_sound_3";
                break;
            case AudioButtonOnClickEnum.merge_sound_1:
                soundPath += "merge_sound_1";
                break;
            case AudioButtonOnClickEnum.merge_sound_2:
                soundPath += "merge_sound_2";
                break;
            case AudioButtonOnClickEnum.merge_sound_3:
                soundPath += "merge_sound_3";
                break;
            case AudioButtonOnClickEnum.merge_sound_4:
                soundPath += "merge_sound_4";
                break;
        }

        AudioClip tempClip = ResourcesManager.LoadData<AudioClip>(soundPath);
        if (tempClip != null) {
            
            AudioSource.PlayClipAtPoint(tempClip, transform.position, CommonConfigure.SoundVolume);
        }
           
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="bgmEnum"></param>
    public void playBGMClip(BGMInfoBean data)
    {
        if (audioSource == null)
            return;
        if (data != null)
        {
            string audioPath = data.FilePath;
            aduioClip = ResourcesManager.LoadData<AudioClip>(audioPath);
            audioSource.clip = aduioClip;
            audioSource.loop = false;
            audioSource.volume = CommonConfigure.BGMVolume;
            audioSource.Play();
        }
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="position"></param>
    public void playBGMClip(int position)
    {
        if (audioSource == null)
            return;
        if (listBGMInfo != null && listBGMInfo.Count > 0 && position <= listBGMInfo.Count - 1)
        {
            BGMInfoBean data = listBGMInfo[position];
            string audioPath = data.FilePath;
            aduioClip = ResourcesManager.LoadData<AudioClip>(audioPath);
            audioSource.clip = aduioClip;
            audioSource.loop = false;
            audioSource.volume = CommonConfigure.BGMVolume;
            audioSource.Play();
        }
    }

    /// <summary>
    /// 播放之前的BGM
    /// </summary>
    public void playBeforeBGMClip()
    {
        if (audioSource == null)
            return;
        if (aduioClip == null)
        {
            List<BGMInfoBean> dataList = BGMInfoManager.LoadBGMInfo(1);
            if (dataList != null && dataList.Count > 0)
            {
                string audioPath = dataList[0].FilePath;
                aduioClip = ResourcesManager.LoadData<AudioClip>(audioPath);
            }
            else
            {
                return;
            }
        }
        audioSource.clip = aduioClip;
        audioSource.volume = CommonConfigure.BGMVolume;
        audioSource.Play();
    }

    /// <summary>
    /// 停止播放
    /// </summary>
    public void stopBGMClip()
    {
        isOpenAudio = EnabledEnum.OFF;
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// 是否在播放音乐
    /// </summary>
    /// <returns></returns>
    public bool isPlayBGMClip()
    {
        if (audioSource != null)
        {
            return audioSource.isPlaying;
        }
        return false;
    }
}
