using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UnitDataBase : DataBase<UnitDataBase>
{
    protected override string FileName => "unit_data.json";
    protected override string Path => Application.dataPath + "\\" + FileName;
    protected override string Key => "";
    protected override string IV => "";
}