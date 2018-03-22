using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using DG.Tweening;

public class MenuBackGroundCpt : BaseMonoBehaviour
{
    List<GameObject> listJigsaw;
    private bool isCreateJigsaw;
    private void Awake()
    {
        isCreateJigsaw = false;
        listJigsaw = new List<GameObject>();
    }

    public void startCreateJigsaw()
    {
        isCreateJigsaw = true;
      //  StartCoroutine(createJigsaw());
    }

    private void Update()
    {

    }

    public void endCreateJigsaw()
    {
        isCreateJigsaw = false;
      //  StopCoroutine(createJigsaw());
    }

    private IEnumerator createJigsaw()
    {
        while (isCreateJigsaw)
        {
            Texture2D texture2D = Texture2D.blackTexture;
            JigsawBean jigsawBean = CreateJigsawDataUtils.createJigsaw(JigsawStyleEnum.Normal, 1f, 1f, texture2D);

            GameObject jigsawObj = JigsawObjBuilder.buildJigsawGameObj(jigsawBean, texture2D);
            float startX = DevUtil.getRandomFloat(-(int)(DevUtil.GetScreenWith() / 2f), (int)(DevUtil.GetScreenWith() / 2f));
            float startY = DevUtil.GetScreenHeight() / 2f;
            float endX = DevUtil.getRandomFloat(-(int)(DevUtil.GetScreenWith() / 2f), (int)(DevUtil.GetScreenWith() / 2f));
            float endY = -DevUtil.GetScreenHeight() / 2f;
            Vector3 startPosition = new Vector3(startX, startY, 2);
            jigsawObj.transform.position = startPosition;
            jigsawObj.transform.DOMove(new Vector3(endX, endY, 2), 10);
            yield return new WaitForSeconds(1f);
        }
    }

    private void destroyJigsaw()
    {

    }
}

