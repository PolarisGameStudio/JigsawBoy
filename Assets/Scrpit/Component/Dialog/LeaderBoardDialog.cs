using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class LeaderBoardDialog : BaseMonoBehaviour
{
    private int mDialogType;
    private string mCurrentScoreTitleStr;
    private string mCurrentScoreContentStr;
    private string mRankTitleStr;
    private string mRankContentStr;
    private string mBestScoreTitleStr;
    private string mBestScoreContentStr;
    private string mCancelStr;
    private string mSubmitStr;

    private Text mCurrentScoreTitle;
    private Text mCurrentScoreContent;
    private Text mRankTitle;
    private Text mRankContent;
    private Text mBestScoreTitle;
    private Text mBestScoreContent;

    private Text mCancelText;
    private Button mCancelBT;
    private Text mSubmitText;
    private Button mSubmitBT;

    private Transform mCurrentScore;
    public LeaderBoardDialog()
    {
        mDialogType = 0;

        mCurrentScoreTitleStr = CommonData.getText(17);
        mCurrentScoreContentStr = "00:00:00";

        mRankTitleStr = CommonData.getText(18);
        mRankContentStr = "0";

        mBestScoreTitleStr = CommonData.getText(19);
        mBestScoreContentStr = "00:00:00";

    }

    private void Start()
    {
        mCurrentScore = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "CurrentScore");

        mCurrentScoreTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "CurrentScoreTitle");
        mCurrentScoreContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "CurrentScoreContent");
        mRankTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "RankTitle");
        mRankContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "RankContent");
        mBestScoreTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "BestScoreTitle");
        mBestScoreContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "BestScoreContent");

        mCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "CancelBT");
        mCancelText = CptUtil.getCptFormParentByName<Button, Text>(mCancelBT, "Text");

        mSubmitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "SubmitBT");
        mSubmitText = CptUtil.getCptFormParentByName<Button, Text>(mSubmitBT, "Text");

        initData();
    }

    private void initData()
    {
        if (mDialogType == 1)
        {
            mCancelStr = CommonData.getText(21);
            mSubmitStr = CommonData.getText(23);
            if (mCurrentScore != null)
                mCurrentScore.gameObject.SetActive(false);
        }
        else
        {
            mCancelStr = CommonData.getText(20);
            mSubmitStr = CommonData.getText(22);
            if (mCurrentScore != null)
                mCurrentScore.gameObject.SetActive(true);
        }
        if (mCurrentScoreTitle != null)
            mCurrentScoreTitle.text = mCurrentScoreTitleStr;
        if (mCurrentScoreContent != null)
            mCurrentScoreContent.text = mCurrentScoreContentStr;
        if (mRankTitle != null)
            mRankTitle.text = mRankTitleStr;
        if (mRankContent != null)
            mRankContent.text = mRankContentStr;
        if (mBestScoreTitle != null)
            mBestScoreTitle.text = mBestScoreTitleStr;
        if (mBestScoreContent != null)
            mBestScoreContent.text = mBestScoreContentStr;
        if (mCancelText != null)
            mCancelText.text = mCancelStr;
        if (mSubmitText != null)
            mSubmitText.text = mSubmitStr;
    }

    /// <summary>
    /// 设置弹窗类型
    /// </summary>
    /// <param name="dialogType"></param>
    /// <returns></returns>
    public LeaderBoardDialog setDialogType(int dialogType)
    {
        mDialogType = dialogType;
        initData();
        return this;
    }
}