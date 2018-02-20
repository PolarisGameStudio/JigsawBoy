using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartControl : MonoBehaviour
{

    public JigsawResInfoBean jigsawInfoData;

    // Use this for initialization
    void Start()
    {
        jigsawInfoData = CommonData.selectJigsawInfo;
        createJigsaw(jigsawInfoData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setJigsawResInfo(JigsawResInfoBean jigsawInfoData)
    {
        this.jigsawInfoData = jigsawInfoData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jigsawInfoData"></param>
    private void createJigsaw(JigsawResInfoBean jigsawInfoData)
    {
        if (jigsawInfoData == null)
            return;
        string resFilePath = jigsawInfoData.resFilePath;
        int horizontalNumber = jigsawInfoData.horizontalNumber;
        int verticalJigsawNumber = jigsawInfoData.verticalJigsawNumber;

        if (resFilePath == null)
        {
            LogUtil.log("没有拼图图片路径");
            return;
        }
        if (horizontalNumber == 0 || verticalJigsawNumber == 0)
        {
            LogUtil.log("没有拼图生成数量");
            return;
        }


        Texture2D pic2D = (Texture2D)Resources.Load(jigsawInfoData.resFilePath);
        List<JigsawBean> listData = CreateJigsawDataUtils.createJigsawDataList(JigsawStyleEnum.Normal, horizontalNumber, verticalJigsawNumber, pic2D);
        CreateJigsawGameObjUtil.createJigsawGameObjList(listData);

        List<GameObject> containerList = CreateJigsawContainerObjUtil.createJigsawContainerObjList(listData);
        for (int i = 0; i < listData.Count; i++)
        {
            JigsawBean item = listData[i];

            containerList[i].transform.position = new Vector3(item.MarkLocation.x * 3.5f, item.MarkLocation.y * 3.5f, 10);
            containerList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
