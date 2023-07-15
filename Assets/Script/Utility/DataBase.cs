using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class DataBase<T> : SingletonMonoBehaviour<T>
where T : MonoBehaviour
{
    protected abstract string FileName { get; }
    protected abstract string Path { get; }
    protected abstract string Key { get; }
    protected abstract string IV { get; }

    protected bool Load()
    {
        return false;
    }

    protected bool Save()
    {
        return false;
    }
}