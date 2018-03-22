using System;



public abstract class HttpResponseHandler<T> 
{
    public abstract void onSuccess(T result);

    public abstract void onError(String message);
}

