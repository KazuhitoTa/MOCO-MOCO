using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapStatusSO : ScriptableObject
{
    public List<MapStatus> mapStatusList=new List<MapStatus>();

    [System.Serializable]
    public class MapStatus
    {
        [SerializeField] GameObject mapTileModel;
        [SerializeField] string stageName;
        [SerializeField] int mapMaxRows;
        [SerializeField] int mapMaxColumns;
        [SerializeField] MyTuple[] tuples;
        [SerializeField] Wave[] waves;

        public GameObject MapTileModel{get=>mapTileModel;}
        public string StageName{get=>stageName;}
        public int MapMaxRows{get=>mapMaxRows;}
        public int MapMaxColumns{get=>mapMaxColumns;}
        public MyTuple[] Tuples{get=>tuples;}
        public Wave[] Waves{get=>waves;}

    }

    [System.Serializable]
    public class MyTuple
    {
        public Vector2 enemySpawn;
        public int enemySpawnNumber;
    }
    

    [System.Serializable]
    public class Wave
    {
        public MyTuple[] tuple;
    }

   
    
}
