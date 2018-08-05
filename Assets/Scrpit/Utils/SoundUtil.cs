using UnityEngine;
using UnityEditor;

public class SoundUtil 
{
    /// <summary>
    /// 播放音乐片段
    /// </summary>
    /// <param name="onClickEnum"></param>
    public static void playSoundClip(AudioButtonOnClickEnum onClickEnum)
    {
        if (CommonConfigure.isOpenSound == EnabledEnum.OFF)
            return;
        AudioSourceControl control= Camera.main.GetComponent<AudioSourceControl>();
        if(control!=null)
        control.playSoundClip(onClickEnum);
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