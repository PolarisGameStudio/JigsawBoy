using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;

public class GameCameraControlCpt : BaseMonoBehaviour
{
    //鼠标滚轮操作关键词
    private static string MouseScrollWheel = "Mouse ScrollWheel";

    //镜头放大最大值
    public float zoomOutMax = 50f;
    //镜头放大增加量
    public float zoomOutMaxAdd = 5f;


    //镜头缩小最小值
    public float zoomInMax = 5f;
    //镜头缩小增加量
    public float zoomInMaxAdd = 5f;

    //镜头移动横向最大距离
    public float cameraMoveWithMax = 0;
    //镜头移动纵向最大距离
    public float cameraMoveHighMax = 0;
    //镜头移动缩放比例
    private float cameraMoveScale = 2f;

    //镜头抖动时间
    public float shakeDuration = 0.5f;
    //镜头抖动强度
    public float shakeStrength = 0.5f;
    //镜头抖动值
    public int shakeVibrato = 10;
    //镜头抖动随机值
    public float shakeRandomness = 10;

    //镜头移动增量
    private float cameraMoveAdd = 1f;
    //游戏镜头
    private Camera gameCamera;
    //右键起始点
    private Vector3 vecStart;
    //镜头是否移动中
    private bool isMove = false;

    //镜头初始缩放大小
    public float startCameraOrthographicSize;
    //镜头初始位置
    public Vector3 startCameraPosition;
    void Start()
    {
        CommonData.IsMoveCamera = true;
        gameCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!CommonData.IsMoveCamera)
            return;
        if (gameCamera == null)
        {
            LogUtil.log("没有游戏镜头");
            return;
        }
        //Zoom out
        if (Input.GetAxis(MouseScrollWheel) < 0)
        {
            if (Camera.main.orthographicSize <= zoomOutMax)
                Camera.main.orthographicSize += zoomOutMaxAdd;
        }
        //Zoom in
        if (Input.GetAxis(MouseScrollWheel) > 0)
        {
            if (Camera.main.orthographicSize >= zoomInMax && Camera.main.orthographicSize> zoomInMax)
                Camera.main.orthographicSize -= zoomInMaxAdd;
        }
        //CameraMove
        if (Input.GetKey(KeyCode.A))
        {
            moveCamera(new Vector2(-cameraMoveAdd, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveCamera(new Vector2(cameraMoveAdd, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveCamera(new Vector2(0, cameraMoveAdd));
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveCamera(new Vector2(0, -cameraMoveAdd));
        }

        if (Input.GetMouseButtonUp(1))
        {
            onMouseUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            onMouseDown();
        }
        if (isMove)
        {
            onMouseDrag();
        }
    }

    /// <summary>
    /// 设置镜头的缩放大小
    /// </summary>
    /// <param name="orthographicSize"></param>
    public void setCameraOrthographicSize(float orthographicSize)
    {
        Camera.main.orthographicSize = orthographicSize;
    }

    /// <summary>
    /// 镜头移动
    /// </summary>
    /// <param name="move"></param>
    public void moveCamera(Vector3 move)
    {
        if (gameCamera == null)
            return;

        float moveAfterX = transform.position.x + move.x;
        float moveAfterY = transform.position.y + move.y;

        if (Mathf.Abs(moveAfterX) >= ((cameraMoveWithMax / 2f) * cameraMoveScale))
        {
            move.x = 0f;
        }
        if (Mathf.Abs(moveAfterY) >= ((cameraMoveHighMax / 2f) * cameraMoveScale))
        {
            move.y = 0f;
        }
        gameCamera.transform.Translate(move);

    }

    /// <summary>
    /// 抖动镜头
    /// </summary>
    public void shakeCamera()
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
    }


    /// <summary>
    /// 当鼠标按下时触发(其实就是初始化_vec3Offset值，需要注意的是一切的位置坐标都是为了得到这个差值)
    /// </summary>
    private void onMouseDown()
    {
        isMove = true;
        //获取鼠标相对于摄像头的点击位置
        vecStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// 鼠标抬起时
    /// </summary>
    private void onMouseUp()
    {
        isMove = false;
    }

    /// <summary>
    /// 在用户拖拽GUI元素或碰撞提的时候调用，在鼠标按下的每一帧被调用
    /// </summary>
    private void onMouseDrag()
    {
        if (vecStart == null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveOffset = vecStart - mousePos;
        moveCamera(moveOffset);

    }
}

