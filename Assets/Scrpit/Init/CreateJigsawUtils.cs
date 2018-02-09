using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CreateJigsawUtils
{


    /// <summary>
    /// 创建拼图碎片合集
    /// </summary>
    /// <param name="jigsawStyle">拼图样式</param>
    /// <param name="horizontalJigsawNumber">横向拼图块数</param>
    /// <param name="verticalJigsawNumber">纵向拼图快速</param>
    /// <param name="jigsawPic">拼图图片</param>
    /// <returns></returns>
    public static List<JigsawBean> createJigsawList(JigsawStyleEnum jigsawStyle, int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        if (horizontalJigsawNumber == 0)
            throw new Exception("横向块数为0");
        if (verticalJigsawNumber == 0)
            throw new Exception("纵向块数为0");
        if (jigsawPic == null)
            throw new Exception("没有图片");

        List<JigsawBean> listJigsawBean = null;

        IBaseJigsawBuilder jigsawBuilder;
        //按样式生成不同的拼图碎片
        if (jigsawStyle == JigsawStyleEnum.Normal)
            jigsawBuilder = new NomralJigsawBuilder();
        else
            throw new Exception("没有相对于样式的拼图");


        listJigsawBean = jigsawBuilder.buildJigsawList(horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);

        if (listJigsawBean == null)
            throw new Exception("生成拼图碎片失败");
        else
            return listJigsawBean;

    }


}
