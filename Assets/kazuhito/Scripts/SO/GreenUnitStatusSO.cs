using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GreenUnitStatusSO : ScriptableObject
{
    public List<GreenUnitStatus> greenUnitStatuses=new List<GreenUnitStatus>();

    [System.Serializable]
    public class GreenUnitStatus
    {
        [SerializeField] GameObject unitModel;
        [SerializeField] GameObject bulletModel;
        [SerializeField] string unitName;
        [SerializeField] int hp;
        [SerializeField] int attack;
        [SerializeField] int criticalProbability;
        [SerializeField] int criticalDamage;
        [SerializeField] int attackSpeed;
        [SerializeField] int unitNumber;


        public GameObject UnitModel{get=>unitModel;}
        public GameObject BulletModel{get=>bulletModel;}
        public string UnitName{get=>unitName;}
        public int HP{get=>hp;}
        public int Attack{get=>attack;}
        public int CriticalProbability{get=>criticalProbability;}
        public int CriticalDamage{get=>criticalDamage;}
        public float AttackSpeed{get=>attackSpeed;}
        public int UnitNumber{get=>unitNumber;}


    }
   
    
}
