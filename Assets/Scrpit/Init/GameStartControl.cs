using System.Collections.Generic;
using UnityEngine;


public class GameStartControl : BaseMonoBehaviour
{

    public static string Game_Timer_Obj_Path = "/GameUI/GameTimer";
    //图片信息
    public JigsawResInfoBean jigsawInfoData;
    //所有拼图信息
    private List<JigsawBean> listJigsawBean;

    //图片的宽和高
    private float picAllWith;
    private float picAllHigh;

    //游戏计时器
    private Transform gameTimerTF;

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
        Texture2D pic2D = ResourcesManager.loadData<Texture2D>(jigsawInfoData.resFilePath);
        if (pic2D == null)
        {
            LogUtil.log("没有源图片");
            return;
        }
        //获取计时
        gameTimerTF = transform.Find(Game_Timer_Obj_Path);


        //生成拼图
        createJigsaw(pic2D, horizontalNumber, verticalJigsawNumber);
        //获取图片的高和宽
        if (listJigsawBean != null && listJigsawBean.Count > 0)
        {
            JigsawBean itemJigsawBean = listJigsawBean[0];
            picAllWith = itemJigsawBean.JigsawWith * horizontalNumber;
            picAllHigh = itemJigsawBean.JigsawHigh * verticalJigsawNumber;
        }
        //生成围墙
        createWall(picAllWith, picAllHigh);
        //生成背景
        createBackground(picAllWith, picAllHigh);
        //增加镜头控制
        addCameraControl(picAllWith, picAllHigh);
        //增加拼图控制
        addJigsawControl(picAllWith, picAllHigh);
    }

    /// <summary>
    /// 创建拼图
    /// </summary>
    /// <param name="jigsawInfoData"></param>
    private void createJigsaw(Texture2D pic2D, int horizontalNumber, int verticalJigsawNumber)
    {
        listJigsawBean = CreateJigsawDataUtils.createJigsawDataList(JigsawStyleEnum.Normal, horizontalNumber, verticalJigsawNumber, pic2D);
        CreateJigsawGameObjUtil.createJigsawGameObjList(listJigsawBean, pic2D);

        List<GameObject> containerList = CreateJigsawContainerObjUtil.createJigsawContainerObjList(listJigsawBean);
        for (int i = 0; i < listJigsawBean.Count; i++)
        {
            JigsawBean item = listJigsawBean[i];
            Vector3 jigsawPosition = new Vector3(
                item.MarkLocation.x * item.JigsawWith - item.JigsawWith * horizontalNumber / 2f + item.JigsawWith / 2f,
                item.MarkLocation.y * item.JigsawHigh - item.JigsawHigh * verticalJigsawNumber / 2f + item.JigsawHigh / 2f
                );
            containerList[i].transform.position = jigsawPosition;
        }
        GameStartAnimationManager.startAnimation(this, containerList, GameStartAnimationEnum.Closure_Dispersed);
    }

    /// <summary>
    /// 创建围墙
    /// </summary>
    /// <param name="wallWith"></param>
    /// <param name="wallHigh"></param>
    private void createWall(float picAllWith, float picAllHigh)
    {
        if (picAllWith == 0 || picAllHigh == 0)
        {
            LogUtil.log("无法生成围墙，缺少高和宽");
            return;
        }
        CreateGameWallUtil.createWall(picAllWith, picAllHigh);
    }

    /// <summary>
    /// 创建背景
    /// </summary>
    /// <param name="picAllWith"></param>
    /// <param name="picAllHigh"></param>
    private void createBackground(float picAllWith, float picAllHigh)
    {
        if (picAllWith == 0 || picAllHigh == 0)
        {
            LogUtil.log("无法生成背景，缺少高和宽");
            return;
        }
        CreateGameBackgroundUtil.createBackground(picAllWith, picAllHigh);
    }

    /// <summary>
    /// 增加镜头控制
    /// </summary>
    private void addCameraControl(float picAllWith, float picAllHigh)
    {
        GameCameraControlCpt cameraControl = gameObject.AddComponent<GameCameraControlCpt>();
        //设置镜头缩放大小
        if (picAllWith > picAllHigh)
        {
            cameraControl.setCameraOrthographicSize(picAllHigh );
            cameraControl.zoomOutMax = picAllWith;
        }
        else
        {
            cameraControl.setCameraOrthographicSize(picAllWith);
            cameraControl.zoomOutMax = picAllHigh;
        }
        cameraControl.cameraMoveWithMax = picAllWith;
        cameraControl.cameraMoveHighMax = picAllHigh;
    }


    /// <summary>
    /// 增加拼图控制
    /// </summary>
    private void addJigsawControl(float picAllWith, float picAllHigh)
    {
        GameJigsawControlCpt jigsawControl = gameObject.AddComponent<GameJigsawControlCpt>();
        jigsawControl.moveWithMax = picAllWith;
        jigsawControl.moveHighMax = picAllHigh;
    }
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void gameStart()
    {
        if (gameTimerTF != null)
        {
            GameTimerControlCpt gameTimer = gameTimerTF.GetComponent<GameTimerControlCpt>();
            if (gameTimer != null)
                gameTimer.startTimer();
        }
    }
}
