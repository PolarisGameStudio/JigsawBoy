using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class HttpRequestExecutor : BaseMonoBehaviour
{

    public void requestGet<T>(string baseHttpUrl, BaseParams baseParams, HttpResponseHandler<T> responseHandler)
    {
        string httpUrl = baseHttpUrl + baseParams.dataToUrlStr();
        LogUtil.log("requestGet:"+ httpUrl);
        StartCoroutine(SendGet(httpUrl, responseHandler));
    }

    public void requestPostForm<T>(string httpUrl, BaseParams baseParams, HttpResponseHandler<T> responseHandler)
    {
        WWWForm form = baseParams.dataToWWWForm();
        StartCoroutine(SendPostForm(httpUrl, form, responseHandler));
    }

    IEnumerator SendPostForm<T>(string httpUrl, WWWForm sendParams, HttpResponseHandler<T> responseHandler)
    {
        WWW postData = new WWW(httpUrl, sendParams);
        yield return postData;
        if (postData.error != null)
        {
            LogUtil.log(postData.error);
            responseHandler.onError(postData.error);
        }
        else
        {
            T result=default(T);
            if (postData.text != null)
            {
                LogUtil.log(postData.text);
                result = JsonUtil.FromJson<T>(postData.text);
            }
            responseHandler.onSuccess(result);
        }
    }

    IEnumerator SendGet<T>(string httpUrl, HttpResponseHandler<T> responseHandler)
    {
        WWW getData = new WWW(httpUrl);
        yield return getData;
        if (getData.error != null)
        {
            LogUtil.log(getData.error);
            responseHandler.onError(getData.error);
        }
        else
        {
            T result = default(T);
            if (getData.text != null)
            {
                LogUtil.log(getData.text);
                result = JsonUtil.FromJson<T>(getData.text);
            }
            responseHandler.onSuccess(result);
        }
    }
}

