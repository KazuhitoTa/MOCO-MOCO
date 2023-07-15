using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageEnemyStatusLevel : ScriptableObject
{
    public List<EnemyList> enemyLeveles=new List<EnemyList>();
    [System.Serializable]
    public class EnemyList
    {
        [SerializeField]List<float> enemyLevel;
        public List<float> EnemyLevel{get=>enemyLevel;}
    }
}
