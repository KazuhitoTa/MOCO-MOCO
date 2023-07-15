 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public sealed class EnemyDataBase : DataBase<EnemyDataBase>
{
    protected override string FileName => "enemy_data.bin";
    protected override string Path => @$"{Application.dataPath}/{FileName}";
    protected override string Key => "01234567890123456789012345678912";
    
    public override async void Load()
    {
        throw new System.NotImplementedException();
    }

    public override async void Save()
    {
        throw new System.NotImplementedException();
    }
}