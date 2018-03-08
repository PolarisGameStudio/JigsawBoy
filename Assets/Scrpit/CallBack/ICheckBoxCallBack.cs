using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public interface ICheckBoxCallBack
{
    void onClickBox<T>(Toggle toggle,bool value,T data);
}

