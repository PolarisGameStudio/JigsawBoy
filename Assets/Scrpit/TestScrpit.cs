using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Texture2D pic2D = (Texture2D)Resources.Load("text1");
        Debug.Log("width:"+pic2D.width+" height:"+pic2D.height);

       List<JigsawBean> listData= CreateJigsawUtils.createJigsawList(JigsawStyleEnum.Normal,4,2,pic2D);
        for(int i = 0; i < listData.Count; i++)
        {
            JigsawBean item = listData[i];
            
            item.JigsawGameObj.transform.position=new Vector3(item.MarkLocation.x * 3.5f,item.MarkLocation.y * 3.5f,30);
            item.JigsawGameObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
