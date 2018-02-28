using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameInfoUIControl : BaseMonoBehaviour
{

    //信息画布
    public Canvas gameInfoUICanvas;
    //信息介绍
    public Image gameInfoDetails;
    private Vector3 gameInfoDetailsOldPosition;

    // Use this for initialization
    void Start()
    {
        gameInfoUICanvas = GetComponent<Canvas>();
        Image[] imageList = GetComponentsInChildren<Image>();
        if (imageList != null)
        {
            int imageListSize = imageList.Length;
            for (int i = 0; i < imageListSize; i++)
            {
                Image itemImage = imageList[i];
                if (itemImage.name.Equals("GameInfoDetails"))
                {
                    gameInfoDetails = itemImage;
                    gameInfoDetailsOldPosition = gameInfoDetails.transform.position;
                }
            }
        }
        gameInfoUICanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            setGameInfoUIEnabled();
        }
    }

    /// <summary>
    /// 设置是否显示信息
    /// </summary>
    public void setGameInfoUIEnabled()
    {
        if (gameInfoUICanvas == null)
            return;
        bool isEnabled = gameInfoUICanvas.isActiveAndEnabled;
        Image gameInfoUIBackground = gameInfoUICanvas.GetComponent<Image>();
        if (isEnabled)
        {
            //设置背景颜色
            if (gameInfoUIBackground != null)
                gameInfoUIBackground.DOFade(0, 0.2f).OnComplete(delegate ()
                {
                    gameInfoUICanvas.enabled = !isEnabled;
                });
            gameInfoDetails.DOFade(0, 0.2f);
        }
        else
        {
            //设置背景颜色
            if (gameInfoUIBackground != null)
                gameInfoUICanvas.enabled = !isEnabled;
            gameInfoUIBackground.DOFade(0.5f, 0.2f);
            gameInfoDetails.DOFade(0.2f, 0.2f);
        }
    }
}
