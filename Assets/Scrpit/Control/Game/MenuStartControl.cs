using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartControl : BaseMonoBehaviour
{
    public UIMasterControl uiMasterControl;
    public AudioSourceControl audioSourceControl;
    public MenuBackGroundCpt menuBackGround;

    private void Awake()
    {
        uiMasterControl = gameObject.AddComponent<UIMasterControl>();
        audioSourceControl = gameObject.AddComponent<AudioSourceControl>();

        GameObject menuBackGroundObj = MenuBackGroundBuilder.buildMenuBack(new Vector3(0, 0, 0));
        menuBackGround = menuBackGroundObj.GetComponent<MenuBackGroundCpt>();
    }

    private void Start()
    {
        if (CommonData.SelectPuzzlesInfo != null)
        {
            if (CommonData.SelectPuzzlesInfo.puzzlesInfo.mark_file_name.Contains("workshop_pic"))
            {
                uiMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuWorkshop);
            }
            else
            {
                uiMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuSelectUI);
            }
          
        }
        else
        {
            uiMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
        }
 

        if (menuBackGround != null)
            menuBackGround.startCreateJigsaw();
    }
}

