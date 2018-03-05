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
        playBGM();
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

    public void playBGM()
    {
        AudioClip audio= ResourcesManager.loadData<AudioClip>("Sound/BGM/Op9_No2");
        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }
}
