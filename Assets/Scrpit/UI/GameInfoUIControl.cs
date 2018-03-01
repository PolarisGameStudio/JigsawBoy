using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameInfoUIControl : BaseMonoBehaviour
{

    //信息画布
    public Canvas gameInfoUICanvas;
    public Image gameInfoUIBackground;
    //信息介绍
    public ScrollRect gameInfoDetails;
    public Image gameInfoDetailsBackground;
    public Transform gameInfoDetailsContentTF;
    public GameInfoDetails gameInfoDetailsContentSC;


    // Use this for initialization
    void Start()
    {
        //初始化画布信息
        gameInfoUICanvas = GetComponent<Canvas>();
        gameInfoUIBackground = GetComponent<Image>();

        //初始化信息介绍
        gameInfoDetailsBackground = CptUtil.getCptFormParentByName<Transform, Image>(transform, "GameInfoDetails");
        gameInfoDetails = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "GameInfoDetails");

        gameInfoDetailsContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(gameInfoDetails, "Content");
        if (gameInfoDetailsContentTF != null)
        {
            gameInfoDetailsContentSC= gameInfoDetailsContentTF.gameObject.AddComponent<GameInfoDetails>();
            gameInfoDetailsContentSC.loadData(CommonData.selectJigsawInfo);
        }
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
        if (gameInfoUICanvas == null || gameInfoUIBackground == null)
            return;
        bool isEnabled = gameInfoUICanvas.isActiveAndEnabled;
        if (isEnabled)
        {
            //设置背景颜色
            gameInfoUIBackground.DOFade(0, 0.2f).OnComplete(delegate ()
            {
                gameInfoUICanvas.enabled = !isEnabled;
            });
            gameInfoDetailsBackground.DOFade(0, 0.2f);
        }
        else
        {
            //设置背景颜色
            gameInfoUICanvas.enabled = !isEnabled;
            gameInfoUIBackground.DOFade(0.5f, 0.2f);
            gameInfoDetailsBackground.DOFade(0.2f, 0.2f);
        }
    }
}
