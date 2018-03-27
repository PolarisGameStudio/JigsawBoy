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
                else if (value is string)
                {
                    form.AddField(key, (string)value);
                }
                else
                {
                    string valueStr = Convert.ToString(value);
                    form.AddField(key, valueStr);
                }
            }
        }
        return form;
    }

    public string dataToUrlStr()
    {
        StringBuilder urlStr = new StringBuilder("?");
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
                        urlStr.Append(key + "=" + "1" + "&");
                    else
                        urlStr.Append(key + "=" + "0" + "&");
                }
                else if (value is string)
                {
                    urlStr.Append(key + "=" + (string)value + "&");
                }
                else
                {
                    string valueStr = Convert.ToString(value);
                    urlStr.Append(key + "=" + valueStr + "&");
                }
            }
        }
        return urlStr.ToString();
    }
}

