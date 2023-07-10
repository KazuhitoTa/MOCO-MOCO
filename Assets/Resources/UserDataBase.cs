using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public sealed class UserDataBase : DataBase<UserDataBase>
{
    protected override string FileName => "user_data.bin";
    protected override string Path => @$"{Application.dataPath}/{FileName}";
    protected override string Key => "01234567890123456789012345678912";
    [SerializeField] private float bgmVolume;
    public float BGMVolume
    {
        get => bgmVolume;
        set
        {
            bgmVolume = Math.Clamp(value, 0, 1);
            isEdited = true;
        }
    }
    [SerializeField] private float seVolume;
    public float SEVolume
    {
        get => seVolume;
        set
        {
            seVolume = Math.Clamp(value, 0, 1);
            isEdited = true;
        }
    }
    [SerializeField] private bool isShowDamage;
    public bool IsShowDamage
    {
        get => isShowDamage;
        set
        {
            isShowDamage = value;
            isEdited = true;
        }
    }

    void Start()
    {
        BGMVolume = 1;
        SEVolume = 1;
        IsShowDamage = true;
    }
    
    void Update()
    {
        #if UNITY_EDITOR
        switch (step)
        {
            case 0:
                Save();
                step++;
                break;
            case 1:
                if (!IsSaveing) step++;
                break;
            case 2:
                Load();
                step++;
                break;
            case 3:
                if (!IsLoading) step++;
                break;
            default:
                break;
        }
        #endif
    }

    public override async void Load()
    {
        string json = await load();
        JsonUtility.FromJson<List<UnitData>>(json);
    }

    public override async void Save()
    {
        string json = JsonUtility.ToJson(this);
        await save(json);
    }
}