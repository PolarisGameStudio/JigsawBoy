using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJigsawGameObjUtil {


    /// <summary>
    /// 创建拼图对象
    /// </summary>
    /// <param name="jigsawData"></param>
    /// <returns></returns>
    public static GameObject createJigsawGameObj(JigsawBean jigsawData, Texture2D jigsawPic)
    {
        GameObject jigsawGameObj=  JigsawObjBuilder.buildJigsawGameObj(jigsawData, jigsawPic);
        jigsawData.JigsawGameObj = jigsawGameObj;
        return jigsawGameObj;
    }

    /// <summary>
    /// 创建拼图对象集合
    /// </summary>
    /// <param name="listJigsawData"></param>
    /// <returns></returns>
    public static List<GameObject> createJigsawGameObjList(List<JigsawBean> listJigsawData, Texture2D jigsawPic)
    {
        List<GameObject> listJigsawGameObj = new List<GameObject>();
        if (listJigsawData == null)
        {
            LogUtil.logError("拼图数据集合为空");
            return listJigsawGameObj;
        }
           
        foreach (JigsawBean itemBean in listJigsawData)
        {
            GameObject itemObj= createJigsawGameObj(itemBean,jigsawPic);
            listJigsawGameObj.Add(itemObj);
        }
        return listJigsawGameObj;
    }
}
