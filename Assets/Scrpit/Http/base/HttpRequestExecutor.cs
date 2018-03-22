using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class HttpRequestExecutor : BaseMonoBehaviour
{

    public void requestGet<T>(HttpResponseHandler<T> responseHandler)
    {

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
            responseHandler.onError(postData.error);
        }
        else
        {
            T result=default(T);
            if (postData.text != null)
            {
                result= JsonUtil.FromJson<T>(postData.text);
            }
            responseHandler.onSuccess(result);
        }
    }

    IEnumerator SendGet(string httpUrl)
    {
        WWW getData = new WWW(httpUrl);
        yield return getData;
        if (getData.error != null)
        {
            Debug.Log(getData.error);
        }
        else
        {
            Debug.Log(getData.text);
        }
    }
}

