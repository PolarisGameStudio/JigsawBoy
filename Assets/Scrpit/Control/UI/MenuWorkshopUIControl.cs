using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public SteamWorkshopSelect installSelect;

    public uint currentPage;

    private new void Awake()
    {
        base.Awake();

        exitBt.onClick.AddListener(ExitOnClick);
        pageNumberAddBt.onClick.AddListener(AddPageOnClick);
        pageNumberSubBt.onClick.AddListener(SubPageOnClick);
        workshoCreateBt.onClick.AddListener(CreateOnClick);
        refreshUI();
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        GetInstallItemInfo(currentPage);
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
            workshopTitleText.text = CommonData.getText(122);
        if (pageNumberText != null)
            pageNumberText.text = currentPage + "";
        if (workshopCreateText != null)
            workshopCreateText.text = CommonData.getText(131);
    }

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

    public void GetInstallItemInfo(uint page)
    {
        SteamWorkshopHandle.QueryInstallInfo(this, page, new InstallItemListCallBack(this));
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