using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeData
{
    public static string NextSceneName;
}

public class LoadingSceneCpt : MonoBehaviour
{
    private float loadingSpeed = 1;
    private float targetValue;
    private bool isPrepare = false;
    private AsyncOperation operation;

    // Use this for initialization
    void Start()
    {
        //启动协程
        StartCoroutine(AsyncLoading());
        StartCoroutine(prepareTime());
    }

    // Update is called once per frame
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }
        if (targetValue.Equals(1)&&isPrepare)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(SceneChangeData.NextSceneName);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        yield return operation;
    }

    /// <summary>
    /// 准备时间
    /// </summary>
    /// <returns></returns>
    IEnumerator prepareTime()
    {
        yield return new WaitForSeconds(loadingSpeed);
        isPrepare = true;
    }
}
