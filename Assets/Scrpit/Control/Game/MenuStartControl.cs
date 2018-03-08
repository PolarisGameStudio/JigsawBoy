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


    private void Awake()
    {
        uiMasterControl = gameObject.AddComponent<UIMasterControl>();
        audioSourceControl= gameObject.AddComponent<AudioSourceControl>();   
    }

    private void Start()
    {
        uiMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }
}

