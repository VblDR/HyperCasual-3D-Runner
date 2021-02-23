using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public delegate void AwaitableCallback();
    public static IEnumerator WaitFor(float seconds, AwaitableCallback callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }
}
