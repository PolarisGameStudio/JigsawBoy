using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabCpt : BaseMonoBehaviour
{
    public Sprite unclickSprite;
    public Sprite clickSprite;

    public List<Button> listTab;

    public void selectTab(Button selectButton)
    {
        if (listTab == null)
            return;
        if (unclickSprite == null)
            return;
        if (clickSprite == null)
            return;
        foreach (Button itemTab in listTab) {
            Image itemImage= itemTab.GetComponent<Image>();
            if (itemImage == null)
                continue;
            if (itemTab == selectButton)
            {
                itemImage.sprite = clickSprite;
            }
            else
            {
                itemImage.sprite = unclickSprite;
            }
        }
    }
}