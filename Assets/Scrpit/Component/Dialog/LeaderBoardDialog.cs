using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class LeaderBoardDialog : BaseMonoBehaviour, LeaderboardFindResultCallBack, LeaderboardEntriesFindResultCallBack
{
    private int mDialogType;//1为普通查询模式  0为带成绩查询模式
    private string mCurrentScoreTitleStr;
    private string mCurrentScoreContentStr;
    private string mRankTitleStr;
    private string mRankContentStr;
    private string mBestScoreTitleStr;
    private int mBestScore;
    private string mBestScoreContentStr;
    private string mCancelStr;
    private string mSubmitStr;

    private string LeaderBoardItemPath = "Prefab/UI/Common/LeaderBoardRankItem";

    private Text mCurrentScoreTitle;
    private Text mCurrentScoreContent;
    private Text mRankTitle;
    private Text mRankContent;
    private Text mBestScoreTitle;
    private Text mBestScoreContent;
    private Text mTrophyName;

    private Text mCancelText;
    private Button mCancelBT;
    private Text mSubmitText;
    private Button mSubmitBT;

    private Transform mCurrentScore;
    private Transform mWorldRank;
    private Transform mTrophy;
    private Transform mLoading;

    private List<LeaderBoardItemData> mListLeaderBoardInfo;
    private LeaderboardHandleImpl mLeaderboardHandle;
    private PuzzlesGameInfoBean mGameInfoBean;

    private CallBack mCallBack;
    private int mUserScore;
    private ulong mLeaderboardId;
    public LeaderBoardDialog()
    {
        mDialogType = 0;
        mUserScore = 0;
        mLeaderboardId = 0;

        mCurrentScoreTitleStr = CommonData.getText(17);
        mCurrentScoreContentStr = getTimeStr(0);

        mRankTitleStr = CommonData.getText(18);
        mRankContentStr = "0";

        mBestScoreTitleStr = CommonData.getText(19);
        mBestScoreContentStr = getTimeStr(0);

        mLeaderboardHandle = new LeaderboardHandleImpl();
        mListLeaderBoardInfo = new List<LeaderBoardItemData>();
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
        mTrophyName = CptUtil.getCptFormParentByName<Transform, Text>(transform, "TrophyName");

        mCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "CancelBT");
        mCancelText = CptUtil.getCptFormParentByName<Button, Text>(mCancelBT, "Text");

        mSubmitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "SubmitBT");
        mSubmitText = CptUtil.getCptFormParentByName<Button, Text>(mSubmitBT, "Text");

        mWorldRank = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "Content");
        mTrophy = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "Trophy");
        mLoading = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "Loading");

        mCancelBT.onClick.AddListener(cancelOnClick);
        mSubmitBT.onClick.AddListener(submitOnClick);
        initData();
        //设置本地最好分数
        PuzzlesCompleteStateBean completeData = mGameInfoBean.completeStateInfo;
        if (completeData != null && completeData.completeTime != null)
        {
            mBestScore = completeData.completeTime.totalSeconds;
            mBestScoreContentStr = getTimeStr(completeData.completeTime.totalSeconds);
            mBestScoreContent.text = mBestScoreContentStr;
        }
        //查询网络数据
        mLeaderboardHandle.findLeaderboard(mGameInfoBean.puzzlesInfo.Id + "_" + mGameInfoBean.puzzlesInfo.Mark_file_name, this);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void initData()
    {
        if (mDialogType == 1)
        {
            mCancelStr = CommonData.getText(20);
            mSubmitStr = CommonData.getText(22);
            if (mCurrentScore != null)
                mCurrentScore.gameObject.SetActive(false);
            if (mTrophy != null)
                mTrophy.gameObject.SetActive(true);
        }
        else
        {
            mCancelStr = CommonData.getText(21);
            mSubmitStr = CommonData.getText(23);
            if (mCurrentScore != null)
                mCurrentScore.gameObject.SetActive(true);
            if (mTrophy != null)
                mTrophy.gameObject.SetActive(false);
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
        if (mTrophyName != null)
            mTrophyName.text = mGameInfoBean.puzzlesInfo.Name;
    }


    /// <summary>
    /// 设置拼图信息
    /// </summary>
    /// <param name="gameInfoBean"></param>
    public void setPuzzlesInfo(PuzzlesGameInfoBean gameInfoBean)
    {
        mGameInfoBean = gameInfoBean;
    }

    /// <summary>
    /// 根据秒获取具体时间
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public string getTimeStr(int score)
    {
        TimeSpan timeSpan = new TimeSpan(0, 0, score);
        return
            timeSpan.Hours + CommonData.getText(24) + " " +
            timeSpan.Minutes + CommonData.getText(25) + " " +
            timeSpan.Seconds + CommonData.getText(26) + " ";
    }

    /// <summary>
    /// 获取排行榜类型
    /// </summary>
    /// <returns></returns>
    public int getDialogType()
    {
        return mDialogType;
    }

    /// <summary>
    /// 获取排行榜ID
    /// </summary>
    /// <returns></returns>
    public ulong getLeaderBoardId()
    {
        return mLeaderboardId;
    }

    /// <summary>
    /// 返回排行榜列表数据
    /// </summary>
    /// <returns></returns>
    public List<LeaderBoardItemData> getLeaderBoardList()
    {
        return mListLeaderBoardInfo;
    }

    /// <summary>
    /// 返回用户分数
    /// </summary>
    /// <returns></returns>
    public int getUserScore()
    {
        return mUserScore;
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

    /// <summary>
    /// 设置用户分数
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public LeaderBoardDialog setUserScore(int score)
    {
        mUserScore = score;
        mCurrentScoreContentStr = getTimeStr(score);
        return this;
    }
    /// <summary>
    /// 设置回调
    /// </summary>
    /// <param name="mCallBack"></param>
    /// <returns></returns>
    public LeaderBoardDialog setCallBack(CallBack mCallBack)
    {
        this.mCallBack = mCallBack;
        return this;
    }

    /// <summary>
    /// 设置提交按钮文字
    /// </summary>
    /// <param name="mSubmitStr"></param>
    /// <returns></returns>
    public LeaderBoardDialog setSubmitButtonStr(string mSubmitStr)
    {
        this.mSubmitStr = mSubmitStr;
        if (mSubmitText != null)
            mSubmitText.text = mSubmitStr;
        return this;
    }

    #region -------- 查询排行榜ID -------- 
    /// <summary>
    /// 查询排行榜成功回调
    /// </summary>
    /// <param name="leaderboardId"></param>
    public void leaderboradFindSuccess(ulong leaderboardId)
    {
        mLeaderboardId = leaderboardId;
        getSelfLeaderBoardEntries(leaderboardId);
    }

    /// <summary>
    /// 查询排行榜失败回调
    /// </summary>
    /// <param name="msg"></param>
    public void leaderboradFindFail(string msg)
    {
        DialogManager
            .createToastDialog()
            .setToastText(msg);
    }
    #endregion



    #region -------- 查询自己的排行榜数据 -------- 
    private void getSelfLeaderBoardEntries(ulong leaderboardId)
    {
        mLeaderboardHandle.findLeaderboardEntriesForUser(leaderboardId, this);
        // MHttpManagerFactory.getSteamManagerPartner().getLeaderboradEntriesForUser(leaderboardId, new SelfLeaderBoradEntriesCallBack(this));
    }

    public void leaderboradEntriesFindResultForSelf(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList)
    {
        checkLeaderboradEntriesResultListForSelf(resultList);
    }

    private void checkLeaderboradEntriesResultListForSelf(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList)
    {
        //如果当前类型为不带成绩查询 则
        if (getDialogType() == 1)
        {
            if (resultList != null && resultList.Count != 0)
            {
                GetLeaderboardEntriesResult.LeaderboardEntries leaderBoardData = resultList[0];
                mBestScoreContent.text = getTimeStr(leaderBoardData.score);
                mRankContent.text = leaderBoardData.rank + "";
            }
            getGlobalLeaderBoardEntries(mLeaderboardId);
        }
        //如果当前类型为带成绩查询
        else
        {

            if (resultList == null || resultList.Count == 0)
            {
                //如果没有个人分数 增更新个人成绩
                updateLeaderBoard(getLeaderBoardId(), getUserScore());
                // getGlobalLeaderBoardEntries(mLeaderboardId);
            }
            else
            {
                GetLeaderboardEntriesResult.LeaderboardEntries leaderBoardData = resultList[0];
                mBestScoreContent.text = getTimeStr(leaderBoardData.score);
                mRankContent.text = leaderBoardData.rank + "";
                if (getUserScore() == 0)
                {
                    getGlobalLeaderBoardEntries(mLeaderboardId);
                    return;
                }
                if (getUserScore() < leaderBoardData.score)
                    updateLeaderBoard(getLeaderBoardId(), getUserScore());
                else
                    getGlobalLeaderBoardEntries(mLeaderboardId);
            }
        }
    }

    public class SelfLeaderBoradEntriesCallBack : HttpResponseHandler<GetLeaderboardEntriesResult>
    {
        private LeaderBoardDialog leaderBoardDialog;
        public SelfLeaderBoradEntriesCallBack(LeaderBoardDialog leaderBoardDialog)
        {
            this.leaderBoardDialog = leaderBoardDialog;
        }
        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="result"></param>
        public override void onSuccess(GetLeaderboardEntriesResult result)
        {
            if (result == null || result.leaderboardEntryInformation == null)
                return;
            List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList = result.leaderboardEntryInformation.leaderboardEntries;
            leaderBoardDialog.checkLeaderboradEntriesResultListForSelf(resultList);
        }

        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="message"></param>
        public override void onError(string message)
        {
            DialogManager
                 .createToastDialog()
                 .setToastText("查询个人成绩失败");
            leaderBoardDialog.getGlobalLeaderBoardEntries(leaderBoardDialog.getLeaderBoardId());
        }
    }
    #endregion



    #region -------- 更新用户分数 --------
    private void updateLeaderBoard(ulong leaderboardId, int score)
    {
        MHttpManagerFactory.getSteamManagerPartner().updateLeaderboardData(leaderboardId, score, new UpdateLeaderBoardCallBack(this));

    }

    public class UpdateLeaderBoardCallBack : HttpResponseHandler<SetLeaderboardScoreResult>
    {
        private LeaderBoardDialog leaderBoardDialog;
        public UpdateLeaderBoardCallBack(LeaderBoardDialog leaderBoardDialog)
        {
            this.leaderBoardDialog = leaderBoardDialog;
        }
        public override void onError(string message)
        {
            DialogManager
             .createToastDialog()
             .setToastText("更新个人成绩失败");
            leaderBoardDialog.getGlobalLeaderBoardEntries(leaderBoardDialog.getLeaderBoardId());
        }

        public override void onSuccess(SetLeaderboardScoreResult result)
        {
            leaderBoardDialog.getSelfLeaderBoardEntries(leaderBoardDialog.getLeaderBoardId());
        }
    }
    #endregion



    #region -------- 查询全球前20的排行榜数据 -------- 
    private void getGlobalLeaderBoardEntries(ulong leaderboardId)
    {
        mLeaderboardHandle.findLeaderboardEntriesForAll(leaderboardId, 1, 20, this);
        // MHttpManagerFactory.getSteamManagerPartner().getLeaderboradEntriesForGlobal(leaderboardId, 1, 20, new GlobalLeaderBoradEntriesCallBack(this));
    }


    public void leaderboradEntriesFindResultForAll(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList)
    {
        checkLeaderboradEntriesResultListForAll(resultList);
    }

    public void checkLeaderboradEntriesResultListForAll(List<GetLeaderboardEntriesResult.LeaderboardEntries> listUserInfo)
    {
        List<string> userIdList = new List<string>();
        foreach (GetLeaderboardEntriesResult.LeaderboardEntries itemData in listUserInfo)
        {
            userIdList.Add(itemData.steamID);
            LeaderBoardItemData itemLeaderBoardData = new LeaderBoardItemData();
            itemLeaderBoardData.userId = itemData.steamID;
            itemLeaderBoardData.leaderboardEntries = itemData;
            mListLeaderBoardInfo.Add(itemLeaderBoardData);
        }
        getGlobalUserInfo(userIdList);
    }

    public class GlobalLeaderBoradEntriesCallBack : HttpResponseHandler<GetLeaderboardEntriesResult>
    {
        private LeaderBoardDialog leaderBoardDialog;
        public GlobalLeaderBoradEntriesCallBack(LeaderBoardDialog leaderBoardDialog)
        {
            this.leaderBoardDialog = leaderBoardDialog;
        }

        public override void onSuccess(GetLeaderboardEntriesResult result)
        {
            if (result == null || result.leaderboardEntryInformation == null || result.leaderboardEntryInformation.leaderboardEntries == null)
                return;
            List<GetLeaderboardEntriesResult.LeaderboardEntries> listUserInfo = result.leaderboardEntryInformation.leaderboardEntries;
            leaderBoardDialog.checkLeaderboradEntriesResultListForAll(listUserInfo);
        }


        public override void onError(string message)
        {
            DialogManager
                 .createToastDialog()
                 .setToastText("查询全球排行失败");
        }
    }
    #endregion



    #region -------- 查询用户的个人资料 -------- 
    private void getGlobalUserInfo(List<string> userIdList)
    {
        if (userIdList == null || userIdList.Count == 0) {
            mLoading.gameObject.SetActive(false);
            return;
        }
        MHttpManagerFactory.getSteamManagerPowered().getSteamUserInfo(userIdList, new SteamUserInfoCallBack(this));
    }

    public class SteamUserInfoCallBack : HttpResponseHandler<SteamUserInfoResult>
    {
        private LeaderBoardDialog leaderBoardDialog;
        public SteamUserInfoCallBack(LeaderBoardDialog leaderBoardDialog)
        {
            this.leaderBoardDialog = leaderBoardDialog;
        }

        public override void onSuccess(SteamUserInfoResult result)
        {
            leaderBoardDialog.mLoading.gameObject.SetActive(false);
            if (leaderBoardDialog.mListLeaderBoardInfo == null || result.response == null || result.response.players == null)
                return;
            List<SteamUserInfoResult.SteamUserItemInfo> listUserInfo = result.response.players;
            foreach (LeaderBoardItemData itemData in leaderBoardDialog.mListLeaderBoardInfo)
            {
                foreach (SteamUserInfoResult.SteamUserItemInfo itemUserInfo in listUserInfo)
                {
                    if (itemData.userId.Equals(itemUserInfo.steamid))
                    {
                        itemData.steamUserItemInfo = itemUserInfo;
                        break;
                    }
                };
            }
            leaderBoardDialog.setLeaderBoardListData(leaderBoardDialog.mListLeaderBoardInfo);
        }

        public override void onError(string message)
        {
            DialogManager
                 .createToastDialog()
                 .setToastText("查询用户数据失败");
        }
    }
    #endregion



    #region -------- 添加列表数据 --------
    private void setLeaderBoardListData(List<LeaderBoardItemData> listData)
    {
        //清空数据
        for (int i = 0; i < mWorldRank.childCount; i++)
        {
            Destroy(mWorldRank.GetChild(i).gameObject);
        }
        foreach (LeaderBoardItemData itemData in listData)
        {
            createLeaderBoardItem(itemData);
        }
    }

    private void createLeaderBoardItem(LeaderBoardItemData itemData)
    {
        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(LeaderBoardItemPath));
        itemObj.name = itemData.userId;
        itemObj.transform.SetParent(mWorldRank);
        itemObj.transform.localScale = Vector3.one;
        //设置头像图片
        Image userIcon = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "UserIcon");
        StartCoroutine(ResourcesManager.loadAsyncHttpImage(itemData.steamUserItemInfo.avatar, userIcon));
        //设置名字
        Text userName = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "UserName");
        userName.text = itemData.steamUserItemInfo.personaname;
        //设置排名
        Text userRank = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "UserRank");
        userRank.text = itemData.leaderboardEntries.rank + "";
        //设置分数
        Text userScore = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "UserScore");
        userScore.text = getTimeStr(itemData.leaderboardEntries.score);
    }
    #endregion



    #region -------- 按钮点击事件 --------
    private void cancelOnClick()
    {
        if (mCallBack != null)
            mCallBack.cancelOnClick();
        Destroy(gameObject);
    }
    private void submitOnClick()
    {
        if (mCallBack != null)
            mCallBack.submitOnClick();
        Destroy(gameObject);
    }
    #endregion



    #region -------- 点击回调 --------   
    public interface CallBack
    {
        void cancelOnClick();

        void submitOnClick();
    }
    #endregion



    #region -------- 列表Item数据 -------- 
    public class LeaderBoardItemData
    {
        public string userId;
        public SteamUserInfoResult.SteamUserItemInfo steamUserItemInfo;
        public GetLeaderboardEntriesResult.LeaderboardEntries leaderboardEntries;
    }
    #endregion
}