using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<TClass> : MonoBehaviour
where TClass : MonoBehaviour
{
    private static TClass instance;
	public static TClass Instance
    {
		get
        {
			if (instance == null)
            {
				instance = (TClass)FindObjectOfType(typeof(TClass));
				if (instance == null)
                {
					Debug.LogError (typeof(TClass) + "is nothing");
				}
				DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}
	protected void Awake()
	{
		if( this == Instance) return;
		Destroy(this);
	}
}