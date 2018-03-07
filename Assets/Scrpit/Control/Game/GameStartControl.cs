using System;
using System.Collections.Generic;
using UnityEngine;


public class GameStartControl : BaseMonoBehaviour
{

    public UIMasterControl uiMasterControl;
    public AudioSourceControl audioSourceControl;

    //图片信息
    public PuzzlesInfoBean jigsawInfoData;
    //所有拼图信息
    private List<JigsawBean> listJigsawBean;
    //所有的拼图容器
    private List<GameObject> containerList;

    //图片的宽和高
    public float picAllWith;
    public float picAllHigh;

    private void Awake()
    {
        uiMasterControl = gameObject.AddComponent<UIMasterControl>();
        audioSourceControl = gameObject.AddComponent<AudioSourceControl>();
        initData();
    }

    private void Start()
    {
        uiMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void initData()
    {
        jigsawInfoData = CommonData.SelectPuzzlesInfo;
        if (jigsawInfoData == null)
        {
            LogUtil.log("没有拼图数据");
            return;
        }
        string resFilePath = jigsawInfoData.Data_file_path + jigsawInfoData.Mark_file_name;
        int horizontalNumber = jigsawInfoData.Horizontal_number;
        int verticalJigsawNumber = jigsawInfoData.Vertical_number;
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
        Texture2D pic2D = ResourcesManager.loadData<Texture2D>(resFilePath);
        if (pic2D == null)
        {
            LogUtil.log("没有源图片");
            return;
        }

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
        //启动动画
        startAnim();
    }


    /// <summary>
    /// 创建拼图
    /// </summary>
    /// <param name="jigsawInfoData"></param>
    private void createJigsaw(Texture2D pic2D, int horizontalNumber, int verticalJigsawNumber)
    {
        listJigsawBean = CreateJigsawDataUtils.createJigsawDataList(JigsawStyleEnum.Normal, horizontalNumber, verticalJigsawNumber, pic2D);
        CreateJigsawGameObjUtil.createJigsawGameObjList(listJigsawBean, pic2D);

        containerList = CreateJigsawContainerObjUtil.createJigsawContainerObjList(listJigsawBean);
        for (int i = 0; i < listJigsawBean.Count; i++)
        {
            JigsawBean item = listJigsawBean[i];
            Vector3 jigsawPosition = new Vector3(
                item.MarkLocation.x * item.JigsawWith - item.JigsawWith * horizontalNumber / 2f + item.JigsawWith / 2f,
                item.MarkLocation.y * item.JigsawHigh - item.JigsawHigh * verticalJigsawNumber / 2f + item.JigsawHigh / 2f
                );
            containerList[i].transform.position = jigsawPosition;
        }
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
            cameraControl.setCameraOrthographicSize(picAllHigh);
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

    /// <summary>
    /// 开始动画
    /// </summary>
    private void startAnim()
    {
        int animInt = DevUtil.getRandomInt(1, 3);
        GameStartAnimationEnum animEnum = (GameStartAnimationEnum)animInt;
        GameStartAnimationManager.startAnimation(this, containerList, animEnum);
    }

    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void gameStart()
    {
        GameMainUIControl gameMainUI= uiMasterControl.getUIByType<GameMainUIControl>(UIEnum.GameMainUI);
        if (gameMainUI != null)
        {
            gameMainUI.startTimer();
        }
    }
}
