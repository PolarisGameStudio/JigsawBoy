using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCpt
{

    static bool isExiting = false;
    static bool WantsToQuit()
    {
        if (isExiting)
        {
            return true;
        }
        else
        {
            DialogManager
                .createGeneralDialog()
                .setTitle("确认")
                .setContent("是否要退出")
                .setCallBack(new DialogCallBack());
            return false;
        }

    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
       // Application.wantsToQuit += WantsToQuit;
    }

    public class DialogCallBack : GeneralDialog.CallBack
    {
        public void cancelClick()
        {
            isExiting = false;
        }

        public void submitClick()
        {
            isExiting = true;
            Application.Quit();
        }
    }
}
