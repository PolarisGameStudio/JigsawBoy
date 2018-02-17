using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            circleVertices.Add(new Vector3(coordinateX, coordinateY, 0));
        }

        return circleVertices;
    }

}
