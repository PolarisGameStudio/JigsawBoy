using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectUIControl : BaseMonoBehaviour
{
    public Canvas menuSelectUICanvas;

    public ScrollRect resTypeSelectView;
    public JigsawResTypeSelect resTypeSelectContent;

    public ScrollRect jigsawSelectView;
    public JigsawSelect jigsawSelectContent;

    void Start()
    {
        menuSelectUICanvas = GetComponent<Canvas>();
        ScrollRect[] listScrollRect = GetComponentsInChildren<ScrollRect>();
        if (listScrollRect != null)
        {
            int listScrollSize = listScrollRect.Length;
            for (int i = 0; i < listScrollSize; i++)
            {
                ScrollRect itemScroll = listScrollRect[i];
                if (itemScroll.name.Equals("ResTypeSelectView"))
                {
                    resTypeSelectView = itemScroll;
                    Transform contentView = getScorllViewChildContent(resTypeSelectView.transform);
                    if (contentView != null)
                        resTypeSelectContent = contentView.gameObject.AddComponent<JigsawResTypeSelect>();
                }
                else if (itemScroll.name.Equals("JigsawSelectView"))
                {
                    jigsawSelectView = itemScroll;
                    Transform contentView = getScorllViewChildContent(jigsawSelectView.transform);
                    if (contentView != null) {
                        jigsawSelectContent = contentView.gameObject.AddComponent<JigsawSelect>();
                        jigsawSelectContent.loadJigsaw(JigsawResourcesEnum.Painting);
                    }
                      
                }
            }
        }
    }

    /// <summary>
    /// 设置拼图类型选择数据
    /// </summary>
    /// <param name="resTypeSelectView"></param>
    private void setResTypeSelectData(JigsawResTypeSelect select)
    {
        if (select == null)
            return;
        foreach (JigsawResourcesEnum itemEnum in Enum.GetValues(typeof(JigsawResourcesEnum)))
        {
            //TODO
        }
    }

    /// <summary>
    /// 获取内存对象
    /// </summary>
    /// <param name="fatherTF"></param>
    /// <returns></returns>
    private Transform getScorllViewChildContent(Transform fatherTF)
    {
        Transform[] childTFList = fatherTF.GetComponentsInChildren<Transform>();
        if (childTFList != null)
        {
            int childTFSize = childTFList.Length;
            for (int i = 0; i < childTFSize; i++)
            {
                Transform itemTF = childTFList[i];
                if (itemTF.name.Equals("Content"))
                {
                    return itemTF;
                }
            }
        }
        return null;
    }
}

