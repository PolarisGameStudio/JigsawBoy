using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : BaseMonoBehaviour
{
    /// <summary>
    /// 创建普通文本弹窗
    /// </summary>
    /// <returns></returns>
    public static GeneralDialog createGeneralDialog()
    {
        GameObject dialogObj = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/UI/Common/GeneralDialog"));
        GeneralDialog dialog = dialogObj.GetComponent<GeneralDialog>();
        return dialog;
    }

    /// <summary>
    /// 创建文本提示弹窗
    /// </summary>
    /// <returns></returns>
    public static ToastDialog createToastDialog()
    {
        GameObject dialogObj = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/UI/Common/ToastDialog"));
        ToastDialog dialog = dialogObj.GetComponent<ToastDialog>();
        return dialog;
    }

    /// <summary>
    ///  创建盘排行榜弹窗
    /// </summary>
    /// <param name="dialogType">0有当前成绩 1无当前成绩</param>
    /// <returns></returns>
    public static LeaderBoardDialog createLeaderBoradDialog(int dialogType) {
        GameObject dialogObj = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/UI/Common/LeaderBoardDialog"));
        LeaderBoardDialog dialog = dialogObj.GetComponent<LeaderBoardDialog>();
        dialog.setDialogType(dialogType);
        return dialog;
    }
}