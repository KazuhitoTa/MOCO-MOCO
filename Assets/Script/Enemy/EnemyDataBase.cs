 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyDataBase : DataBase<EnemyDataBase>
{
    protected override string FileName => "enemy_data.json";
    protected override string Path => Application.dataPath + "\\" + FileName;
    protected override string Key => "";
    protected override string IV => "";
}