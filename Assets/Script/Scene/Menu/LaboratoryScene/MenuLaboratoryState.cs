using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLaboratoryState : State<MenuStateID, MenuStateMachine>
{
    public LaboratoryScene laboratoryScene;
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
        laboratoryScene = GetComponent<LaboratoryScene>();
        laboratoryScene.SceneInit();
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        //Debug.Log($"Laboratory:OnEntry");
        laboratoryScene.SceneEntry();
    }
    public override void OnUpdate()
    {
        laboratoryScene.SceneUpdate();
        //Debug.Log($"Laboratory:OnUpdate");
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Upgrade);
        //     Debug.Log($"Down A Key" + new string('+', 15));
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
        ui.SetActive(false);
        // Debug.Log($"Laboratory:OnExit");
        // Debug.Log(new string('=', 30));
        laboratoryScene.SceneExit();
    }

    public void EntryUpgrade()
    {
        stateMachine.SetMoveFlag(MenuStateID.Upgrade);
        // Debug.Log($"Down A Key" + new string('+', 15));
    }

    public void EntryCollection()
    {
        stateMachine.SetMoveFlag(MenuStateID.Collection);
        // Debug.Log($"Down D Key" + new string('+', 15));
    }

    public void EntrySelect()
    {
        stateMachine.SetMoveFlag(MenuStateID.Select);
        // Debug.Log($"Down Escape Key" + new string('+', 15));
    }

    public void EntrySetting()
    {
        stateMachine.SetMoveFlag(MenuStateID.Setting, true);
        // Debug.Log($"Down Return Key" + new string('+', 15));
    }
}