using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuEquipUIControl : BaseUIControl
{
    public Transform jigsawSelectTiltebar;
    public Button titleBarExitBT;
    public Text titleBarJigsawPointTV;
    public Text titleBarTitleName;

    private new void Awake()
    {
        base.Awake();
        //初始化标题栏
        jigsawSelectTiltebar = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "TitleBar");
        titleBarExitBT = CptUtil.getCptFormParentByName<Transform, Button>(jigsawSelectTiltebar, "ExitBT");
        titleBarJigsawPointTV = CptUtil.getCptFormParentByName<Transform, Text>(jigsawSelectTiltebar, "PuzzlesPointText");
        titleBarTitleName = CptUtil.getCptFormParentByName<Transform, Text>(jigsawSelectTiltebar, "TitleName");
        if (titleBarExitBT != null)
        {
            titleBarExitBT.onClick.AddListener(addExitOnClick);
        }

        refreshUI();
    }
    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        throw new System.NotImplementedException();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void refreshUI()
    {
        refreshPuzzlesPoint();
    }

    /// <summary>
    /// 刷新拼图点数
    /// </summary>
    public void refreshPuzzlesPoint()
    {
        titleBarJigsawPointTV.text = "x" + DataStorageManage.getUserInfoDSHandle().getData(0).puzzlesPoint + " PP";
    }

    public void addExitOnClick()
    {
        if(mUIMasterControl!=null)
            mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }
}