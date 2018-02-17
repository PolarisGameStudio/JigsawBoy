using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JigsawContainerCpt : MonoBehaviour
{
    //容器所含拼图对象数据
    public List<JigsawBean> listJigsaw;
    //是否被选中
    public bool isSelect;
    //是否开启合并检测
    public bool isOpenMergeCheck;

    //合并的判断间距
    public float mergeVectorOffset;
    public float mergeAnglesOffset;


    public JigsawContainerCpt()
    {
        isOpenMergeCheck = true;
        listJigsaw = new List<JigsawBean>();
        isSelect = false;
        mergeVectorOffset = 1f;
        mergeAnglesOffset = 10;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
    /// 碰撞开始
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
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
            // 添加拼图碎片到碰撞容器里
            collisionJCC.addJigsawList(getJigsawList());
            // 最后删除当前容器
            Destroy(gameObject);
        }
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
            Vector3 jigsawLocation = jigsawTF.localPosition ;

            Vector2 leftMarkLocation = new Vector2(jigsawData.MarkLocation.x - 1, jigsawData.MarkLocation.y);
          //  Vector3 leftVector =  new Vector3(jigsawLocation.x - jigsawData.JigsawWith, jigsawLocation.y, jigsawLocation.z);
            Vector3 leftVector = jigsawLocation + Vector3.left * jigsawData.JigsawWith;

            Vector2 aboveMarkLocation = new Vector2(jigsawData.MarkLocation.x, jigsawData.MarkLocation.y + 1);
            //Vector3 aboveVector = new Vector3(jigsawLocation.x, jigsawLocation.y + jigsawData.JigsawHigh, jigsawLocation.z);
            Vector3 aboveVector = jigsawLocation + Vector3.up * jigsawData.JigsawWith;

            Vector2 rightMarkLocation = new Vector2(jigsawData.MarkLocation.x + 1, jigsawData.MarkLocation.y);
            //Vector3 rightVector = new Vector3(jigsawLocation.x + jigsawData.JigsawWith, jigsawLocation.y, jigsawLocation.z);
            Vector3 rightVector = jigsawLocation + Vector3.right * jigsawData.JigsawWith;

            Vector2 belowMarkLocation = new Vector2(jigsawData.MarkLocation.x, jigsawData.MarkLocation.y - 1);
            //Vector3 belowVector = new Vector3(jigsawLocation.x, jigsawLocation.y - jigsawData.JigsawHigh, jigsawLocation.z);
            Vector3 belowVector = jigsawLocation + Vector3.down * jigsawData.JigsawWith;

            if (!mapMergeList.ContainsKey(leftMarkLocation))
            {
                mapMergeList.Add(leftMarkLocation, jigsawTF.TransformPoint(leftVector));
            }
            if (!mapMergeList.ContainsKey(aboveMarkLocation))
            {
                mapMergeList.Add(aboveMarkLocation, jigsawTF.TransformPoint(aboveVector) );
            }
            if (!mapMergeList.ContainsKey(rightMarkLocation))
            {
                mapMergeList.Add(rightMarkLocation, jigsawTF.TransformPoint(rightVector) );
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

            mapJigsawPositionVectorList.Add(jigsawData.MarkLocation, jigsawLocation);
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
                    if (distance < mergeVectorOffset&& offsetAngles< mergeAnglesOffset)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
