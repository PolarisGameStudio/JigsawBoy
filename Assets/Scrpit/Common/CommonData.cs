using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData {

    //是否能拖拽物体
    public static bool IsDargMove = false;
    //是否能移动摄像头
    public static bool IsMoveCamera = false;
    //选择的拼图信息
    public static PuzzlesInfoBean SelectPuzzlesInfo;

    //UI文本信息
    static Dictionary<long, UITextBean> UITextMap;

    static CommonData()
    {
        UITextMap = new Dictionary<long, UITextBean>();

        UITextMap= UITextManager.LoadAllUIText();
    }
}
