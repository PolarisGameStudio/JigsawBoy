using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData {

    //是否能拖拽物体
    public static bool IsDargMove = false;
    //是否能移动摄像头
    public static bool IsMoveCamera = false;
    //选择的拼图信息
    public static PuzzlesGameInfoBean SelectPuzzlesInfo;

    //UI文本信息
    static Dictionary<long, UITextBean> UITextMap;

    static CommonData()
    {
        refreshData();
    }

    public static void refreshData()
    {
        UITextMap = new Dictionary<long, UITextBean>();
        UITextMap = UITextManager.LoadAllUIText();
    }
    /// <summary>
    /// 获取文本信息 By ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string getText(long id) {
        UITextBean textData= UITextMap[id];
        return textData.Content;
    }
}
