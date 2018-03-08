using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameMusicDetails : BaseMonoBehaviour
{

    public List<BGMInfoBean> listBGM;
    public void loadData()
    {
        listBGM = BGMInfoManager.LoadAllBGMInfo();
    }

    private void createMusicSelectItem(BGMInfoBean itemData)
    {

    }
}

