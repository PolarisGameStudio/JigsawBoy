using System.Collections.Generic;
using UnityEngine;

public class CreateGameWallUtil 
{
    public static Vector3 wallCenter = new Vector3(0, 0, 0);
    //墙厚度
    public static float wallThick = 1000f;
    //墙缩放大小
    public static float wallScale = 2f;

    public static void createWall(GameWallEnum gameWallEnum,float picAllW, float picAllH)
    {
        if (gameWallEnum.Equals(GameWallEnum.Def))
        {
            createDefWall(picAllW, picAllH);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Circle))
        {
            createCircleWall(picAllW, picAllH);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Square))
        {
            createSquareWall(picAllW, picAllH);
        }
    }

    /// <summary>
    /// 创建自适应围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    private static void createDefWall( float picAllW, float picAllH)
    {
        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;

        GameObject gameWall = new GameObject("GameWall");

        GameObject leftWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        leftWall.name = "LeftWall";
        leftWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        leftWall.transform.position = new Vector3(-(wallWith / 2f + wallThick / 2f), 0f, 0f);
        leftWall.transform.parent = gameWall.transform;

        GameObject rightWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        rightWall.name = "RightWall";
        rightWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        rightWall.transform.position = new Vector3((wallWith / 2f + wallThick / 2f), 0f, 0f);
        rightWall.transform.parent = gameWall.transform;

        GameObject aboveWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        aboveWall.name = "AboveWall";
        aboveWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        aboveWall.transform.position = new Vector3(0f, (wallHigh / 2f + wallThick / 2f), 0f);
        aboveWall.transform.parent = gameWall.transform;

        GameObject belowWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        belowWall.name = "BelowWall";
        belowWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        belowWall.transform.position = new Vector3(0f, -(wallHigh / 2f + wallThick / 2f), 0f);
        belowWall.transform.parent = gameWall.transform;

    }

    /// <summary>
    /// 创建圆形围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    public static void createCircleWall(float picAllW, float picAllH)
    {
        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;

        int circleNumber = 36;
        float circleR = 0;
        if (picAllH > picAllW)
        {
            circleR = picAllH;
        }
        else
        {
            circleR = picAllW;
        }
        GameObject gameWall = new GameObject("GameWall");

        List<Vector3> listPosition=  GeometryUtil.getCircleVertices(new Vector3(0,0), circleR* wallScale, circleNumber,true,CircleStartVectorEnum.Above);
        float angleItem = (float)360 / (float)circleNumber;
        for (int i = 0; i < listPosition.Count; i++) {
            Vector3 itemPosition = listPosition[i];
            GameObject belowWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
            belowWall.name = "Wall x:" + itemPosition.x + " y:" + itemPosition.y;
            belowWall.transform.localScale = new Vector3(circleR * wallScale, circleR * wallScale, 1f);
            belowWall.transform.position = itemPosition ;
            belowWall.transform.parent = gameWall.transform;
            belowWall.transform.Rotate(new Vector3(0, 0, -i*angleItem), Space.Self);

            belowWall.transform.position += belowWall.transform.up * circleR;
        }
    }

    /// <summary>
    /// 创建正方形围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    public static void createSquareWall(float picAllW, float picAllH)
    {
        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;
        if(picAllW> picAllH)
        {
            wallHigh = wallWith;
        }
        else
        {
            wallWith = wallHigh;
        }

        GameObject gameWall = new GameObject("GameWall");

        GameObject leftWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        leftWall.name = "LeftWall";
        leftWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        leftWall.transform.position = new Vector3(-(wallWith / 2f + wallThick / 2f), 0f, 0f);
        leftWall.transform.parent = gameWall.transform;

        GameObject rightWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        rightWall.name = "RightWall";
        rightWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        rightWall.transform.position = new Vector3((wallWith / 2f + wallThick / 2f), 0f, 0f);
        rightWall.transform.parent = gameWall.transform;

        GameObject aboveWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        aboveWall.name = "AboveWall";
        aboveWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        aboveWall.transform.position = new Vector3(0f, (wallHigh / 2f + wallThick / 2f), 0f);
        aboveWall.transform.parent = gameWall.transform;

        GameObject belowWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        belowWall.name = "BelowWall";
        belowWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        belowWall.transform.position = new Vector3(0f, -(wallHigh / 2f + wallThick / 2f), 0f);
        belowWall.transform.parent = gameWall.transform;
    }
}