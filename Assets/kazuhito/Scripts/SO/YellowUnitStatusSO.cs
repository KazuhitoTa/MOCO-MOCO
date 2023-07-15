using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class YellowUnitStatusSO : ScriptableObject
{
    public List<YellowUnitStatus> yellowUnitStatuses=new List<YellowUnitStatus>();

    [System.Serializable]
    public class YellowUnitStatus
    {
        [SerializeField] GameObject unitModel;
        [SerializeField] GameObject bulletModel;
        [SerializeField] Sprite buttonImage;
        [SerializeField] string unitName;
        [SerializeField] int hp;
        [SerializeField] int attack;
        [SerializeField] int criticalProbability;
        [SerializeField] int criticalDamage;
        [SerializeField] float attackSpeed;
        [SerializeField] int unitNumber;
        [SerializeField] int numbnessTime;
        [SerializeField] int numbnessInterval;
        [SerializeField] int numbnessCount;


        public GameObject UnitModel{get=>unitModel;}
        public GameObject BulletModel{get=>bulletModel;}
        public Sprite BottonImg{get=>buttonImage;}
        public string UnitName{get=>unitName;}
        public int HP{get=>hp;}
        public int Attack{get=>attack;}
        public int CriticalProbability{get=>criticalProbability;}
        public int CriticalDamage{get=>criticalDamage;}
        public float AttackSpeed{get=>attackSpeed;}
        public int UnitNumber{get=>unitNumber;}
        public int NumbnessTime{get=>numbnessTime;}
        public int NumbnessInterval{get=>numbnessInterval;}
        public int NumbnessCount{get=>numbnessCount;}



    }
   
    
}

