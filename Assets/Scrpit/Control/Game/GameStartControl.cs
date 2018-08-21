using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class GameStartControl : BaseMonoBehaviour ,LeaderBoardDialog.CallBack
{

    public UIMasterControl uiMasterControl;
    public AudioSourceControl audioSourceControl;
    public GameCameraControlCpt cameraControl;

    //图片信息
    public PuzzlesInfoBean jigsawInfoData;
    //所有拼图信息
    private List<JigsawBean> listJigsawBean;
    //所有的拼图容器
    private List<GameObject> containerList;

    //图片的宽和高
    public float picAllWith;
    public float picAllHigh;

    public float gameFinshAnimTime;
    private void Awake()
    {
        CommonData.GameStatus = 0;
        gameFinshAnimTime = 3f;
        uiMasterControl = gameObject.AddComponent<UIMasterControl>();
        audioSourceControl = gameObject.AddComponent<AudioSourceControl>();
        audioSourceControl.stopBGMClip();
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
        jigsawInfoData = CommonData.SelectPuzzlesInfo.puzzlesInfo;
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
        Texture2D pic2D;
        if (jigsawInfoData.Data_type.Equals((int)JigsawResourcesEnum.Custom))
        {
            WWW www= ResourcesManager.LoadLocationData(resFilePath);
            pic2D = www.texture;
        }
        else
        {
             pic2D = ResourcesManager.LoadAssetBundlesTexture2DForBytes(resFilePath, jigsawInfoData.Mark_file_name);
        }
     
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

            //设置起始位置和角度
            JigsawContainerCpt containerCpt = containerList[i].transform.GetComponent<JigsawContainerCpt>();
            if (containerCpt != null)
            {
                containerCpt.startPosition = jigsawPosition;
                containerCpt.startRotation = containerList[i].transform.rotation;
            }

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
        cameraControl = gameObject.AddComponent<GameCameraControlCpt>();
        //设置镜头缩放大小
        float cameraOrthographicSize = 0;
        if (picAllWith > picAllHigh)
        {
            cameraOrthographicSize = picAllHigh;
            cameraControl.zoomOutMax = picAllWith;
        }
        else
        {
            cameraOrthographicSize = picAllWith;
            cameraControl.zoomOutMax = picAllHigh;
        }
        cameraControl.cameraMoveWithMax = picAllWith;
        cameraControl.cameraMoveHighMax = picAllHigh;
        cameraControl.setCameraOrthographicSize(cameraOrthographicSize);

        cameraControl.startCameraOrthographicSize = cameraOrthographicSize;
        cameraControl.startCameraPosition = cameraControl.transform.position;
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
        GameStartAnimationManager.StartAnimation(this, containerList);
    }

    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void gameStart()
    {
        CommonData.IsCheating = false;
        CommonData.GameStatus = 1;
        CommonData.IsDargMove = true;
        GameMainUIControl gameMainUI = uiMasterControl.getUIByType<GameMainUIControl>(UIEnum.GameMainUI);
        if (gameMainUI != null)
        {
            gameMainUI.startTimer();
        }
        Camera.main.gameObject.AddComponent<SecretCodeCpt>();
    }

    /// <summary>
    /// 游戏完成
    /// </summary>
    public void gameFinsh()
    {
        CommonData.GameStatus = 2;
        CommonData.IsDargMove = false;
        CommonData.IsMoveCamera = false;
        float startCameraOrthographicSize = cameraControl.startCameraOrthographicSize;
        //结束游戏时间
        GameMainUIControl gameMainUI = uiMasterControl.getUIByType<GameMainUIControl>(UIEnum.GameMainUI);
        TimeBean completeTime = null;
        if (gameMainUI != null)
        {
            gameMainUI.endTimer();
            completeTime = gameMainUI.getGameTimer();
        }

        if (!CommonData.IsCheating){
            if (CommonData.SelectPuzzlesInfo.puzzlesInfo.data_type.Equals((int)JigsawResourcesEnum.Custom))
            {

            }
            else
            {
                //增加PP
                ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).increaseUserPuzzlesPoint(CommonData.SelectPuzzlesInfo.puzzlesInfo.level * CommonData.SelectPuzzlesInfo.puzzlesInfo.level);
            }
            //保存数据
            GameUtil.FinshSaveCompleteData(CommonData.SelectPuzzlesInfo, completeTime);
        }
    
        //镜头移动
        cameraControl.transform.DOMove(cameraControl.startCameraPosition, gameFinshAnimTime);
        Tween cameraTW = DOTween
            .To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, startCameraOrthographicSize, gameFinshAnimTime)
            .OnComplete(
            delegate () {
                if (CommonData.SelectPuzzlesInfo.puzzlesInfo.data_type.Equals((int)JigsawResourcesEnum.Custom))
                {
                    SceneUtil.jumpMainScene();
                }
                else
                {
                    int leaderType = 0;
                    if (CommonData.IsCheating)
                    {
                        leaderType = 1;
                    }
                    DialogManager
                    .createLeaderBoradDialog(leaderType, CommonData.SelectPuzzlesInfo)
                    .setUserScore(completeTime.totalSeconds)
                    .setCallBack(this)
                    .setCancelButtonStr(CommonData.getText(21))
                    .setSubmitButtonStr(CommonData.getText(23));
                }
                CommonData.IsCheating = false;
            });
        //图像归位
        int containerListSize = containerList.Count;
        for (int i = 0; i < containerListSize; i++)
        {
            GameObject container = containerList[i];
            if (container != null)
            {
                JigsawContainerCpt containerCpt = container.GetComponent<JigsawContainerCpt>();
                Rigidbody2D containerRB = container.GetComponent<Rigidbody2D>();
                containerRB.Sleep();

                container.transform.DORotate(new Vector3(containerCpt.startRotation.x, containerCpt.startRotation.y), gameFinshAnimTime);
                container.transform.DOMove(containerCpt.startPosition, gameFinshAnimTime);
                return;
            }
        }
       

    }

    public void cancelOnClick()
    {
        SceneUtil.jumpMainScene();
    }

    public void submitOnClick()
    {
        SceneUtil.jumpGameScene();
    }
}
