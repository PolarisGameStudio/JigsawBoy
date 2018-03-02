using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class GameInfoDetails : MonoBehaviour
{
    public JigsawResInfoBean selectJigsawInfo;
    private static string GameInfoTextItem = "Prefab/UI/Game/GameInfoTextItem";

    public int nameTextSize = 25;

    public void loadData(JigsawResInfoBean selectJigsawInfo)
    {
        if (selectJigsawInfo == null)
            return;
        this.selectJigsawInfo = selectJigsawInfo;

        JigsawResInfoIntroduceBean details = selectJigsawInfo.details;
        if (details == null)
            return;

        string introductionContent = details.introductionContent;

        string workOfName = details.workOfName;
        string storageArea = details.storageArea;
        string specifications = details.specifications;
        string timeOfCreation = details.timeOfCreation;
        string workOfCreator = details.workOfCreator;

        string moveOfName = details.moveOfName;
        string moveOfDirector = details.moveOfDirector;
        string stars = details.stars;
        string length = details.length;
        string releaseDate = details.releaseDate;

        string celebrityName = details.celebrityName;
        string bornAndDeath = details.bornAndDeath;
        string country = details.country;
        string knownFor = details.knownFor;
        string works = details.works;

        if (workOfName != null && workOfName.Length != 0)
            createTextItem("作品名称", workOfName, nameTextSize);
        if (workOfCreator != null && workOfCreator.Length != 0)
            createTextItem("作品作者", workOfCreator);
        if (storageArea != null && storageArea.Length != 0)
            createTextItem("作品所在", storageArea);
        if (specifications != null && specifications.Length != 0)
            createTextItem("作品规格", specifications);
        if (timeOfCreation != null && timeOfCreation.Length != 0)
            createTextItem("创作时间", timeOfCreation);

        if (moveOfName != null && moveOfName.Length != 0)
            createTextItem("电影名称", moveOfName, nameTextSize);
        if (moveOfDirector != null && moveOfDirector.Length != 0)
            createTextItem("电影导演", moveOfDirector);
        if (stars != null && stars.Length != 0)
            createTextItem("电影主演", stars);
        if (length != null && length.Length != 0)
            createTextItem("电影片长", length);
        if (releaseDate != null && releaseDate.Length != 0)
            createTextItem("上映日期", releaseDate);

        if (celebrityName != null && celebrityName.Length != 0)
            createTextItem("名字", celebrityName, nameTextSize);
        if (knownFor != null && knownFor.Length != 0)
            createTextItem("职业", knownFor);
        if (bornAndDeath != null && bornAndDeath.Length != 0)
            createTextItem("出生逝世", bornAndDeath);
        if (country != null && country.Length != 0)
            createTextItem("所在国家", country);
        if (works != null && works.Length != 0)
            createTextItem("主要成就", works);

        if (introductionContent != null && introductionContent.Length != 0)
            createTextItem("简介", introductionContent);
    }

    /// <summary>
    /// 创建文本控件
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    private void createTextItem(string title, string content)
    {
        createTextItem(title, content, 0);
    }

    /// <summary>
    /// 创建文本控件
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    private void createTextItem(string title, string content, int contentTextSize)
    {
        GameObject textObj = Instantiate(Resources.Load(GameInfoTextItem)) as GameObject;

        Text titleText = CptUtil.getCptFormParentByName<Transform, Text>(textObj.transform, "GameInfoTextTitle");
        Text contentText = CptUtil.getCptFormParentByName<Transform, Text>(textObj.transform, "GameInfoTextContent");


        //设置内容
        titleText.text = title + ":";
        contentText.text = content;
        //设置样式
        if (contentTextSize != 0)
        {
            contentText.fontSize = contentTextSize;
            contentText.resizeTextMaxSize = contentTextSize;
        }


        //设置高度
        RectTransform textTF = textObj.GetComponent<RectTransform>();
        float contentTextHight = contentText.preferredHeight;
        if (textTF != null)
        {
            textTF.sizeDelta = new Vector2(textTF.rect.width, contentTextHight);
        }
        //添加文本
        textObj.transform.parent = transform;
    }
}

