using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMoveCpt : MonoBehaviour
{

    //位置差
    Vector3 vec3Offset;

    bool isSelect;


    /// <summary>
    /// 选中的物体
    /// </summary>
    private RaycastHit2D hitRC;


    // Use this for initialization
    void Start()
    {
        isSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp();
        }

        if (isSelect)
        {
            onMouseDrag();
        }
    }


    /// <summary>
    /// 当鼠标按下时触发(其实就是初始化_vec3Offset值，需要注意的是一切的位置坐标都是为了得到这个差值)
    /// </summary>
    private void onMouseDown()
    {
        //获取鼠标相对于摄像头的点击位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        hitRC = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hitRC.collider != null)
        {
            isSelect = true;
            Collider2D jigsawCollider = hitRC.collider;
            GameObject jigsawGameObj = jigsawCollider.gameObject;
            Transform jigsawTransform = jigsawGameObj.transform;
            //获取父对象
            Transform jigsawContainer = jigsawTransform.parent;
            //在鼠标按下时，鼠标和物体在控件坐标在空间上的位置差
            if (jigsawContainer != null)
                vec3Offset = jigsawContainer.position - new Vector3(mousePos.x, mousePos.y);
        }

    }

    /// <summary>
    /// 鼠标抬起时
    /// </summary>
    private void onMouseUp()
    {
        isSelect = false;
    }


    /// <summary>
    /// 在用户拖拽GUI元素或碰撞提的时候调用，在鼠标按下的每一帧被调用
    /// </summary>
    private void onMouseDrag()
    {
        if (hitRC == null)
            return;
        Collider2D jigsawCollider = hitRC.collider;
        GameObject jigsawGameObj = jigsawCollider.gameObject;
        Transform jigsawTransform = jigsawGameObj.transform;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //获取父对象
        Transform jigsawContainer = jigsawTransform.parent;
        //物体速度位置是这一帧鼠标在空间的位置加上它们的差值
        if(jigsawContainer!=null)
        jigsawContainer.position = new Vector3(mousePos.x, mousePos.y) + vec3Offset;
    }
}
