using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuEquipUIControl : BaseUIControl
{
    public Transform jigsawSelectTiltebar;
    public Button titleBarExitBT;
    public Text titleBarJigsawPointTV;
    public Text titleBarTitleName;

    public Button mBTPuzzlesShape;
    public Text mTVPuzzlesShape;

    public Button mBTBorderShape;
    public Text mTVBorderShape;

    public Button mBTBorderColor;
    public Text mTVBorderColor;

    public Button mBTBackgroundColor;
    public Text mTVBackgroundColor;

    public Transform mEquipContent;

    public EquipSelect equipSelect;

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

        mBTPuzzlesShape = CptUtil.getCptFormParentByName<Transform, Button>(transform, "PuzzlesShape");
        mTVPuzzlesShape = CptUtil.getCptFormParentByName<Transform, Text>(transform, "PuzzlesShapeTitle");

        mBTBorderShape = CptUtil.getCptFormParentByName<Transform, Button>(transform, "BorderShape");
        mTVBorderShape = CptUtil.getCptFormParentByName<Transform, Text>(transform, "BorderShapeTitle");

        mBTBorderColor = CptUtil.getCptFormParentByName<Transform, Button>(transform, "BorderColor");
        mTVBorderColor = CptUtil.getCptFormParentByName<Transform, Text>(transform, "BorderColorTitle");

        mBTBackgroundColor = CptUtil.getCptFormParentByName<Transform, Button>(transform, "BackgroundColor");
        mTVBackgroundColor = CptUtil.getCptFormParentByName<Transform, Text>(transform, "BackgroundColorTitle");

        mEquipContent = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "EquipContent");

        mBTPuzzlesShape.onClick.AddListener(addPuzzlesShapeOnClick);
        mBTBorderShape.onClick.AddListener(addBorderShapeOnClick);
        mBTBorderColor.onClick.AddListener(addBorderColorOnClick);
        mBTBackgroundColor.onClick.AddListener(addBackgroundOnClick);

        equipSelect = gameObject.AddComponent<EquipSelect>();
        equipSelect.setMenuSelectUIControl(this);
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
        refreshPuzzlesPoint();
    }

    public override void refreshUI()
    {
        refreshPuzzlesPoint();
        if (mTVPuzzlesShape != null)
            mTVPuzzlesShape.text = CommonData.getText(80);
        if (mTVBorderShape != null)
            mTVBorderShape.text = CommonData.getText(81);
        if (mTVBorderColor != null)
            mTVBorderColor.text = CommonData.getText(82);
        if (mTVBackgroundColor != null)
            mTVBackgroundColor.text = CommonData.getText(83);
    }

    /// <summary>
    /// 刷新拼图点数
    /// </summary>
    public void refreshPuzzlesPoint()
    {
        titleBarJigsawPointTV.text = "x" + DataStorageManage.getUserInfoDSHandle().getData(0).puzzlesPoint + " PP";
    }

    /// <summary>
    /// 返回点击事件
    /// </summary>
    public void addExitOnClick()
    {
        if (mUIMasterControl != null)
            mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }

    /// <summary>
    /// 加载拼图形状数据
    /// </summary>
    public void addPuzzlesShapeOnClick()
    {
        cleanItem();
        equipSelect.selectPuzzlesShape(mEquipContent);
    }

    /// <summary>
    /// 加载边框形状数据
    /// </summary>
    public void addBorderShapeOnClick()
    {
        cleanItem();
        equipSelect.selectBorderShape(mEquipContent);
    }

    /// <summary>
    /// 加载边框颜色数据
    /// </summary>
    public void addBorderColorOnClick()
    {
        cleanItem();
        equipSelect.selectBorderColor(mEquipContent);
    }

    /// <summary>
    /// 加载背景颜色数据
    /// </summary>
    public void addBackgroundOnClick()
    {
        cleanItem();
        equipSelect.selectBackground(mEquipContent);
    }

    /// <summary>
    /// 清除原数据
    /// </summary>
    private void cleanItem()
    {
        StopAllCoroutines();
        //删除原数据
        for (int i = 0; i < mEquipContent.childCount; i++)
        {
            Destroy(mEquipContent.GetChild(i).gameObject);
        }
    }
}