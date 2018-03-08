using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : BaseMonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip aduioClip;
    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    /// <summary>
    /// 播放按钮点击音效
    /// </summary>
    /// <param name="onClickEnum"></param>
    public void playOnClickClip(AudioButtonOnClickEnum onClickEnum)
    {
        string soundPath = "Sound/Button/";
        if (onClickEnum.Equals(AudioButtonOnClickEnum.def))
        {
            soundPath += "button_onclick_def";
        }
        AudioClip tempClip = ResourcesManager.loadData<AudioClip>(soundPath);
        if (tempClip != null)
            AudioSource.PlayClipAtPoint(tempClip, transform.position);
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
            aduioClip = ResourcesManager.loadData<AudioClip>(audioPath);
            audioSource.clip = aduioClip;
            audioSource.loop = true;
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
            List<BGMInfoBean> dataList=  BGMInfoManager.LoadBGMInfo(1);
            if (dataList != null && dataList.Count > 0)
            {
                string audioPath = dataList[0].FilePath;
                aduioClip = ResourcesManager.loadData<AudioClip>(audioPath);
            }
            else
            {
                return;
            }

        }
        audioSource.clip = aduioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// 停止播放
    /// </summary>
    public void stopBGMClip()
    {
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
