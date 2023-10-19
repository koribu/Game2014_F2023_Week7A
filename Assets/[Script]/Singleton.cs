using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public abstract class Singleton<T> where T : Singleton<T>
{
    protected Singleton() { }

    public static T Instance { get; }

    private static T Create()
    {
        Type t = typeof(T);
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var constructor = t.GetConstructor(flags, null, Type.EmptyTypes, null);
        var instance = constructor.Invoke(null);
        return Instance as T;
    }
 }
