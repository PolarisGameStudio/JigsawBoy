using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Texture2D pic2D = (Texture2D)Resources.Load("text1");

        List<Vector3> listVertices = new List<Vector3>();
        listVertices.Add(new Vector3(0, 0, 0));
        listVertices.Add(new Vector3(0, 1, 0));
        listVertices.Add(new Vector3(1, 1, 0));
        listVertices.Add(new Vector3(1, 0, 0));
        List<Vector2> listUvPostion = new List<Vector2>();
        listUvPostion.Add(new Vector2(0, 0));
        listUvPostion.Add(new Vector2(0, 1));
        listUvPostion.Add(new Vector2(1, 1));
        listUvPostion.Add(new Vector2(1, 0));
        GameObject gameObject = CreateJigsawGameObj.getJigsawGameObj(listVertices, listUvPostion, pic2D);

        gameObject.AddComponent<Transform>();
        gameObject.transform.position=new Vector3(0,1,-8);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
