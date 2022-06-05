using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : class, new()
{
    private static T _Instance = null;
    public static T Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = new T();
			}
			return _Instance;
		}
	}
}
