using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJigsawCpt : MonoBehaviour
{
    //拼图数据
    private JigsawBean jigsawData;

    //每条边的合并情况
    private JigsawMergeStatusEnum[] edgeListMergeStatus;



    public NormalJigsawCpt()
    {
        this.edgeListMergeStatus = new JigsawMergeStatusEnum[4];
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
    /// 设置边的合并状态
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="mergeStatus"></param>
    public void setEdgeMergeStatus(JigsawStyleNormalEdgeEnum edge, JigsawMergeStatusEnum mergeStatus)
    {
        if (edge.Equals(JigsawStyleNormalEdgeEnum.Left))
        {
            edgeListMergeStatus[0] = mergeStatus;
        }
        else if (edge.Equals(JigsawStyleNormalEdgeEnum.Above))
        {
            edgeListMergeStatus[1] = mergeStatus;
        }
        else if (edge.Equals(JigsawStyleNormalEdgeEnum.Right))
        {
            edgeListMergeStatus[2] = mergeStatus;
        }
        else if (edge.Equals(JigsawStyleNormalEdgeEnum.Below))
        {
            edgeListMergeStatus[3] = mergeStatus;
        }
    }

    /// <summary>
    /// 设置拼图数据
    /// </summary>
    /// <param name="jigsawData"></param>
    public  void setJigsawData(JigsawBean jigsawData)
    {
        this.jigsawData = jigsawData;
    }
    /// <summary>
    /// 获取拼图数据
    /// </summary>
    /// <returns></returns>
    public JigsawBean getJigsawData()
    {
        return jigsawData;
    }
}
