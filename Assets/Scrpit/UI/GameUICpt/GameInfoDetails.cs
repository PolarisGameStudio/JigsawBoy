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

    public void loadData(JigsawResInfoBean selectJigsawInfo)
    {
        if (selectJigsawInfo == null)
            return;
        this.selectJigsawInfo = selectJigsawInfo;

        JigsawResInfoIntroduceBean details = selectJigsawInfo.details;
        if (details == null)
            return;

        string workOfName = details.workOfName;
        string introductionContent = details.introductionContent;
        string storageArea = details.storageArea;
        string specifications = details.specifications;
        string timeOfCreation = details.timeOfCreation;
        string workOfCreator = details.workOfCreator;

        if (workOfName != null && workOfName.Length != 0)
            createTextItem("作品名称", workOfName);
        if (workOfCreator != null && workOfCreator.Length != 0)
            createTextItem("作    者", workOfCreator);
        if (introductionContent != null && introductionContent.Length != 0)
            createTextItem("简    介", introductionContent);
        if (storageArea != null && storageArea.Length != 0)
            createTextItem("作品所在", storageArea);
        if (specifications != null && specifications.Length != 0)
            createTextItem("规    格", specifications);
        if (timeOfCreation != null && timeOfCreation.Length != 0)
            createTextItem("创作时间", timeOfCreation);
    }

    /// <summary>
    /// 创建文本控件
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    private void createTextItem(string title, string content)
    {
        GameObject textObj = Instantiate(Resources.Load(GameInfoTextItem)) as GameObject;
        Text titleText = CptUtil.getCptFormParentByName<Transform, Text>(textObj.transform, "GameInfoTextTitle");
        Text contentText = CptUtil.getCptFormParentByName<Transform, Text>(textObj.transform, "GameInfoTextContent");

        titleText.text = title + ":";
        contentText.text = content;
        float contentH= contentText.preferredHeight;
        float ch = contentText.rectTransform.rect.height;
        RectTransform f= transform.GetComponent<RectTransform>();
        f.sizeDelta = new Vector2(f.rect.width, ch);
        textObj.transform.parent = transform;
    }
}

