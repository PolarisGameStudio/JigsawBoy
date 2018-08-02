
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JigsawSelect : BaseMonoBehaviour
{
    public MenuSelectUIControl menuSelectUIControl;

    private JigsawResourcesEnum resourcesType;
    private static string JigsawSelectItemPath = "Prefab/UI/Menu/JigsawSelectItem";
    private static string JigsawSelectLockItemPath = "Prefab/UI/Menu/JigsawSelectLockItem";
    private static string JigsawSelectCustomItemPath = "Prefab/UI/Menu/JigsawSelectCustomItem";

    private Sprite mLevel1;
    private Sprite mLevel2;
    private Sprite mLevel3;
    // Use this for initialization
    void Start()
    {
        mLevel1 = ResourcesManager.loadData<Sprite>("Texture/UI/icon_level_1");
        mLevel2 = ResourcesManager.loadData<Sprite>("Texture/UI/icon_level_2");
        mLevel3 = ResourcesManager.loadData<Sprite>("Texture/UI/icon_level_3");
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
        for (int itemPosition = 0; itemPosition < listData.Count; itemPosition++)
        {
            PuzzlesGameInfoBean itemInfo = listData[itemPosition];
            createSelectItem(itemPosition, itemInfo);
        }
    }


    /// <summary>
    /// 创建相对应按钮
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createSelectItem(int position, PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        if (infoBean.Data_type.Equals((int)JigsawResourcesEnum.Custom))
        {
            createCustomItem(itemInfo);
        }
        else
        {
            if ( completeStateBean == null || completeStateBean.unlockState.Equals(JigsawUnlockEnum.Lock))
            {
                if (infoBean.level == 1)
                    createNormalItem(itemInfo);
                else
                    createLockItem(position, itemInfo);
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
    private void createLockItem(int position, PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        //解锁点数处理
        if (infoBean.unlock_point == 1)
        {
            infoBean.unlock_point = infoBean.level * infoBean.level;
        }

        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectLockItemPath));
        Button itemBT = itemObj.GetComponent<Button>();

        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);

        //设置按键
        Button unLockBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawUnLock");
        unLockBT.onClick.AddListener(
        delegate ()
        {
            long userPoint = DataStorageManage.getUserInfoDSHandle().getData(0).puzzlesPoint;
            if (userPoint < infoBean.unlock_point)
            {
                //如果没有PP则提示不足
                DialogManager.createToastDialog().setToastText(CommonData.getText(16));
            }
            else
            {
                //如果有PP则解锁
                //保存信息
                ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).decreaseUserPuzzlesPoint(infoBean.unlock_point);
                menuSelectUIControl.refreshPuzzlesPoint();
                //解锁拼图
                if (completeStateBean == null)
                {
                    completeStateBean = new PuzzlesCompleteStateBean();
                    completeStateBean.puzzleId = infoBean.id;
                    completeStateBean.puzzleType = infoBean.data_type;
                }
                completeStateBean.unlockState = JigsawUnlockEnum.UnLock;
                DataStorageManage.getPuzzlesCompleteDSHandle().saveData(completeStateBean);
                //menuSelectUIControl.refreshJigsawSelectData();
                menuSelectUIControl.refreshItemJigsawSelectData(position, itemObj, itemInfo);
                //解锁成功动画
                string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
                DialogManager.createUnlockPuzzlesDialog(infoBean.name, filePath);
            }
        });
        //设置文本信息
        Text jigsawUnLockText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawUnLockText");
        jigsawUnLockText.text = CommonData.getText(13) + "( " + infoBean.unlock_point + "PP )";

        //设置拼图等级
        setLevel(itemObj, infoBean.level);
    }


    /// <summary>
    /// 创建正常样式
    /// </summary>
    public GameObject createNormalItem(PuzzlesGameInfoBean itemInfo)
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
        StartCoroutine(ResourcesManager.loadAsyncDataImage(filePath + "", backImage));


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
            DialogManager.createLeaderBoradDialog(1, itemInfo);
        });

        //设置文本信息
        Text jigsawNameText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawName");
        Text startBTText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawStartText");
        Text scoreBTText = CptUtil.getCptFormParentByName<Button, Text>(itemBT, "JigsawScoreText");
        jigsawNameText.text = infoBean.Name;
        startBTText.text = CommonData.getText(14);
        scoreBTText.text = CommonData.getText(15);

        //设置拼图等级
        setLevel(itemObj, infoBean.level);
        return itemObj;
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
        Image backImage = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "JigsawPic");
        string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
        StartCoroutine(ResourcesManager.loadAsyncLocationImage(filePath, backImage));

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
        Text jigsawNameText = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "JigsawName");
        jigsawNameText.text = infoBean.Name;

        //设置按钮信息
        //编辑按钮
        Button editBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawEdit");
        editBT.onClick.AddListener(delegate ()
        {
            MenuCustomUpLoadUIControl upLoadUIControl = menuSelectUIControl.mUIMasterControl.getUIByType<MenuCustomUpLoadUIControl>(UIEnum.MenuCustomUpLoadUI);
            upLoadUIControl.setInitData(infoBean);
            menuSelectUIControl.mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuCustomUpLoadUI);
        });
        //删除按钮
        Button deleteBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawDelete");
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
        Sprite levelTX;
        if (level > 0 && level <= 10)
            levelTX = mLevel1;
        else if (level > 10 && level <= 20)
            levelTX = mLevel2;
        else
            levelTX = mLevel3;
        levelPic.sprite = levelTX;
        levelText.text = "x" + level;
    }
}
