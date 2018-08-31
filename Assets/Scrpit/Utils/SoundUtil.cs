using System.Collections.Generic;
using UnityEngine;

public class SoundUtil
{
    /// <summary>
    /// 合成音效集合
    /// </summary>
    public static AudioButtonOnClickEnum[] listMergeClip = new AudioButtonOnClickEnum[]
    {
            AudioButtonOnClickEnum.merge_sound_1,
            AudioButtonOnClickEnum.merge_sound_2,
            AudioButtonOnClickEnum.merge_sound_3,
            AudioButtonOnClickEnum.merge_sound_4
     };

    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="onClickEnum"></param>
    public static void playSoundClip(AudioButtonOnClickEnum onClickEnum)
    {
        if (CommonConfigure.IsOpenSound == EnabledEnum.OFF)
            return;
        AudioSourceControl control = Camera.main.GetComponent<AudioSourceControl>();
        if (control != null)
            control.playSoundClip(onClickEnum);
    }

    /// <summary>
    /// 播放合并音乐片段
    /// </summary>
    /// <param name="onClickEnum"></param>
    public static void playSoundClipForMerge()
    {
        playSoundClip(listMergeClip[DevUtil.getRandomInt(0, 3)]);
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public static void playBGMClip()
    {
        AudioSourceControl control = Camera.main.GetComponent<AudioSourceControl>();
        if (control != null)
            control.playBGMClip(0);
    }

    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public static void stopBGMClip()
    {
        AudioSourceControl control = Camera.main.GetComponent<AudioSourceControl>();
        if (control != null)
            control.stopBGMClip();
    }
}