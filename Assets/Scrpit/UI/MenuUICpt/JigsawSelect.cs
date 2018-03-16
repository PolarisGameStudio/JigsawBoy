
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
        List<PuzzlesInfoBean> listInfoData = PuzzlesInfoManager.LoadAllPuzzlesDataByType(resourcesEnum);
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


        createNormalItem(itemInfo);
        //if (infoBean.Level <= 1)
        //{
        //    createNormalItem(itemInfo);
        //    return;
        //}

        //if (completeStateBean == null || completeStateBean.unlockState.Equals(JigsawUnlockEnum.Lock))
        //{
        //    createLockItem(itemInfo);
        //    return;
        //}
        //else
        //{
        //    createNormalItem(itemInfo);
        //    return;
        //}
    }

    /// <summary>
    /// 创建未解锁样式
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createLockItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;

        GameObject itemObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectLockItemPath));
        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);
    }

    /// <summary>
    /// 创建正常样式
    /// </summary>
    private void createNormalItem(PuzzlesGameInfoBean itemInfo)
    {
        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        GameObject buttonObj = Instantiate(ResourcesManager.loadData<GameObject>(JigsawSelectItemPath));
        buttonObj.name = infoBean.Mark_file_name;
        buttonObj.transform.SetParent(transform);

        //设置背景图片
        Image backImage = buttonObj.GetComponent<Image>();
        string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
        Sprite backSp = ResourcesManager.loadData<Sprite>(filePath);
        backImage.sprite = backSp;

        //设置按键
        Button itemBT = buttonObj.GetComponent<Button>();
        itemBT.onClick.AddListener(delegate ()
        {
            CommonData.SelectPuzzlesInfo = itemInfo;
            SceneUtil.jumpGameScene();
        });


        //设置文本信息
        Text[] allText = buttonObj.GetComponentsInChildren<Text>();
        if (allText != null)
        {
            int allTextSize = allText.Length;
            for (int textPosition = 0; textPosition < allTextSize; textPosition++)
            {
                Text textItem = allText[textPosition];
                if (textItem.name.Equals("JigsawName"))
                {
                    textItem.text = infoBean.Name + infoBean.Level;
                }
            }
        }
    }

}
