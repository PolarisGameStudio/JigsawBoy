using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawMoveCpt : BaseMonoBehaviour
{



	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveTo(float xLocation,float yLocation)
    {
        transform.position = new Vector3(xLocation, yLocation);
    }
}
