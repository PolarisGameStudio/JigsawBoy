using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMusicDetails : BaseMonoBehaviour
{
    private static string GameMusicSelectItem = "Prefab/UI/Game/GameMusicSelectItem";
    private IButtonCallBack<Button, BGMInfoBean> buttonCallBack;

    public List<BGMInfoBean> listBGM;

    public void loadData()
    {
        listBGM = BGMInfoManager.LoadAllBGMInfo();
        if (listBGM != null)
        {
            int listBGMSize = listBGM.Count;
            for (int i = 0; i < listBGMSize; i++)
            {
                BGMInfoBean itemData = listBGM[i];
                createMusicSelectItem(itemData);
            }
        }
    }

    /// <summary>
    /// 设置监听
    /// </summary>
    /// <param name="radioButtonCallBack"></param>
    public void addMusicSelectCallBack(IButtonCallBack<Button, BGMInfoBean> buttonCallBack)
    {
        this.buttonCallBack = buttonCallBack;
    }

    private void createMusicSelectItem(BGMInfoBean itemData)
    {
        GameObject itemObj = Instantiate(ResourcesManager.LoadData<GameObject>(GameMusicSelectItem));

        Button itemBT = itemObj.GetComponent<Button>();
        itemBT.onClick.AddListener(() => changeMusic(itemBT, itemData));

        Text itemText = CptUtil.getCptFormParentByName<Transform, Text>(itemObj.transform, "MusicSelectItemText");
        itemObj.transform.SetParent(transform);
        itemText.text = itemData.Name;
    }

    private void changeMusic(Button itemBT, BGMInfoBean itemData)
    {
        if (buttonCallBack != null)
            buttonCallBack.buttonOnClick(itemBT, itemData);
    }
}

