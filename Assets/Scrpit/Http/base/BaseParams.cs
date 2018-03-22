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
            foreach (var item in listData)
            {
                form.AddField(item.Key, (string)item.Value);
            }
        }
        return form;
    }
}

