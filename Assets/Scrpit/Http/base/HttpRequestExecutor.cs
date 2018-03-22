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
            Debug.Log(postData.text);
        }
    }

    IEnumerator SendGet(string _url)
    {
        WWW getData = new WWW(_url);
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

