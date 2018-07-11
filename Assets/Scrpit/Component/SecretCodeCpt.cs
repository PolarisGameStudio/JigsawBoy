using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeCpt : BaseMonoBehaviour
{

    private string[] mSecretCode = new string[]
    {
        "ADDPUZZLESPOINT"
    };
    private int mSecretCodeMax;
    private bool isOpenSecretCode = false;

    public SecretCodeCpt()
    {
        mSecretCodeMax = 0;
        foreach (string itemCode in mSecretCode) {
            if (itemCode.Length > mSecretCodeMax) {
                mSecretCodeMax = itemCode.Length;
            }
        }
    }

    private void Update()
    {
        detectPressedKeyOrButton();
    }

    private string mTempCode = "";

    /// <summary>
    /// 检测是否开启秘密代码
    /// </summary>
    /// <returns></returns>
    private bool checkIsOpenSecretCode()
    {
        MenuStartControl uiControl = GetComponent<MenuStartControl>();
        if (uiControl != null && uiControl.uiMasterControl != null)
            return uiControl.uiMasterControl.isShowUI(UIEnum.MenuMainUI);
        else
            return false;
    }

    /// <summary>
    /// 遍历按键
    /// </summary>
    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                checkCode(kcode.ToString());
        }
    }

    /// <summary>
    /// 秘密代码检测
    /// </summary>
    /// <param name="itemCode"></param>
    private void checkCode(string itemCode)
    {
        if (itemCode.Equals("Return"))
        {
            if (checkIsOpenSecretCode()) {
                foreach (string itemSecretCode in mSecretCode)
                {
                    if (itemSecretCode.Equals(mTempCode))
                    {
                        secretCodeHandler(mTempCode);
                        break;
                    }
                }
            }
            mTempCode = "";
        }
        else
        {
            mTempCode += itemCode;
        }
        if(mTempCode.Length> mSecretCodeMax)
        {
            mTempCode = "";
        }
    }

    /// <summary>
    /// 秘密代码处理
    /// </summary>
    /// <param name="secretCode"></param>
    private void secretCodeHandler(string secretCode)
    {
        LogUtil.log("SecretCode:" + secretCode);
        switch (secretCode)
        {
            case "ADDPUZZLESPOINT":
                addPuzzlesPoint();
                break;
        }
    }


    /// <summary>
    /// 增加拼图点数
    /// </summary>
    private void addPuzzlesPoint()
    {
        ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).increaseUserPuzzlesPoint(1000);
    }


}
