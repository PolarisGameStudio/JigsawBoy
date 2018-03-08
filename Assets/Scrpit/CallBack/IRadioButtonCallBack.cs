using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;


public interface IRadioButtonCallBack
{
    void radioBTOnClick(Toggle toggle,bool value);
}
