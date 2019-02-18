using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJigsawControlCpt : BaseMonoBehaviour
{

    //位置差
    private Vector3 vec3Offset;
    //是否选中物体
    private bool isSelect;
    //选中的拼图容器
    private JigsawContainerCpt jigsawContainerCpt;
    //选中的物体
    private RaycastHit2D hitRC;

    //从中心点出发可以移动的高宽最远距离
    public float moveWithMax = 0f;
    public float moveHighMax = 0f;

    private float moveScale = 2f;

    //选择物体增量
    public float rotateObjAngleAdd = 365;

    public GameParticleControl gameParticleControl;

    // Use this for initialization
    void Start()
    {
        CommonData.IsDargMove = false;
        isSelect = false;
        //获取镜头控制
        GameObject cameraObj = Camera.main.gameObject;
        if (cameraObj != null)
        {
            gameParticleControl = cameraObj.GetComponent<GameParticleControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CommonData.IsDargMove)
            return;
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
        if(isSelect&& Input.GetMouseButton(1))
        {
            rotateObject(RotationDirectionEnum.Clockwise);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotateObject(RotationDirectionEnum.Anticlockwise);
        }
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
        {
            rotateObject(RotationDirectionEnum.Clockwise);
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
        if (hitRC.collider != null && hitRC.transform.GetComponent<JigsawContainerCpt>() != null)
        {
            Collider2D jigsawCollider = hitRC.collider;
            Transform jigsawTransform = jigsawCollider.transform;
            //让被选中的物体停止移动
            Rigidbody2D baseRB = jigsawTransform.GetComponent<Rigidbody2D>();
            baseRB.velocity = Vector3.zero;
            baseRB.constraints = RigidbodyConstraints2D.FreezeAll;
            baseRB.constraints = RigidbodyConstraints2D.None;
            //在鼠标按下时，鼠标和物体在控件坐标在空间上的位置差    
            jigsawContainerCpt = jigsawTransform.GetComponent<JigsawContainerCpt>();
            if (jigsawContainerCpt != null)
            {
                isSelect = true;
                vec3Offset = jigsawTransform.position - new Vector3(mousePos.x, mousePos.y);
                jigsawContainerCpt.setIsSelect(true);
            }
        }
    }

    /// <summary>
    /// 鼠标抬起时
    /// </summary>
    private void onMouseUp()
    {
        isSelect = false;
        if (jigsawContainerCpt != null)
        {
            jigsawContainerCpt.setIsSelect(false);
        }
    }


    /// <summary>
    /// 在用户拖拽GUI元素或碰撞提的时候调用，在鼠标按下的每一帧被调用
    /// </summary>
    private void onMouseDrag()
    {
        if (hitRC == false)
            return;
        Collider2D jigsawCollider = hitRC.collider;
        GameObject jigsawGameObj = jigsawCollider.gameObject;
        Transform jigsawTransform = jigsawGameObj.transform;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //物体速度位置是这一帧鼠标在空间的位置加上它们的差值
        if (jigsawTransform != null)
        {
            Vector3 movePos = new Vector3(jigsawTransform.position.x, jigsawTransform.position.y, jigsawTransform.position.z);
            Vector3 nowOffset = new Vector3(vec3Offset.x, vec3Offset.y, vec3Offset.z);
            movePos.x = mousePos.x;
            movePos.y = mousePos.y;
            //if (Mathf.Abs(mousePos.x) <= ((moveWithMax / 2f) * moveScale))
            //{
            //    movePos.x = mousePos.x;
            //}
            //else
            //{
            //    nowOffset.x = 0f;
            //}
            //if (Mathf.Abs(mousePos.y) <= ((moveHighMax / 2f) * moveScale))
            //{
            //    movePos.y = mousePos.y;
            //}
            //else
            //{
            //    nowOffset.y = 0f;
            //}
            jigsawTransform.position = Vector3.Lerp(jigsawTransform.position, movePos + nowOffset, 20f * Time.deltaTime);
            // jigsawTransform.position = movePos + nowOffset;
        }
    }

    /// <summary>
    /// 选择物体
    /// </summary>
    /// <param name="rotationDirection"></param>
    private void rotateObject(RotationDirectionEnum rotationDirection)
    {
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currMousePos2D = new Vector2(currMousePos.x, currMousePos.y);
        Collider2D jigsawCollider = null;
        if (isSelect)
        {
            jigsawCollider = hitRC.collider;
        }
        else
        {
            RaycastHit2D currRH = Physics2D.Raycast(currMousePos2D, Vector2.zero);
            if (currRH)
                jigsawCollider = currRH.collider;
        }
        if (jigsawCollider == null)
            return;
        GameObject jigsawGameObj = jigsawCollider.gameObject;
        if (jigsawGameObj.GetComponent<JigsawContainerCpt>() == null)
            return;
        Transform jigsawTransform = jigsawGameObj.transform;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Transform[] childTF = jigsawTransform.GetComponentsInChildren<Transform>();

        if (childTF != null && childTF.Length > 0)
        {
            if (rotationDirection.Equals(RotationDirectionEnum.Clockwise))
            {
                jigsawTransform.Rotate(0, 0, -rotateObjAngleAdd * Time.deltaTime);
            }
            else if (rotationDirection.Equals(RotationDirectionEnum.Anticlockwise))
            {

                jigsawTransform.Rotate(0, 0, rotateObjAngleAdd * Time.deltaTime);
            }
        }
    }
}
