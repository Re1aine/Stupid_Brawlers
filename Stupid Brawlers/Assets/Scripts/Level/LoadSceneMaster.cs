using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMaster
{
    private readonly ICoroutineExecutor _coroutineExecutor;

    public LoadSceneMaster(ICoroutineExecutor coroutineExecutor)
    {
        _coroutineExecutor = coroutineExecutor;
    }
    
    public void LoadSceneByName(string sceneName, Action isLoaded) => 
        _coroutineExecutor.StartCoroutine(LoadSceneAsync(sceneName, isLoaded));

    private IEnumerator LoadSceneAsync(string sceneName, Action isLoaded )
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            isLoaded?.Invoke();
            yield break;
        }
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        
        while (asyncOperation.isDone != true)
            yield return null;

        isLoaded.Invoke();
    }
}