using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenUnitData : UnitData
{
    public override UnitGroup unitGroup => UnitGroup.Green;
    protected override int growthLevel => dataBase.GetGrowthLevel(unitGroup);
    public override float GetDefaltHp(UnionLevel unionLevel)
    {
        return (float)(10+((2*(float)unionLevel)+(5* Math.Pow(2, growthLevel-1)))*0.8f);
        //return (float)((float)unionLevel * growthLevel);
    }
    public override float GetDefaltAttack(UnionLevel unionLevel)
    {
        return unionLevel switch
        {
            UnionLevel.Lv1 => 2 * growthLevel,
            UnionLevel.Lv2 => 3 * growthLevel,
            UnionLevel.Lv3 => 5 * growthLevel,
            UnionLevel.Lv4 => 7 * growthLevel,
            UnionLevel.Lv5 => 11 * growthLevel,
            _ => throw new Exception()
        };
    }
}
