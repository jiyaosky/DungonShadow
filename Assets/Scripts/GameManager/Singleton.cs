using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Singleton<T> where T : new()
{
    private static readonly T instance = new T();

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void Update(float dt)
    {

    }

    public virtual void OnDestroy(float dt)
    {

    }
}
