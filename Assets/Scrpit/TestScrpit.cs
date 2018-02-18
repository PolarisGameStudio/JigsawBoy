using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Texture2D pic2D = (Texture2D)Resources.Load("text1");

       List<JigsawBean> listData= CreateJigsawDataUtils.createJigsawDataList(JigsawStyleEnum.Normal,10,5,pic2D);
        CreateJigsawGameObjUtil.createJigsawGameObjList(listData);

       List<GameObject> containerList= CreateJigsawContainerObjUtil.createJigsawContainerObjList(listData);
        for(int i = 0; i < listData.Count; i++)
        {
            JigsawBean item = listData[i];

            containerList[i].transform.position=new Vector3(item.MarkLocation.x * 3.5f,item.MarkLocation.y * 3.5f,0);
            containerList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
