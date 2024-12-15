using System.Collections;
using UnityEngine;

public interface ICoroutineExecutor
{
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(IEnumerator coroutine);
}