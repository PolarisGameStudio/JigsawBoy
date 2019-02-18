using System.Collections.Generic;
using UnityEngine;

public class CreateGameWallUtil
{
    public static Vector3 wallCenter = new Vector3(0, 0, 0);
    //墙厚度
    public static float wallThick = 2000f;
    //墙缩放大小
    public static float wallScale = 2f;
    

    public static void createWall(GameWallEnum gameWallEnum, EquipColorEnum gameWallColor, float picAllW, float picAllH)
    {
        wallScale = 2f;

        if (gameWallEnum.Equals(GameWallEnum.Def))
        {
            createDefWall(gameWallColor, picAllW, picAllH,2);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Circle))
        {
            createCircleWall(gameWallColor, picAllW, picAllH);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Square))
        {
            createSquareWall(gameWallColor, picAllW, picAllH);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Def2))
        {
            createDefWall(gameWallColor, picAllW, picAllH,4);
        }
        else if (gameWallEnum.Equals(GameWallEnum.Def3))
        {
            createDefWall(gameWallColor, picAllW, picAllH, 6);
        }
    }

    /// <summary>
    /// 创建自适应围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    private static void createDefWall(EquipColorEnum gameWallColor, float picAllW, float picAllH,int size)
    {
        wallScale = size;

        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;

        GameObject gameWall = new GameObject("GameWall");

        GameObject leftWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        leftWall.name = "LeftWall";
        leftWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        leftWall.transform.position = new Vector3(-(wallWith / 2f + wallThick / 2f), 0f, 0f);
        leftWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, leftWall);

        GameObject rightWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        rightWall.name = "RightWall";
        rightWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        rightWall.transform.position = new Vector3((wallWith / 2f + wallThick / 2f), 0f, 0f);
        rightWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, rightWall);

        GameObject aboveWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        aboveWall.name = "AboveWall";
        aboveWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        aboveWall.transform.position = new Vector3(0f, (wallHigh / 2f + wallThick / 2f), 0f);
        aboveWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, aboveWall);

        GameObject belowWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        belowWall.name = "BelowWall";
        belowWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        belowWall.transform.position = new Vector3(0f, -(wallHigh / 2f + wallThick / 2f), 0f);
        belowWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, belowWall);

    }

    /// <summary>
    /// 创建圆形围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    public static void createCircleWall(EquipColorEnum gameWallColor, float picAllW, float picAllH)
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

        List<Vector3> listPosition = GeometryUtil.getCircleVertices(new Vector3(0, 0), circleR * wallScale * wallThick, circleNumber, true, CircleStartVectorEnum.Above);
        float angleItem = (float)360 / (float)circleNumber;
        for (int i = 0; i < listPosition.Count; i++)
        {
            Vector3 itemPosition = listPosition[i];
            GameObject itemWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
            itemWall.name = "Wall x:" + itemPosition.x + " y:" + itemPosition.y;
            itemWall.transform.localScale = new Vector3(circleR * wallScale * wallThick, circleR * wallScale * wallThick, 1f);
            itemWall.transform.position = itemPosition;
            itemWall.transform.parent = gameWall.transform;
            itemWall.transform.Rotate(new Vector3(0, 0, -i * angleItem), Space.Self);

            itemWall.transform.position += itemWall.transform.up * circleR;
            setWallColor(gameWallColor, itemWall);
        }
    }

    /// <summary>
    /// 创建正方形围墙
    /// </summary>
    /// <param name="picAllW"></param>
    /// <param name="picAllH"></param>
    public static void createSquareWall(EquipColorEnum gameWallColor, float picAllW, float picAllH)
    {
        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;
        if (picAllW > picAllH)
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
        setWallColor(gameWallColor, leftWall);

        GameObject rightWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        rightWall.name = "RightWall";
        rightWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        rightWall.transform.position = new Vector3((wallWith / 2f + wallThick / 2f), 0f, 0f);
        rightWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, rightWall);

        GameObject aboveWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        aboveWall.name = "AboveWall";
        aboveWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        aboveWall.transform.position = new Vector3(0f, (wallHigh / 2f + wallThick / 2f), 0f);
        aboveWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, aboveWall);

        GameObject belowWall = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/WallGameObj"));
        belowWall.name = "BelowWall";
        belowWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        belowWall.transform.position = new Vector3(0f, -(wallHigh / 2f + wallThick / 2f), 0f);
        belowWall.transform.parent = gameWall.transform;
        setWallColor(gameWallColor, belowWall);
    }


    private static void setWallColor(EquipColorEnum equipColorEnum, GameObject wallObj)
    {
        Renderer render = wallObj.GetComponent<Renderer>();
        if (render == null)
            return;
        EquipInfoBean infoBean = new EquipInfoBean();
        EnumUtil.getEquipColor(infoBean, equipColorEnum);
        Color equipColor = ColorUtil.getColor(infoBean.equipImageColor);
        render.material.color = equipColor;
    }
}