using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameInfoUIControl : BaseUIControl
{
    //背景
    public Image gameInfoUIBackground;
    //信息介绍
    public ScrollRect gameInfoDetails;
    public Image gameInfoDetailsBackground;
    public Transform gameInfoDetailsContentTF;
    public GameInfoDetails gameInfoDetailsContentSC;
    //图片
    public ScrollRect gameInfoPic;
    public RectTransform gameInfoPicTF;
    public Image gameInfoPicImage;
    public RectTransform gameInfoPicImageTF;

    private new void Awake()
    {
        base.Awake();
        //初始化背景信息
        gameInfoUIBackground = GetComponent<Image>();

        //初始化信息介绍
        gameInfoDetailsBackground = CptUtil.getCptFormParentByName<Transform, Image>(transform, "GameInfoDetails");
        gameInfoDetails = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "GameInfoDetails");

        gameInfoDetailsContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(gameInfoDetails, "Content");
        if (gameInfoDetailsContentTF != null && CommonData.SelectPuzzlesInfo != null)
        {
            gameInfoDetailsContentSC = gameInfoDetailsContentTF.gameObject.AddComponent<GameInfoDetails>();
            gameInfoDetailsContentSC.loadData(CommonData.SelectPuzzlesInfo.puzzlesInfo);
        }

        //初始化图片
        gameInfoPic = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "GameInfoPic");
        gameInfoPicImage = CptUtil.getCptFormParentByName<ScrollRect, Image>(gameInfoPic, "Image");
        if (gameInfoPic != null)
        {
            gameInfoPicTF = gameInfoPic.GetComponent<RectTransform>();
        }
        if (gameInfoPicImage != null && CommonData.SelectPuzzlesInfo != null)
        {
            string picPath = CommonData.SelectPuzzlesInfo.puzzlesInfo.Data_file_path + CommonData.SelectPuzzlesInfo.puzzlesInfo.Mark_file_name;
            Sprite picSP = ResourcesManager.loadData<Sprite>(picPath);

            float gameInfoPicImageH = gameInfoPicTF.rect.height * 0.9f;
            float gameInfoPicImageW = (gameInfoPicTF.rect.height / picSP.texture.height) * picSP.texture.width * 0.9f;

            gameInfoPicImageTF = gameInfoPicImage.GetComponent<RectTransform>();
            gameInfoPicImageTF.sizeDelta = new Vector2(gameInfoPicImageW, gameInfoPicImageH);

            gameInfoPicImage.sprite = picSP;
        }
    }


    /// <summary>
    /// 设置是否显示信息
    /// </summary>
    private void setGameInfoUIEnabled(bool isEnabled)
    {
        if (mUICanvas == null)
            return;

        if (isEnabled)
        {
            //设置背景颜色
            mUICanvas.enabled = isEnabled;
            if (gameInfoUIBackground != null)
                gameInfoUIBackground.DOFade(0.5f, 0.2f);
            if (gameInfoDetailsBackground != null)
                gameInfoDetailsBackground.DOFade(0.2f, 0.2f);
        }
        else
        {
            //设置背景颜色
            if (gameInfoUIBackground != null)
            {
                gameInfoUIBackground.DOFade(0, 0.2f).OnComplete(delegate ()
                {
                    mUICanvas.enabled = isEnabled;
                });
            }
            else
            {
                mUICanvas.enabled = isEnabled;
            }
            if (gameInfoDetailsBackground != null)
                gameInfoDetailsBackground.DOFade(0, 0.2f);
        }
    }

    public override void openUI()
    {
        setGameInfoUIEnabled(true);
    }

    public override void closeUI()
    {
        setGameInfoUIEnabled(false);
    }

    public override void loadUIData()
    {

    }
}
