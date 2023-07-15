using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitData
{
    public UnitGroup unitGroup { get; protected set; }
    public int growthLevel { get; protected set; }
    public UnitData(int lv, UnitGroup ug) { growthLevel = lv; unitGroup = ug; }
    public virtual float GetDefaltHp(UnionLevel unionLevel) => 0;
    public virtual float GetDefaltAttack(UnionLevel unionLevel) => 0;
}