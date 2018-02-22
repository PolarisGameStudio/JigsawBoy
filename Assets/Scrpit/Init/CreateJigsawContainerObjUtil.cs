using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJigsawContainerObjUtil {


    /// <summary>
    /// 创建拼图容器对象
    /// </summary>
    /// <returns></returns>
    public static GameObject createJigsawContainerObj()
    {
        GameObject jigsawGameObj = createJigsawContainerObj(null);
        return jigsawGameObj;
    }


    /// <summary>
    /// 创建拼图容器对象
    /// </summary>
    /// <param name="jigsawData"></param>
    /// <returns></returns>
    public static GameObject createJigsawContainerObj(JigsawBean jigsawData)
    {
        GameObject jigsawGameObj = JigsawContainerGameObjBuilder.buildJigsawContainerObj();
        JigsawContainerCpt containerCpt= jigsawGameObj.GetComponent<JigsawContainerCpt>();
        if (jigsawData != null)
            containerCpt.addJigsaw(jigsawData); 
        return jigsawGameObj;
    }


    /// <summary>
    /// 创建拼图容器对象集
    /// </summary>
    /// <param name="jigsawData"></param>
    /// <returns></returns>
    public static List<GameObject> createJigsawContainerObjList(List<JigsawBean> jigsawData)
    {
        List<GameObject> listJigsawContainer = new List<GameObject>();
        foreach (JigsawBean itemData in jigsawData)
        {
            GameObject jigsawGameObj = createJigsawContainerObj(itemData);
            listJigsawContainer.Add(jigsawGameObj);
        }
        return listJigsawContainer;
    }
}
