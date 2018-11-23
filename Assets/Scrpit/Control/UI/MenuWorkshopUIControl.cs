using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using Steamworks;

public class MenuWorkshopUIControl : BaseUIControl
{
    //退出按钮
    public Button exitBt;
    //标题
    public Text workshopTitleText;
    public Text workshopCreateText;
    //增加按钮
    public Button workshoCreateBt;

    //页数加减按钮
    public Button pageNumberAddBt;
    public Button pageNumberSubBt;
    public Text pageNumberText;

    //类型修改
    public Button subscribedBT;
    public Button myCreateBT;
    public Text subscribedText;
    public Text myCreateText;

    public SteamWorkshopSelect installSelect;

    public uint currentPage;
    public EUserUGCList pageType;

    private new void Awake()
    {
        base.Awake();

        exitBt.onClick.AddListener(ExitOnClick);
        pageNumberAddBt.onClick.AddListener(AddPageOnClick);
        pageNumberSubBt.onClick.AddListener(SubPageOnClick);
        workshoCreateBt.onClick.AddListener(CreateOnClick);

        subscribedBT.onClick.AddListener(SubscribedTypeOnClick);
        myCreateBT.onClick.AddListener(MyCreateTypeOnClick);

        refreshUI();
       
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        GetInstallItemInfo(currentPage, pageType);
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
        refreshUI();
        loadUIData();
    }

    public override void refreshUI()
    {
        if (workshopTitleText != null)
            if (pageType == EUserUGCList.k_EUserUGCList_Published)
            {
                workshopTitleText.text = CommonData.getText(132);
            }
            else if (pageType == EUserUGCList.k_EUserUGCList_Subscribed)
            {
                workshopTitleText.text = CommonData.getText(122);
            }
        if (pageNumberText != null)
            pageNumberText.text = currentPage + "";
        if (workshopCreateText != null)
            workshopCreateText.text = CommonData.getText(131);
        if(subscribedText!=null)
            subscribedText.text= CommonData.getText(122);
        if (myCreateText != null)
            myCreateText.text = CommonData.getText(132);
    }

    public void SubscribedTypeOnClick()
    {
        currentPage = 1;
        pageType = EUserUGCList.k_EUserUGCList_Subscribed;
        refreshUI();
        loadUIData();
    }


    public void MyCreateTypeOnClick()
    {
        currentPage = 1;
        pageType = EUserUGCList.k_EUserUGCList_Published;
        refreshUI();
        loadUIData();
    }

    /// <summary>
    /// 创建
    /// </summary>
    public void CreateOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuWorkshopCreate);
    }

    /// <summary>
    /// 退出
    /// </summary>
    public void ExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }

    /// <summary>
    /// 增加页数
    /// </summary>
    public void AddPageOnClick()
    {
        currentPage++;
        refreshUI();
        loadUIData();
    }

    /// <summary>
    /// 减少页数
    /// </summary>
    public void SubPageOnClick()
    {
        if (currentPage > 1)
        {
            currentPage--;
            refreshUI();
            loadUIData();
        }
    }

    public void GetInstallItemInfo(uint page, EUserUGCList type)
    {
        SteamWorkshopHandle.QueryInstallInfo(this, page, type, new InstallItemListCallBack(this));
    }


    public class InstallItemListCallBack : ISteamWorkshopQueryInstallInfoCallBack
    {
        private readonly MenuWorkshopUIControl mMenuWorkshopUI;

        public InstallItemListCallBack(MenuWorkshopUIControl menuWorkshopUI)
        {
            this.mMenuWorkshopUI = menuWorkshopUI;
        }
        public void GetInstallInfoFail(SteamWorkshopQueryImpl.SteamWorkshopQueryFailEnum failEnum)
        {
            LogUtil.log("fail");
        }

        public void GetInstallInfoSuccess(List<SteamWorkshopQueryInstallInfoBean> listData)
        {
            mMenuWorkshopUI.installSelect.CreateInstallItemList(listData);
        }
    }
}