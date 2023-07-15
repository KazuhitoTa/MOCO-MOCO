using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Stage : MonoBehaviour
{
    //マップ生成とプレイヤー操作用変数
    [SerializeField] MapManager _mapManager=null;
    //敵生成用変数
    [SerializeField]EnemyManager _enemyManager = null;
    [SerializeField]UnitManager _unitManager=null;
    [SerializeField]BulletManager _bulletManager=null;
    [SerializeField]Tower _tower=null;

    void Awake()
    {
        _enemyManager.EnemyAwake();
        _unitManager.UnitAwake();
        _bulletManager.BulletAwake();
    }

    void Start()
    {
        _enemyManager.Init();
        _mapManager.Init();
        _tower.Init();
    }

    void Update()
    {
        _mapManager.ManagedUpdate();
        _tower.ManagedUpdate();
        _unitManager.ManagedUpdate();
        _enemyManager.ManagedUpdate();
        _bulletManager.BulletUpdate();
    }

    
}
