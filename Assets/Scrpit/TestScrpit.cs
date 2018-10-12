using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour
{

    void Start()
    {
        List<Vector3> listPosition = new List<Vector3>();

        listPosition.Add(new Vector3(-1.5f, -1.7f, 0.0f));
        listPosition.Add(new Vector3(-1.5f, 1.7f, 0.0f));
        listPosition.Add(new Vector3(-0.5f, 1.7f, 0.0f));
        listPosition.Add(new Vector3(-0.5f, 0.7f, 0.0f));
        listPosition.Add(new Vector3(0.5f, 0.7f, 0.0f));
        listPosition.Add(new Vector3(0.5f, 1.7f, 0.0f));
        listPosition.Add(new Vector3(1.5f, 1.7f, 0.0f));
        listPosition.Add(new Vector3(1.5f, 0.5f, 0.0f));
        listPosition.Add(new Vector3(0.5f, 0.5f, 0.0f));
        listPosition.Add(new Vector3(0.5f, -0.5f, 0.0f));
        listPosition.Add(new Vector3(1.5f, -0.5f, 0.0f));
        listPosition.Add(new Vector3(1.5f, -1.7f, 0.0f));

        string data = "";
        foreach (int position in TriangulationUtil.GetTriangles(listPosition))
        {
            data += position + " ,";
        }
        LogUtil.log(data);
        bool isIn = GeometryUtil.VertexIsInTriangle(new Vector3(-1f, 0f), new Vector3(0f, 0f), new Vector3(0f, 1f), new Vector3(1f, 0f));
        LogUtil.log("isIn" + isIn);
    }


}
