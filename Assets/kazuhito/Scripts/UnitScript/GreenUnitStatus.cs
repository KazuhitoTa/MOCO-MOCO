using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenUnitStatus : MonoBehaviour
{
    private UnitStatus unitStatus;
    [SerializeField] UnitDataBase db;
    GreenUnitData data;
    float Attack;

    void Start()
    {
        data=db.GetDataInstance<GreenUnitData>(UnitGroup.Green);
        Attack=data.GetDefaltAttack(UnionLevel.Lv1);
        Debug.Log(data.unitGroup);
        Debug.Log(data);
    }
    

}
