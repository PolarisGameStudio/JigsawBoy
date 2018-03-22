using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BaseManagerImpl
{
    protected HttpRequestExecutor excutor = new HttpRequestExecutor();
    protected String baseUrl;

    public void requestPostForm<T,V>(string url,V baseParams, HttpResponseHandler<T> responseHandler) where V : BaseParams
    {
        excutor.requestPostForm(baseUrl+ "/"+url, baseParams, responseHandler);
    }
}

