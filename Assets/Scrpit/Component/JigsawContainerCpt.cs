using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class JigsawContainerCpt : BaseMonoBehaviour
{
    //容器所含拼图对象数据
    public List<JigsawBean> listJigsaw;
    //是否开启合并检测
    public bool isOpenMergeCheck;

    //合并的判断间距
    public float mergeVectorOffset;
    public float mergeAnglesOffset;
    //合并动画持续时间
    public float mergeAnimDuration;

    //是否被选中
    private bool isSelect;
    public JigsawContainerCpt()
    {
        isOpenMergeCheck = true;
        listJigsaw = new List<JigsawBean>();
        isSelect = false;
        mergeVectorOffset = 1.5f;
        mergeAnglesOffset = 45;
        mergeAnimDuration = 0.2f;
    }

    /// <summary>
    /// 添加拼图块
    /// </summary>
    /// <param name="jigsawData"></param>
    public void addJigsaw(JigsawBean jigsawData)
    {
        if (jigsawData == null)
        {
            LogUtil.logError("给拼图容器添加子拼图失败：没有JigsawBean");
            return;
        }
        GameObject jigsawGameObj = jigsawData.JigsawGameObj;
        if (jigsawGameObj == null)
        {
            LogUtil.logError("给拼图容器添加子拼图失败：没有JigsawGameObj");
            return;
        }
        jigsawGameObj.transform.parent = null;
        jigsawGameObj.transform.parent = transform;
        listJigsaw.Add(jigsawData);
        //设置质量为拼图数量和
        Rigidbody2D thisRB = gameObject.GetComponent<Rigidbody2D>();
        if (thisRB != null)
            thisRB.mass = listJigsaw.Count;
    }

    /// <summary>
    /// 添加拼图块
    /// </summary>
    /// <param name="jigsawData"></param>
    public void addJigsawList(List<JigsawBean> jigsawDataList)
    {
        foreach (JigsawBean itemData in jigsawDataList)
        {
            addJigsaw(itemData);
        }
    }

    /// <summary>
    /// 位置纠正
    /// </summary>
    public void jigsawLocationCorrect()
    {
        int jigsawListCount = listJigsaw.Count;
        if (jigsawListCount == 0)
            return;
        JigsawBean baseJigsawItem = listJigsaw[0];
        Transform baseTF = baseJigsawItem.JigsawGameObj.transform;
        //获取基准拼图的标记位
        Vector2 baseMarkLocation = baseJigsawItem.MarkLocation;
        //获取基准拼图的世界坐标
        Vector3 basePosition = baseTF.position;
        //获取基准拼图的本地坐标
        Vector3 baseLocationPosition = baseTF.InverseTransformPoint(basePosition);

        for (int listPosition = 1; listPosition < jigsawListCount; listPosition++)
        {
            //获取其他拼图数据
            JigsawBean jigsawItem = listJigsaw[listPosition];
            //获取其他拼图的标记坐标
            Vector2 itemMarkLocation = jigsawItem.MarkLocation;
            //获取其他拼图的对象
            Transform jigsawTF = jigsawItem.JigsawGameObj.transform;
            //获取相对于基准拼图的标记偏移量
            Vector2 offsetMarkLocation = baseMarkLocation - itemMarkLocation;
            //获取相对于基准拼图的本地位置偏移量
            Vector3 offsetPosition = new Vector2(offsetMarkLocation.x * jigsawItem.JigsawWith, offsetMarkLocation.y * jigsawItem.JigsawHigh);
            //获取其他拼图的本地位置
            Vector3 jigsawItemLocationPosition = baseLocationPosition - offsetPosition;
            //获取其他拼图的世界坐标
            Vector3 jigsawItemPosition = baseTF.TransformPoint(jigsawItemLocationPosition);


            //设置位置
           // jigsawTF.DOMove(jigsawItemPosition, mergeAnimDuration);
           // jigsawTF.DORotate(transform.rotation.eulerAngles, mergeAnimDuration);
            jigsawTF.position = jigsawItemPosition;
            jigsawTF.rotation = transform.rotation;

        }
    }

    /// <summary>
    /// 获取容器下所有拼图对象列表 
    /// </summary>
    public List<JigsawBean> getJigsawList()
    {
        return listJigsaw;
    }

    /// <summary>
    /// 设置合并检测状态
    /// </summary>
    /// <param name="openStatus"></param>
    public void setMergeCheck(bool openStatus)
    {
        isOpenMergeCheck = openStatus;
    }

    /// <summary>
    /// 设置层级
    /// </summary>
    /// <param name="sortingOrder"></param>
    public void setSortingOrder(int sortingOrder)
    {
        Renderer[] jigsawRenderList= GetComponentsInChildren<Renderer>();
        int jigsawRenderListSize = jigsawRenderList.Length;
        for (int jigsawRenderPosition = 0; jigsawRenderPosition < jigsawRenderListSize; jigsawRenderPosition++)
        {
            Renderer itemRender= jigsawRenderList[jigsawRenderPosition];
            itemRender.sortingOrder = sortingOrder;
        };
    }

    /// <summary>
    ///  设置是trigger
    /// </summary>
    /// <param name="isTrigger"></param>
    public void setIsTrigger(bool isTrigger) {
        CompositeCollider2D collider= GetComponent<CompositeCollider2D>();
        collider.isTrigger = isTrigger;
    }

    /// <summary>
    /// 设置是否选中
    /// </summary>
    /// <param name="isSelect"></param>
    public void setIsSelect(bool isSelect)
    {
        this.isSelect = isSelect;
        //设置碰撞
        setIsTrigger(isSelect);
        //设置层级
        int sortingOrder;
        if (isSelect)
            sortingOrder = 32767;
        else
            sortingOrder= DevUtil.getRandomInt(0, 30000);
        setSortingOrder(sortingOrder);
    }

    /// <summary>
    /// 获取该拼图附近能合并的点坐标
    /// </summary>
    public Dictionary<Vector2, Vector3> getMergeVectorList()
    {
        Dictionary<Vector2, Vector3> mapMergeList = new Dictionary<Vector2, Vector3>();
        int listJigsawCount = listJigsaw.Count;
        for (int jigsawPosition = 0; jigsawPosition < listJigsawCount; jigsawPosition++)
        {
            JigsawBean jigsawData = listJigsaw[jigsawPosition];
            GameObject jigsawObj = jigsawData.JigsawGameObj;
            Transform jigsawTF = jigsawObj.transform;
            Vector3 jigsawLocation = jigsawTF.InverseTransformPoint(jigsawTF.position);

            Vector2 leftMarkLocation = new Vector2(jigsawData.MarkLocation.x - 1, jigsawData.MarkLocation.y);
            Vector3 leftVector = jigsawLocation + Vector3.left * jigsawData.JigsawWith;

            Vector2 aboveMarkLocation = new Vector2(jigsawData.MarkLocation.x, jigsawData.MarkLocation.y + 1);
            Vector3 aboveVector = jigsawLocation + Vector3.up * jigsawData.JigsawHigh;

            Vector2 rightMarkLocation = new Vector2(jigsawData.MarkLocation.x + 1, jigsawData.MarkLocation.y);
            Vector3 rightVector = jigsawLocation + Vector3.right * jigsawData.JigsawWith;

            Vector2 belowMarkLocation = new Vector2(jigsawData.MarkLocation.x, jigsawData.MarkLocation.y - 1);
            Vector3 belowVector = jigsawLocation + Vector3.down * jigsawData.JigsawHigh;

            if (!mapMergeList.ContainsKey(leftMarkLocation))
            {
                mapMergeList.Add(leftMarkLocation, jigsawTF.TransformPoint(leftVector));
            }
            if (!mapMergeList.ContainsKey(aboveMarkLocation))
            {
                mapMergeList.Add(aboveMarkLocation, jigsawTF.TransformPoint(aboveVector));
            }
            if (!mapMergeList.ContainsKey(rightMarkLocation))
            {
                mapMergeList.Add(rightMarkLocation, jigsawTF.TransformPoint(rightVector));
            }
            if (!mapMergeList.ContainsKey(belowMarkLocation))
            {
                mapMergeList.Add(belowMarkLocation, jigsawTF.TransformPoint(belowVector));
            }
        }

        return mapMergeList;
    }

    /// <summary>
    /// 获取所有拼图坐标
    /// </summary>
    /// <returns></returns>
    public Dictionary<Vector2, Vector3> getJigsawPositionVectorList()
    {
        Dictionary<Vector2, Vector3> mapJigsawPositionVectorList = new Dictionary<Vector2, Vector3>();
        int listJigsawCount = listJigsaw.Count;
        for (int jigsawPosition = 0; jigsawPosition < listJigsawCount; jigsawPosition++)
        {
            JigsawBean jigsawData = listJigsaw[jigsawPosition];
            GameObject jigsawObj = jigsawData.JigsawGameObj;
            Transform jigsawTF = jigsawObj.transform;
            Vector3 jigsawLocation = jigsawTF.position;

            if (!mapJigsawPositionVectorList.ContainsKey(jigsawData.MarkLocation))
            {
                mapJigsawPositionVectorList.Add(jigsawData.MarkLocation, jigsawLocation);
            }
        }

        return mapJigsawPositionVectorList;
    }
    /// <summary>
    /// 检测是否能合并
    /// </summary>
    public bool checkMerge(JigsawContainerCpt collisionJCC)
    {
        Dictionary<Vector2, Vector3> collisionMergeVectorList = collisionJCC.getMergeVectorList();
        Dictionary<Vector2, Vector3> jigsawPositionVectorList = this.getJigsawPositionVectorList();
        float thisAngles = transform.eulerAngles.z;
        float collisionAngles = collisionJCC.transform.eulerAngles.z;
        float offsetAngles = Mathf.Abs(thisAngles - collisionAngles);
        foreach (KeyValuePair<Vector2, Vector3> jigsawPositionItem in jigsawPositionVectorList)
        {
            foreach (KeyValuePair<Vector2, Vector3> collisionMergeItem in collisionMergeVectorList)
            {
                if (jigsawPositionItem.Key.Equals(collisionMergeItem.Key))
                {
                    float distance = Vector3.Distance(jigsawPositionItem.Value, collisionMergeItem.Value);
                    if (distance < mergeVectorOffset && offsetAngles < mergeAnglesOffset)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------

    //镜头控制
    public GameCameraControlCpt mCameraControlCpt;

    private void Start()
    {
        //获取镜头控制
        GameObject cameraObj =GameObject.Find("/Main Camera");
        if (cameraObj != null)
            mCameraControlCpt = cameraObj.GetComponent<GameCameraControlCpt>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionCheck(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collisionCheck(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionCheck(collision);
    }


    /// <summary>
    /// 碰撞处理
    /// </summary>
    /// <param name="collision"></param>
    private void collisionCheck(Collider2D collision)
    {

        if (!isOpenMergeCheck)
            return;
        if (!isSelect)
            return;

        //获取被撞物体和其父对象
        JigsawContainerCpt collisionJCC = collision.gameObject.GetComponent<JigsawContainerCpt>();
        if (collisionJCC == null)
            return;
        if (checkMerge(collisionJCC))
        {
            //设置不可在拖拽
            CommonData.isDargMove = false;
            isSelect = false;
            // 添加拼图碎片到碰撞容器里
            collisionJCC.addJigsawList(listJigsaw);
            collisionJCC.jigsawLocationCorrect();
            //摇晃镜头
            shakeCamer();
            // 最后删除当前容器
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 抖动镜头
    /// </summary>
    private void shakeCamer()
    {
        if (mCameraControlCpt != null)
            mCameraControlCpt.shakeCamera();
    }
}
