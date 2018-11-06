using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using Steamworks;

public class SteamWorkshopSelect : BaseMonoBehaviour
{
    public GameObject installModel;

    public void CreateInstallItemList(List<SteamWorkshopQueryInstallInfoBean> listData)
    {
        //删除原数据
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                Destroy(transform.GetChild(i).gameObject);
        }

        List<PuzzlesInfoBean> listPuzzlesInfo= CreateGameInfoListByInstallInfo(listData);
        List<PuzzlesCompleteStateBean> listCompleteData = DataStorageManage.getPuzzlesCompleteDSHandle().getAllData();
        List<PuzzlesProgressBean> listProgressData = DataStorageManage.getPuzzlesProgressDSHandle().getAllData();
        List<PuzzlesGameInfoBean> listGameInfoData = PuzzlesDataUtil.MergePuzzlesInfo(listPuzzlesInfo, listCompleteData, listProgressData);

        for (int itemPosition = 0; itemPosition < listData.Count; itemPosition++)
        {
            PuzzlesGameInfoBean itemInfo = listGameInfoData[itemPosition];
            CreateInstallItem(itemInfo);
        }
    }

    private List<PuzzlesInfoBean> CreateGameInfoListByInstallInfo(List<SteamWorkshopQueryInstallInfoBean> listData)
    {
        List<PuzzlesInfoBean> listInfoData = new List<PuzzlesInfoBean>();
        foreach (SteamWorkshopQueryInstallInfoBean itemData in listData)
        {
            PuzzlesInfoBean infoData = new PuzzlesInfoBean();
            infoData.id = 0;
            infoData.data_type = (int)JigsawResourcesEnum.Custom;
        
        }
        return listInfoData;
    }

    private GameObject CreateInstallItem(PuzzlesGameInfoBean itemInfo)
    {

        PuzzlesInfoBean infoBean = itemInfo.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = itemInfo.completeStateInfo;

        itemInfo.completeStateInfo = completeStateBean;
        itemInfo.puzzlesInfo = infoBean;

        GameObject itemObj = Instantiate(installModel);
        itemObj.transform.parent = this.transform;
        itemObj.transform.localScale = new Vector3(1f, 1f, 1f);
        itemObj.SetActive(true);

        itemObj.name = infoBean.Mark_file_name;
        itemObj.transform.SetParent(transform);

        //设置背景图片
        Image backImage = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "JigsawPic");
        string filePath = infoBean.Data_file_path + infoBean.Mark_file_name;
        StartCoroutine(ResourcesManager.LoadAsyncLocationImage(filePath, backImage));

        //设置按键
        Button startBT = CptUtil.getCptFormParentByName<Transform, Button>(itemObj.transform, "JigsawStart");
        startBT.onClick.AddListener(delegate ()
        {
            SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
            CommonData.SelectPuzzlesInfo = itemInfo;
            SceneUtil.jumpGameScene();
        });
        //最好分数
        Transform bestScoreTF = CptUtil.getCptFormParentByName<Transform, Transform>(itemObj.transform, "JigsawBestScore");
        Text bestScore = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "JigsawBestScoreText");
        if (completeStateBean != null && completeStateBean.completeTime != null)
        {
            bestScore.text = GameUtil.GetTimeStr(completeStateBean.completeTime.totalSeconds);
        }
        else
        {
            bestScoreTF.gameObject.SetActive(false);
        }

        //设置文本信息
        Text jigsawNameText = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "JigsawName");
        Text startBTText = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "JigsawStartText");

        if (itemInfo.progressInfo != null)
            startBTText.text = CommonData.getText(85);
        else
            startBTText.text = CommonData.getText(14);


        jigsawNameText.text = infoBean.Name;
        return itemObj;
    }
}