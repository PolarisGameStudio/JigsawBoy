using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuMakerUIControl : BaseUIControl
{
    public Transform mJigsawSelectTiltebar;
    public Button mTitleBarExitBT;

    public Transform mMakerContent;

    private MakerSelect mMakerSelect;
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
        refreshUI();
    }

    public override void refreshUI()
    {
        mMakerSelect.loadMakerData();
    }

    private new void Awake()
    {
        base.Awake();

        //初始化标题栏
        mJigsawSelectTiltebar = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "TitleBar");
        mTitleBarExitBT = CptUtil.getCptFormParentByName<Transform, Button>(mJigsawSelectTiltebar, "ExitBT");

        mMakerContent= CptUtil.getCptFormParentByName<Transform, Transform>(transform, "Content");
        mMakerSelect= mMakerContent.gameObject.AddComponent<MakerSelect>();

        if (mTitleBarExitBT != null)
        {
            mTitleBarExitBT.onClick.AddListener(addExitOnClick);
        }
    }
    /// <summary>
    /// 增加退出按钮监听
    /// </summary>
    public void addExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }
}