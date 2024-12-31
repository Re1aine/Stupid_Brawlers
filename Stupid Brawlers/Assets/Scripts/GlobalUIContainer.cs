using System;
using System.Collections;
using UnityEngine;

public class GlobalUIContainer : MonoBehaviour
{
    public event Action OnLoadScreenHidden;
    
    [SerializeField] private LoadScreen _loadScreen;
    
    private ICoroutineExecutor _coroutineExecutor;

    public void Construct(ICoroutineExecutor coroutineExecutor) => 
        _coroutineExecutor = coroutineExecutor;

    public void ShowLoadScreen() => _loadScreen.Show();

    public void HideLoadScreen()
    {
        _coroutineExecutor.StartCoroutine(HideScreen());
    }
    
    private IEnumerator HideScreen()
    {
        yield return new WaitForSeconds(1f);
        
        _loadScreen.Hide();
        
        OnLoadScreenHidden?.Invoke();
    }
}