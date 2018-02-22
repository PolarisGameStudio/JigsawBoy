using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartControl : MonoBehaviour
{
    //图片信息
    public JigsawResInfoBean jigsawInfoData;
    //所有拼图信息
    private List<JigsawBean> listJigsawBean;

    // Use this for initialization
    void Start()
    {
        jigsawInfoData = CommonData.selectJigsawInfo;
        initData();
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
    /// 初始化数据
    /// </summary>
    private void initData()
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
        if (pic2D==null)
        {
            LogUtil.log("没有源图片");
            return;
        }

        createJigsaw(pic2D, horizontalNumber, verticalJigsawNumber);
        createWall(horizontalNumber, verticalJigsawNumber);
    }

    /// <summary>
    /// 创建拼图
    /// </summary>
    /// <param name="jigsawInfoData"></param>
    private void createJigsaw(Texture2D pic2D,int horizontalNumber,int verticalJigsawNumber)
    {
        listJigsawBean = CreateJigsawDataUtils.createJigsawDataList(JigsawStyleEnum.Normal, horizontalNumber, verticalJigsawNumber, pic2D);
        CreateJigsawGameObjUtil.createJigsawGameObjList(listJigsawBean);

        List<GameObject> containerList = CreateJigsawContainerObjUtil.createJigsawContainerObjList(listJigsawBean);
        for (int i = 0; i < listJigsawBean.Count; i++)
        {
            JigsawBean item = listJigsawBean[i];
            containerList[i].transform.position = new Vector3(0, 0, 0);
            containerList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /// <summary>
    /// 创建围墙
    /// </summary>
    /// <param name="wallWith"></param>
    /// <param name="wallHigh"></param>
    private void createWall(int horizontalNumber, int verticalJigsawNumber)
    {
        if(listJigsawBean!=null&& listJigsawBean.Count > 0)
        {
           JigsawBean itemJigsawBean= listJigsawBean[0];
            float picAllWith = itemJigsawBean.JigsawWith * horizontalNumber;
            float picAllHigh = itemJigsawBean.JigsawHigh * verticalJigsawNumber;
            CreateGameWallUtil.createWall(picAllWith, picAllHigh);
        }
    
    }
}
