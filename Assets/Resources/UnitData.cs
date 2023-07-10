using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitData : MonoBehaviour
{
    [SerializeField] protected UnitDataBase dataBase;
    public abstract UnitGroup unitGroup { get; }
    protected abstract int growthLevel { get; }
    public abstract float GetDefaltHp(UnionLevel unionLevel);
    public abstract float GetDefaltAttack(UnionLevel unionLevel);
}