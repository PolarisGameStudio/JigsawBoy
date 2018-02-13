using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawWireFrame : MonoBehaviour {
    Vector3[] listVertices;
    LineRenderer jigsawLineRender;
    // Use this for initialization
    void Start () {
        MeshFilter jigsawMeshFilter=  this.GetComponent<MeshFilter>() ;     
       jigsawLineRender = gameObject.AddComponent<LineRenderer>();

        if (jigsawMeshFilter == null)
            return;
   listVertices=jigsawMeshFilter.mesh.vertices;
        jigsawLineRender.startColor=Color.red;
        jigsawLineRender.endColor = Color.red;
        jigsawLineRender.startWidth = 1f;
        jigsawLineRender.endWidth = 1f;
     
    }
	
	// Update is called once per frame
	void Update () {

        jigsawLineRender.positionCount = listVertices.Length;
        for(int i=0;i< listVertices.Length; i++)
        {
            jigsawLineRender.SetPosition(i,listVertices[i]);
        }
     

    }

}
