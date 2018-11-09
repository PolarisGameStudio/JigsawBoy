using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeometryUtil
{

    /// <summary>
    ///  已知圆心，半径，角度，求圆上的点坐标
    /// </summary>
    /// <param name="centerVector">圆心</param>
    /// <param name="circleR">直径</param>
    /// <param name="triangleNumber">园内三角形个数</param>
    /// <param name="isClockwise">集合是否顺时针排序</param>
    /// <param name="startVectorEnum">起始点位置</param>
    /// <returns></returns>
    public static List<Vector3> getCircleVertices(Vector3 centerVector, float circleR, int triangleNumber, bool isClockwise, CircleStartVectorEnum startVectorEnum)
    {
        List<Vector3> circleVertices = new List<Vector3>();

        float circleAngleItem = 360f / triangleNumber;
        float circler = circleR / 2f;
        float startAngle = 0f;
        if (startVectorEnum == CircleStartVectorEnum.Left)
            startAngle = -180f;
        else if (startVectorEnum == CircleStartVectorEnum.Above)
            startAngle = 90f;
        else if (startVectorEnum == CircleStartVectorEnum.Right)
            startAngle = 0f;
        else if (startVectorEnum == CircleStartVectorEnum.Below)
            startAngle = -90f;
        for (int triangleposition = 0; triangleposition < triangleNumber; triangleposition++)
        {
            float circleAngleTemp = isClockwise ? (-circleAngleItem * triangleposition) : (circleAngleItem * triangleposition);
            float circleAngle = circleAngleTemp + startAngle;

            float coordinateX = centerVector.x + circler * Mathf.Cos(circleAngle * 3.14f / 180f);
            float coordinateY = centerVector.y + circler * Mathf.Sin(circleAngle * 3.14f / 180f);
            float coordinateXcpt = (float)Math.Round(coordinateX, 2);
            float coordinateYcpt = (float)Math.Round(coordinateY, 2);
            circleVertices.Add(new Vector3(coordinateXcpt, coordinateYcpt, 0));
        }
        return circleVertices;
    }

    /// <summary>
    /// 判断一点是否在三角形内
    /// </summary>
    /// <param name="p">一点</param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool VertexIsInTriangle(Vector3 _target, Vector3 _center, Vector3 _left, Vector3 _right)
    {
        Vector3 Ctl = _left - _center;
        Vector3 Ctr = _right - _center;
        Vector3 Ctt = _target - _center;
        Vector3 Ltr = _right - _left;
        Vector3 Ltc = _right - _center;
        Vector3 Ltt = _left - _target;
        Vector3 Rtl = _left - _right;
        Vector3 Rtc = _center - _right;
        Vector3 Rtt = _target - _right;
        if
            (
          Vector3.Dot(Vector3.Cross(Ctl, Ctr).normalized, Vector3.Cross(Ctl, Ctt).normalized) == -1 ||
          Vector3.Dot(Vector3.Cross(Ltr, Ltc).normalized, Vector3.Cross(Ltr, Ltt).normalized) == -1 ||
          Vector3.Dot(Vector3.Cross(Rtc, Rtl).normalized, Vector3.Cross(Rtc, Rtt).normalized) == -1
           )
            return false;
        else
            return true;
    }

}
