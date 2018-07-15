using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseManagerImpl 
{
    protected HttpRequestExecutor excutor = CptUtil.getCptFormSceneByName<HttpRequestExecutor>("Steam");
    protected String baseUrl;

    public void requestGet<T, V>(string url, V baseParams, HttpResponseHandler<T> responseHandler) where V : BaseParams
    {
        excutor.requestGet(baseUrl + "/" + url, baseParams, responseHandler);
    }

    public void requestPostForm<T, V>(string url, V baseParams, HttpResponseHandler<T> responseHandler) where V : BaseParams
    {
        excutor.requestPostForm(baseUrl + "/" + url, baseParams, responseHandler);
    }
}

