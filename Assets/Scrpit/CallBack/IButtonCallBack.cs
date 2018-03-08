using UnityEngine;

public interface IButtonCallBack<T, V> where T:Component
{
    void buttonOnClick(T button,V data) ;

}