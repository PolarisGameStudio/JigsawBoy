﻿
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class JigsawSelect : BaseMonoBehaviour
{
    public MenuSelectUIControl menuSelectUIControl;

    private JigsawResourcesEnum resourcesType;
    private static string JigsawSelectItemPath = "Prefab/UI/Menu/JigsawSelectItem";
    private static string JigsawSelectLockItemPath = "Prefab/UI/Menu/JigsawSelectLockItem";
    private static string JigsawSelectCustomItemPath = "Prefab/UI/Menu/JigsawSelectCustomItem";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 设置UI控制器
    /// </summary>
    /// <param name="menuSelectUIControl"></param>
    public void setMenuSelectUIControl(MenuSelectUIControl menuSelectUIControl)
    {
        this.menuSelectUIControl = menuSelectUIControl;
    }



    /// <summary>
    /// 读取拼图信息
    /// </summary>
    /// <param name="resourcesEnum"></param>
    public void loadJigsaw(JigsawResourcesEnum resourcesEnum)
    {
        StopAllCoroutines();
        resourcesType = resourcesEnum;
        //删除原数据
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //加载该类型下所有拼图数据
        List<PuzzlesInfoBean> listInfoData = null;
        if (resourcesEnum.Equals(JigsawResourcesEnum.Custom))
        {
            listInfoData = DataStorageManage.getCustomPuzzlesInfoDSHandle().getAllData();
        }
        else
        {
            listInfoData = PuzzlesInfoManager.LoadAllPuzzlesDataByType(resourcesEnum);
        }

        if (listInfoData == null || listInfoData.Count == 0)
            return;
        listInfoData.Sort((x, y) => x.Level.CompareTo(y.Level));

        List<PuzzlesCompleteStateBean> listCompleteData = DataStorageManage.getPuzzlesCompleteDSHandle().getAllData();
        List<PuzzlesGameInfoBean> listData = PuzzlesDataUtil.MergePuzzlesInfoAndCompleteState(listInfoData, listCompleteData);

        StartCoroutine(createSelect(listData));
    }


    IEnumerator createSelect(List<PuzzlesGameInfoBean> listData)
    {
        int resourcesListCount = listData.Count;
        for (int itemPosition = 0; itemPosition < resourcesListCount; itemPosition++)
        {
            PuzzlesGameInfoBean itemInfo = listData[itemPosition];
            yield return new WaitForEndOfFrame();
            createSelectItem(itemInfo);
        }
    }

    /// <summary>
    /// 创建相对应按钮
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createSelectItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        if (infoBean.Data_type.Equals((int)JigsawResourcesEnum.Custom))
        {
            createCustomItem(itemInfo);
        }
        else
        {
            if (completeStateBean == null || completeStateBean.unlockState.Equals(JigsawUnlockEnum.Lock))
            {
                createLockItem(itemInfo);
            }
            else
            {
                createNormalItem(itemInfo);
            }
        }
    }

    /// <summary>
    /// 创建未解锁样式
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createLockItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectLockItemPath));
        Button itemBT = itemObj.GetComponent<Button>();

        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);

        //设置按键
        Button unLockBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawUnLock");
        unLockBT.onClick.AddListener(delegate ()
        {
            if (completeStateBean == null) {
                completeStateBean = new PuzzlesCompleteStateBean();
                completeStateBean.puzzleId = infoBean.id;
                completeStateBean.puzzleType = infoBean.data_type;
            }
            completeStateBean.unlockState = JigsawUnlockEnum.UnLock;
            DataStorageManage.getPuzzlesCompleteDSHandle().saveData(completeStateBean);
            menuSelectUIControl.refreshJigsawSelectData();
        });
        //设置文本信息
        Text jigsawUnLockText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawUnLockText");
        jigsawUnLockText.text = "解锁(" + infoBean.unlock_point+")";

        //设置拼图等级
        setLevel(itemObj, infoBean.level);
    }


    /// <summary>
    /// 创建正常样式
    /// </summary>
    private void createNormalItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectItemPath));
        Button itemBT = itemObj.GetComponent<Button>();

        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);

        //设置背景图片
        Image backImage = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "JigsawPic");
        string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
        Sprite backSp = ResourcesManager.loadData<Sprite>(filePath);
        backImage.sprite = backSp;

        //设置按键
        Button startBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawStart");
        startBT.onClick.AddListener(delegate ()
        {
            CommonData.SelectPuzzlesInfo = itemInfo;
            SceneUtil.jumpGameScene();
        });
        Button scoreBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawScore");
        scoreBT.onClick.AddListener(delegate ()
        {

        });


        //设置文本信息
        Text jigsawNameText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawName");
        if (jigsawNameText != null)
            jigsawNameText.text = infoBean.Name + infoBean.Level;

        //设置拼图等级
        setLevel( itemObj, infoBean.level);
    }



    /// <summary>
    /// 创建自定义样式
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createCustomItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectCustomItemPath));
        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);

        //设置背景图片
        Image backImage = itemObj.GetComponent<Image>();
        string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
        StartCoroutine(ResourcesManager.loadLocationImage(filePath, backImage));

        //设置按键
        Button itemBT = itemObj.GetComponent<Button>();
        itemBT.onClick.AddListener(delegate ()
        {
            CommonData.SelectPuzzlesInfo = itemInfo;
            SceneUtil.jumpGameScene();
        });

        //设置文本信息
        Text jigsawNameText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawName");
        if (jigsawNameText != null)
            jigsawNameText.text = infoBean.Name;

        //设置按钮信息
        //编辑按钮
        Button editBT = CptUtil.getCptFormParentByName<Button, Button>(itemBT, "EditBT");
        editBT.onClick.AddListener(delegate ()
        {
            MenuCustomUpLoadUIControl upLoadUIControl = menuSelectUIControl.mUIMasterControl.getUIByType<MenuCustomUpLoadUIControl>(UIEnum.MenuCustomUpLoadUI);
            upLoadUIControl.setInitData(infoBean);
            menuSelectUIControl.mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuCustomUpLoadUI);
        });
        //删除按钮
        Button deleteBT = CptUtil.getCptFormParentByName<Button, Button>(itemBT, "DeleteBT");
        deleteBT.onClick.AddListener(delegate ()
        {
            FileUtil.DeleteFile(filePath);
            CustomPuzzlesInfoDSHandle handle = (CustomPuzzlesInfoDSHandle)DataStorageManage.getCustomPuzzlesInfoDSHandle();
            handle.removeData(infoBean);
            menuSelectUIControl.setJigsawSelectData(JigsawResourcesEnum.Custom);
        });
    }


    /// <summary>
    /// 设置拼图等级
    /// </summary>
    private void setLevel(GameObject itemObj, int level)
    {
        //设置拼图等级
        Image levelPic = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "JigsawLevelPic");
        Text levelText = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "JigsawLevelText");
        string levelIconPath = "";
        if (level > 0 && level <= 10)
            levelIconPath = "Texture/UI/icon_level_1";
        else if (level > 10 && level <= 20)
            levelIconPath = "Texture/UI/icon_level_2";
        else
            levelIconPath = "Texture/UI/icon_level_3";
        Sprite levelSP = ResourcesManager.loadData<Sprite>(levelIconPath);
        levelPic.sprite = levelSP;
        levelText.text = "x" + level;
    }
}
