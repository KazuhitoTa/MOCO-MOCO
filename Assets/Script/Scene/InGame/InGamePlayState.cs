using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayState : State<InGameStateID, InGameStateMachine>
{
    [SerializeField] private GameObject ui;

    [SerializeField] MapManager _mapManager=null;
    
    [SerializeField]UnitManager _unitManager=null;
    [SerializeField]BulletManager _bulletManager=null;
    [SerializeField]CameraController cameraController=null;
    [SerializeField]EnemyManager _enemyManager=null;
    [SerializeField]Tower _fieldTower=null;
    
    void Start()
    {
        _unitManager.UnitAwake();
        _enemyManager.EnemyAwake();
        _bulletManager.BulletAwake();
        ui.SetActive(false);
        _unitManager._Start();
        _enemyManager.Init();
        _mapManager.Init();
        
        cameraController._Start();
        _fieldTower.Init();
        
    }
    public override void OnEntry()//updateの最初
    {
        ui.SetActive(true);
        Debug.Log($"Play:OnEntry");
    }
    public override void OnUpdate()
    {
        _mapManager.ManagedUpdate();
        _unitManager.ManagedUpdate();
        _enemyManager.ManagedUpdate();
        _bulletManager.BulletUpdate();
        cameraController._Update();
        _fieldTower.ManagedUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetMoveFlag(InGameStateID.Pose);
            Debug.Log($"Down Space Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.SetMoveFlag(InGameStateID.Exitpop, true);
            Debug.Log($"Down A Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            stateMachine.SetMoveFlag(InGameStateID.Setting, true);
            Debug.Log($"Down Return Key" + new string('+', 15));
        }
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Play:OnExit");
        Debug.Log(new string('=', 30));
    }
}