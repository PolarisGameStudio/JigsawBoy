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
}