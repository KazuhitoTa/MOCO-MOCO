using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUpgradeState : State<MenuStateID, MenuStateMachine>
{
    public UpgradeScene upgradeScene;
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
        upgradeScene = GetComponent<UpgradeScene>();
        upgradeScene.SceneInit();
    }
    public override void OnEntry()
    {
        upgradeScene.SceneEntry();
        ui.SetActive(true);
        Debug.Log($"Upgrade:OnEntry");
    }
    public override void OnUpdate()
    {
        upgradeScene.SceneUpdate();
        //Debug.Log($"Upgrade:OnUpdate");
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Laboratory);
        //     Debug.Log($"Down S Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Collection);
        //     Debug.Log($"Down D Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Select);
        //     Debug.Log($"Down Escape Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Setting, true);
        //     Debug.Log($"Down Return Key" + new string('+', 15));
        // }
    }
    public override void OnExit()
    {
        upgradeScene.SceneExit();
        ui.SetActive(false);
        Debug.Log($"Upgrade:OnExit");
        Debug.Log(new string('=', 30));
    }

    public void EntryLaboratory()
    {
        stateMachine.SetMoveFlag(MenuStateID.Laboratory);
        Debug.Log($"Down S Key" + new string('+', 15));
    }

    public void EntryCollection()
    {
        stateMachine.SetMoveFlag(MenuStateID.Collection);
        Debug.Log($"Down D Key" + new string('+', 15));
    }

    public void EntrySelect()
    {
        stateMachine.SetMoveFlag(MenuStateID.Select);
        Debug.Log($"Down Escape Key" + new string('+', 15));
    }

    public void EntrySetting()
    {
        stateMachine.SetMoveFlag(MenuStateID.Setting, true);
        Debug.Log($"Down Return Key" + new string('+', 15));
    }
}