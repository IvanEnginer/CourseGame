using Assets._Project.Develop.Runtime.Utilities.CoroutinesManager;
using System.Collections;
using UnityEngine;

public class CoroutinesPerformer : MonoBehaviour, ICoroutinesPerformer
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Coroutine StartPerform(IEnumerator coroutineFunction)
        => StartCoroutine(coroutineFunction);

    public void StopPerform(Coroutine coroutine)
        => StopCoroutine(coroutine);
}
