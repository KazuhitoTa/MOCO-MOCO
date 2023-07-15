using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public sealed class UserDataBase : DataBase<UserDataBase>
{
    protected override string FileName => "user_data.json";
    protected override string Path => Application.dataPath + "\\" + FileName;
    protected override string Key => "";
    protected override string IV => "";
}