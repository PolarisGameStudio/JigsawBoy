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
        GameObject dialogObj = Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/UI/Common/GeneralDialog"));
        GeneralDialog dialog = dialogObj.GetComponent<GeneralDialog>();
        return dialog;
    }

    /// <summary>
    /// 创建文本提示弹窗
    /// </summary>
    /// <returns></returns>
    public static ToastDialog createToastDialog()
    {
        GameObject dialogObj = Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/UI/Common/ToastDialog"));
        ToastDialog dialog = dialogObj.GetComponent<ToastDialog>();
        return dialog;
    }

    /// <summary>
    ///  创建盘排行榜弹窗
    /// </summary>
    /// <param name="dialogType">0有当前成绩 1无当前成绩</param>
    /// <param name="gameInfoBean">拼图相关数据</param>
    /// <returns></returns>
    public static LeaderBoardDialog createLeaderBoradDialog(int dialogType,PuzzlesGameInfoBean gameInfoBean) {
        if (gameInfoBean == null || gameInfoBean.puzzlesInfo == null)
            return null;
        GameObject dialogObj = Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/UI/Common/LeaderBoardDialog"));
        LeaderBoardDialog dialog = dialogObj.GetComponent<LeaderBoardDialog>();
        dialog.setDialogType(dialogType);
        dialog.setPuzzlesInfo(gameInfoBean);
        return dialog;
    }

    /// <summary>
    /// 创建解锁拼图弹窗动画
    /// </summary>
    /// <param name="puzzlesName"></param>
    /// <param name="puzzlesMarkName"></param>
    /// <param name="puzzlesUrl"></param>
    /// <returns></returns>
    public static PuzzlesUnlockDialog createUnlockPuzzlesDialog(string puzzlesName,string puzzlesMarkName, string puzzlesUrl)
    {
        GameObject dialogObj = Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/UI/Common/PuzzlesUnlockDialog"));  
        PuzzlesUnlockDialog dialog= dialogObj.GetComponent<PuzzlesUnlockDialog>();
        dialog.setPuzzlesName(puzzlesName);
        dialog.setPuzzlesMarkName(puzzlesMarkName);
        dialog.setPuzzlesUrl(puzzlesUrl);
        return dialog;
    }

   public static void createPuzzlesPointAddDialog(int addPuzzlesPoint)
    {

    }
}