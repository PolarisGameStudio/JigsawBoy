using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : MonoBehaviour
{

    public AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        playBGMClip(AudioBGMEnum.Op9_No2);
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
        AudioClip clip = ResourcesManager.loadData<AudioClip>(soundPath);
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="bgmEnum"></param>
    public void playBGMClip(AudioBGMEnum bgmEnum)
    {
        AudioClip audioClip;
        List<BGMInfoBean> bgmDataList = BGMInfoManager.LoadBGMInfo(bgmEnum);
        if (bgmDataList != null && bgmDataList.Count > 0)
        {
            BGMInfoBean item = bgmDataList[0];
            string audioPath = item.FilePath;
            audioClip = ResourcesManager.loadData<AudioClip>(audioPath);
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
