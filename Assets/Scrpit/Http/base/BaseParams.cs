using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BaseParams
{
    public WWWForm dataToWWWForm()
    {
        WWWForm form = new WWWForm();
        Dictionary<string, object> listData = ReflexUtil.getAllNameAndValue(this);
        if (listData != null)
        {
            foreach (string key in listData.Keys)
            {
                object value = listData[key];
                if (value == null)
                    continue;
                if (value is bool)
                {
                    if ((bool)value)
                        form.AddField(key, "1");
                    else
                        form.AddField(key, "0");
                }
                else
                    form.AddField(key, (string)value);
            }
        }
        return form;
    }
}

