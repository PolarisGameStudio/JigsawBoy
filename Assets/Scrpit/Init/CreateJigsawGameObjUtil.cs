using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJigsawGameObjUtil {


    /// <summary>
    /// 创建拼图对象
    /// </summary>
    /// <param name="jigsawData"></param>
    /// <returns></returns>
    public static GameObject createJigsawGameObj(JigsawBean jigsawData)
    {
        GameObject jigsawGameObj=  JigsawObjBuilder.buildJigsawGameObj(jigsawData);
        jigsawData.JigsawGameObj = jigsawGameObj;
        return jigsawGameObj;
    }

    /// <summary>
    /// 创建拼图对象集合
    /// </summary>
    /// <param name="listJigsawData"></param>
    /// <returns></returns>
    public static List<GameObject> createJigsawGameObjList(List<JigsawBean> listJigsawData)
    {
        List<GameObject> listJigsawGameObj = new List<GameObject>();
        if (listJigsawData == null)
        {
            LogUtil.logError("拼图数据集合为空");
            return listJigsawGameObj;
        }
           
        foreach (JigsawBean itemBean in listJigsawData)
        {
            GameObject itemObj= createJigsawGameObj(itemBean);
            listJigsawGameObj.Add(itemObj);
        }
        return listJigsawGameObj;
    }
}
