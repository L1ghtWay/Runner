using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T _instance;

    public static T instance
    {
        get
        {
            CreateInstance();
            return _instance;
        }
    }

    public static void CreateInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }      
        }
    }

    public virtual void Awake()
    {       
        if (_instance != null)
        {
            DestroyImmediate(gameObject);
        }
    }

    protected bool initialized;

    protected virtual void Initialize() { }
}
