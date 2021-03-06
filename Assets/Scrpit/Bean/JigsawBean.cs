﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawBean
{
    /// <summary>
    /// 拼图对象
    /// </summary>
    private GameObject jigsawGameObj;

    /// <summary>
    /// 拼图样式
    /// </summary>
    private JigsawStyleEnum jigsawStyle;

    /// <summary>
    /// 顶点坐标
    /// </summary>
    private List<Vector3> listVertices;

    /// <summary>
    /// 中心点坐标
    /// </summary>
    private Vector3 centerVector;

    /// <summary>
    /// 拼图宽高
    /// </summary>
    private float jigsawWith;
    private float jigsawHigh;

    /// <summary>
    /// UV的框和高
    /// </summary>
    private float jigsawUVWith;
    private float jigsawUVHigh;
    
    /// <summary>
    /// 图片UV坐标点集合
    /// </summary>
    private List<Vector2> listUVposition;

    /// <summary>
    /// 相对于图片的XY的标记
    /// </summary>
    private Vector2 markLocation;

    /// <summary>
    /// 所有边对应的凹凸情况
    /// </summary>
    private JigsawBulgeEnum[] listBulge;

    /// <summary>
    /// 拼图图片路径
    /// </summary>
    private string sourcePicPath;

    /// <summary>
    /// 拼图块数
    /// </summary>
    private int jigsawNumber;


    public GameObject JigsawGameObj
    {
        get
        {
            return jigsawGameObj;
        }

        set
        {
            jigsawGameObj = value;
        }
    }

    public JigsawStyleEnum JigsawStyle
    {
        get
        {
            return jigsawStyle;
        }

        set
        {
            jigsawStyle = value;
        }
    }

    public List<Vector3> ListVertices
    {
        get
        {
            return listVertices;
        }

        set
        {
            listVertices = value;
        }
    }

    public List<Vector2> ListUVposition
    {
        get
        {
            return listUVposition;
        }

        set
        {
            listUVposition = value;
        }
    }

    public Vector2 MarkLocation
    {
        get
        {
            return markLocation;
        }

        set
        {
            markLocation = value;
        }
    }

    public JigsawBulgeEnum[] ListBulge
    {
        get
        {
            return listBulge;
        }

        set
        {
            listBulge = value;
        }
    }
 


    public float JigsawWith
    {
        get
        {
            return jigsawWith;
        }

        set
        {
            jigsawWith = value;
        }
    }

    public float JigsawHigh
    {
        get
        {
            return jigsawHigh;
        }

        set
        {
            jigsawHigh = value;
        }
    }

    public Vector3 CenterVector
    {
        get
        {
            return centerVector;
        }

        set
        {
            centerVector = value;
        }
    }

    public string SourcePicPath
    {
        get
        {
            return sourcePicPath;
        }

        set
        {
            sourcePicPath = value;
        }
    }

    public float JigsawUVWith
    {
        get
        {
            return jigsawUVWith;
        }

        set
        {
            jigsawUVWith = value;
        }
    }

    public float JigsawUVHigh
    {
        get
        {
            return jigsawUVHigh;
        }

        set
        {
            jigsawUVHigh = value;
        }
    }

    public int JigsawNumber
    {
        get
        {
            return jigsawNumber;
        }

        set
        {
            jigsawNumber = value;
        }
    }
}
