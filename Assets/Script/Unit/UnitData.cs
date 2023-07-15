using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class UnitData
{
    [SerializeField] private UnitName name;
    public UnitName Name => name;
    [SerializeField] private UnitType type;
    public UnitType Type => type;
}