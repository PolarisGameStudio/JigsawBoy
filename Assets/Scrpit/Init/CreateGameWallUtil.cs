using UnityEngine;

public class CreateGameWallUtil : BaseMonoBehaviour
{
    public static Vector3 wallCenter = new Vector3(0, 0, 0);
    //墙厚度
    public static float wallThick = 1000f;
    //墙缩放大小
    public static float wallScale = 2f;

    public static void createWall(float picAllW, float picAllH)
    {

        float wallWith = wallScale * picAllW;
        float wallHigh = wallScale * picAllH;

        GameObject gameWall = new GameObject("GameWall");

        GameObject leftWall = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/WallGameObj"));
        leftWall.name = "LeftWall";
        leftWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        leftWall.transform.position = new Vector3(-(wallWith / 2f + wallThick / 2f), 0f, 0f);
        leftWall.transform.parent = gameWall.transform;
    
        GameObject rightWall = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/WallGameObj"));
        rightWall.name = "RightWall";
        rightWall.transform.localScale = new Vector3(wallThick, wallHigh + wallThick * 2, 1f);
        rightWall.transform.position = new Vector3((wallWith / 2f + wallThick / 2f), 0f, 0f);
        rightWall.transform.parent = gameWall.transform;

        GameObject aboveWall = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/WallGameObj"));
        aboveWall.name = "AboveWall";
        aboveWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        aboveWall.transform.position = new Vector3(0f, (wallHigh / 2f + wallThick / 2f), 0f);
        aboveWall.transform.parent = gameWall.transform;

        GameObject belowWall = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/WallGameObj"));
        belowWall.name = "BelowWall";
        belowWall.transform.localScale = new Vector3(wallWith + wallThick * 2, wallThick, 1f);
        belowWall.transform.position = new Vector3(0f, -(wallHigh / 2f + wallThick / 2f), 0f);
        belowWall.transform.parent = gameWall.transform;

    }


}