using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CreateJigsawDataUtils
{


    /// <summary>
    /// 创建拼图碎片合集
    /// </summary>
    /// <param name="jigsawStyle">拼图样式</param>
    /// <param name="horizontalJigsawNumber">横向拼图块数</param>
    /// <param name="verticalJigsawNumber">纵向拼图快速</param>
    /// <param name="jigsawPic">拼图图片</param>
    /// <returns></returns>
    public static List<JigsawBean> createJigsawDataList(JigsawStyleEnum jigsawStyle, int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        List<JigsawBean> listJigsawBean = new List<JigsawBean>();

        if (horizontalJigsawNumber == 0) {
            LogUtil.logError("横向块数为0");
            return listJigsawBean;
        }
        if (verticalJigsawNumber == 0)
        {
            LogUtil.logError("纵向块数为0");
            return listJigsawBean;
        }
        if (jigsawPic == null)
        {
            LogUtil.logError("没有图片");
            return listJigsawBean;
        }

        //按样式生成不同的拼图碎片
        IBaseJigsawBuilder jigsawBuilder;
        if (jigsawStyle == JigsawStyleEnum.Normal)
            jigsawBuilder = new NomralJigsawBuilder();
        else
        {
            LogUtil.logError("没有相对于样式的拼图");
            return listJigsawBean;
        }

        listJigsawBean = jigsawBuilder.buildJigsawList(horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);

        if (listJigsawBean == null)
        {
            LogUtil.logError("生成拼图碎片数据失败");
        }
            return listJigsawBean;

    }


}
