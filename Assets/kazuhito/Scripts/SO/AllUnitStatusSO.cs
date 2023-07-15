
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AllUnitStatusSO : ScriptableObject
{
    [SerializeField] YellowUnitStatusSO yellowUnitStatusSO;
    [SerializeField] GreenUnitStatusSO greenUnitStatusSO;
        
    public YellowUnitStatusSO YellowUnitStatusSO{get=>yellowUnitStatusSO;}
    public GreenUnitStatusSO GreenUnitStatusSO{get=>greenUnitStatusSO;}

}
