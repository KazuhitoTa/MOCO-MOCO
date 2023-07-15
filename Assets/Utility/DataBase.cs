using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public abstract class DataBase<T> : SingletonMonoBehaviour<T>
where T : MonoBehaviour
{
    protected abstract string FileName { get; }
    protected abstract string Path { get; }
    protected abstract string Key { get; }
    protected bool isEdited = true;
    public bool IsLoading { get; protected set; } = false;
    public bool IsSaveing { get; protected set; } = false;
    public virtual async void LoadAsync() { await Task.Delay(1); }
    public virtual async void SaveAsync() { await Task.Delay(1); }
    #if UNITY_EDITOR
    protected bool isDump = true;
    protected int step = -2;
    #endif
    private IReadOnlyList<(string json, string base64)> stringTable = new List<(string json, string base64)>()
    {
        new ("(", "+0/"),
        new (")", "+1/"),
        new ("{", "+3/"),
        new ("}", "+4/"),
        new ("[", "+5/"),
        new ("]", "+6/"),
        new ("\"", "+7/"),
        new (":", "+8/"),
        new (",", "+9/"),
    };

    protected string encrypt(in string plain)
    {
        string encrypted;

        using (Aes aes = Aes.Create())
        using (ICryptoTransform encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(Key), new byte[16]))
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using(StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plain);
                }
            }
            byte[] result = ms.ToArray();
            encrypted = Convert.ToBase64String(result);
        }
        return encrypted;
    }

    protected string decrypt(in string encrypted)
    {
        string plain;
        byte[] cipher = Convert.FromBase64String(encrypted);
        using (Aes aes = Aes.Create())
        using (ICryptoTransform decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(Key), new byte[16]))
        using (MemoryStream ms = new MemoryStream(cipher))
        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        using (StreamReader sr = new StreamReader(cs))
        {
            plain = sr.ReadToEnd();
        }
        return plain;
    }

    protected string replaceToBase64(in string text)
    {
        string result = text;
        foreach (var item in stringTable)
        {
            result = result.Replace(item.json, item.base64);
        }
        return result;
    }

    protected string replaceToJSON(in string text)
    {
        string result = text;
        foreach (var item in stringTable)
        {
            result = result.Replace(item.base64, item.json);
        }
        return result;
    }

    protected async Task saveAsync(string json)
    {
        #if !UNITY_EDITOR
        if (!isEdited) return;
        #endif

        IsSaveing = true;

        string base64 = replaceToBase64(json);

        string encrypted = encrypt(base64);

        await File.WriteAllBytesAsync(Path, Encoding.UTF8.GetBytes(encrypted));

        #if UNITY_EDITOR
        if (isDump)
        {
            Debug.Log("save");
            Debug.Log($"json:{json}");
            Debug.Log($"base64:{base64}");
            Debug.Log($"encrypted:{encrypted}");
        }
        #endif

        isEdited = false;

        IsSaveing = false;
    }

    protected async Task<string> loadAsync()
    {
        IsLoading = true;

        byte[] bytes = await File.ReadAllBytesAsync(Path);
        string encrypted = Encoding.UTF8.GetString(bytes);

        string base64 = decrypt(encrypted);

        string json = replaceToJSON(base64);

        #if UNITY_EDITOR
        if (isDump)
        {
            Debug.Log("load");
            Debug.Log($"encrypted:{encrypted}");
            Debug.Log($"base64:{base64}");
            Debug.Log($"json:{json}");
        }
        #endif

        IsLoading = false;

        return json;
    }
}