using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MenuSettingUIControl : BaseUIControl
{
    public Transform jigsawSelectTiltebar;
    public Button titleBarExitBT;

    public Text languageSelectionTitle;
    public Dropdown languageSelectionDropdown;
    

    private new void Awake()
    {
        base.Awake();

        //初始化标题栏
        jigsawSelectTiltebar = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "TitleBar");
        titleBarExitBT = CptUtil.getCptFormParentByName<Transform, Button>(jigsawSelectTiltebar, "ExitBT");
        if (titleBarExitBT != null)
        {
            titleBarExitBT.onClick.AddListener(addExitOnClick);
        }
        //初始化语言下拉
        languageSelectionTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "LanguageSelectionTitle");
        languageSelectionDropdown = CptUtil.getCptFormParentByName<Transform, Dropdown>(transform, "LanguageSelectionDropdown");

        refreshUI();
    }

    /// <summary>
    /// 语言选择
    /// </summary>
    /// <param name="position"></param>
    private void languageSelection(int position)
    {
        GameConfigureBean configure= DataStorageManage.getGameConfigureDSHandle().getData(0);
        configure.gameLanguage = position;
        DataStorageManage.getGameConfigureDSHandle().saveData(configure);
        CommonConfigure.refreshData();
        CommonData.refreshData();
   
        mUIMasterControl.refreshAllUI();
    }

    /// <summary>
    /// 增加退出按钮监听
    /// </summary>
    public void addExitOnClick()
    {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {

    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void refreshUI()
    {
        List<string>  listLanguageList = new List<string>();
        listLanguageList.Add("中文");
        listLanguageList.Add("English");

        if (languageSelectionTitle != null)
            languageSelectionTitle.text = CommonData.getText(28);
        if (languageSelectionDropdown != null)
        {
            languageSelectionDropdown.ClearOptions();
            languageSelectionDropdown.AddOptions(listLanguageList);
            languageSelectionDropdown.value = (int)CommonConfigure.GameLanguage;
            languageSelectionDropdown.onValueChanged.AddListener(languageSelection);
        }
    }
}