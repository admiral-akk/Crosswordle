using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        ManagerAwake();
    }

    protected virtual void ManagerAwake()
    {

    }
}
