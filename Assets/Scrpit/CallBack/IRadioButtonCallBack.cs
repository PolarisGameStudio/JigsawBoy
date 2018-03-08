using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public interface IRadioButtonCallBack<T, V> where T:Component
{
    void radioBTOnClick(T radioBT,bool value,V data) ;
}
