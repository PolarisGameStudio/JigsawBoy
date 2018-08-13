using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeCpt : BaseMonoBehaviour
{

    private string[] mSecretCode = new string[]
    {
        "ADDPUZZLESPOINT",
        "ILOVEPUZZLES"
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
        UIMasterControl uiControl = GetComponent<UIMasterControl>();
        if (uiControl != null) {
            bool isOpen = false;
            if (uiControl.isShowUI(UIEnum.MenuMainUI)
            || uiControl.isShowUI(UIEnum.GameMainUI))
            {
                isOpen = true;
            }
            return isOpen;
        }
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
            LogUtil.log(mTempCode+"");
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
        switch (secretCode)
        {
            case "ADDPUZZLESPOINT":
                addPuzzlesPoint();
                break;
            case "ILOVEPUZZLES":
                completePuzzles();
                break;
        }
        DialogManager.createToastDialog().setToastText("secretCode");
    }


    /// <summary>
    /// 增加拼图点数
    /// </summary>
    private void addPuzzlesPoint()
    {
        ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).increaseUserPuzzlesPoint(1000);
    }


    /// <summary>
    /// 自动完成拼图
    /// </summary>
    private void completePuzzles()
    {
        JigsawContainerCpt[] cptList = FindObjectsOfType<JigsawContainerCpt>();
        if (cptList == null || cptList.Length == 0)
            return;
        //设置不可在拖拽
        CommonData.IsDargMove = false;
        JigsawContainerCpt tempCpt = cptList[0];
        for (int i = 0; i < cptList.Length; i++) {
            JigsawContainerCpt itemCpt = cptList[i];
            itemCpt.isSelect = false;
            //设置质量为0 防止动画时错位
            Rigidbody2D itemRB = itemCpt.GetComponent<Rigidbody2D>();
            itemRB.velocity = Vector3.zero;
            //顺便冻结缸体
            itemRB.constraints = RigidbodyConstraints2D.FreezeAll;
            // 添加拼图碎片到容器里
            if (i > 0)
            {
                tempCpt.addJigsawList(itemCpt.listJigsaw);
                // 最后删除当前容器
                Destroy(itemCpt.gameObject);
            }
        }
 
        //位置纠正
        tempCpt.jigsawLocationCorrect(3);
    }

}
