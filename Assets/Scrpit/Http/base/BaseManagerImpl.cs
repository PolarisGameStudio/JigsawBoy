using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseManagerImpl 
{
    protected HttpRequestExecutor excutor = Camera.main.GetComponent<HttpRequestExecutor>();
    protected String baseUrl;

    public void requestPostForm<T, V>(string url, V baseParams, HttpResponseHandler<T> responseHandler) where V : BaseParams
    {
        excutor.requestPostForm(baseUrl + "/" + url, baseParams, responseHandler);
    }
}

