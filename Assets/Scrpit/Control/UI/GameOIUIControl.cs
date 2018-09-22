using UnityEngine;
using UnityEngine.UI;

public class GameOIUIControl : BaseUIControl
{
    public Button gameCancelBT;
    public Text mTVTitle;
    public Text mTVMouseSelectContent;
    public Text mTVMouseMoveContent;
    public Text mTVMouseZoomContent;
    public Text mTVKeyMoveContent;
    public Text mTVPauseContent;
    public Text mTVPuzzlesInfoContent;
    public Text mTVPuzzlesOIContent;
    public Text mTVKeyRotateContent;


    private new void Awake()
    {
        base.Awake();

        gameCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameCancelBT");
        gameCancelBT.onClick.AddListener(cancelUI);

        mTVTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOITitle");
        mTVMouseSelectContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIMouseSelectText");
        mTVMouseMoveContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIMouseMoveText");
        mTVMouseZoomContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIMouseZoomText");
        mTVKeyMoveContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIKeyMoveText");
        mTVPauseContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIPauseText");
        mTVPuzzlesInfoContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIPuzzlesInfoText");
        mTVPuzzlesOIContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIOperatingInstructionsText");
        mTVKeyRotateContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "GameOIKeyRotateText"); 

        loadUIData();
    }

    /// <summary>
    /// 关闭当前页面
    /// </summary>
    public void cancelUI()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public override void loadUIData()
    {
        if (mTVTitle != null)
            mTVTitle.text = CommonData.getText(60);
        if (mTVMouseSelectContent != null)
            mTVMouseSelectContent.text = CommonData.getText(61);
        if (mTVMouseMoveContent != null)
            mTVMouseMoveContent.text = CommonData.getText(62);
        if (mTVMouseZoomContent != null)
            mTVMouseZoomContent.text = CommonData.getText(63);
        if (mTVKeyMoveContent != null)
            mTVKeyMoveContent.text = CommonData.getText(64);
        if (mTVPauseContent != null)
            mTVPauseContent.text = CommonData.getText(65);
        if (mTVPuzzlesInfoContent != null)
            mTVPuzzlesInfoContent.text = CommonData.getText(66);
        if (mTVPuzzlesOIContent != null)
            mTVPuzzlesOIContent.text = CommonData.getText(67);
        if (mTVKeyRotateContent != null)
            mTVKeyRotateContent.text = CommonData.getText(86);
    }

    /// <summary>
    /// 打开操作界面UI
    /// </summary>
    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    /// <summary>
    /// 刷新操作界面UI
    /// </summary>
    public override void refreshUI()
    {
        throw new System.NotImplementedException();
    }
}