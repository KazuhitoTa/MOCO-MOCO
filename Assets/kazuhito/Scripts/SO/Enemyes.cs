using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemyes : ScriptableObject
{
    public List<EnemyesStatus> EnemyDefaultStatuses=new List<EnemyesStatus>();
    [System.Serializable]
    public class EnemyesStatus
    {
        [SerializeField] GameObject enemyModel;
        [SerializeField] GameObject bulletModel;
        [SerializeField] string enemyName;
        [SerializeField] int hp;
        [SerializeField] int attack;
        [SerializeField] int criticalProbability;
        [SerializeField] float criticalDamage;
        [SerializeField] float attackSpeed;
        [SerializeField] float moveSpeed;

        public GameObject EnemyModel{get=>enemyModel;}
        public GameObject BulletModel{get=>bulletModel;}
        public string EnemyName{get=>enemyName;}
        public int HP{get=>hp;}
        public int Attack{get=>attack;}
        public int CriticalProbability{get=>criticalProbability;}
        public float CriticalDamage{get=>criticalDamage;}
        public float AttackSpeed{get=>attackSpeed;}
        public float MoveSpeed{get=>moveSpeed;}
    }

}
