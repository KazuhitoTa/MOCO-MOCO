using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectState : State<MenuStateID, MenuStateMachine>
{
    public SelectScene selectScene;
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
        selectScene = GetComponent<SelectScene>();
        selectScene.SceneInit();
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        // Debug.Log($"Select:OnEntry");
        selectScene.SceneEntry();
    }
    public override void OnUpdate()
    {
        selectScene.SceneUpdate();
        //Debug.Log($"Select:OnUpdate");
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Upgrade);
        //     Debug.Log($"Down A Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Teampop);
        //     Debug.Log($"Down Space Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Setting, true);
        //     Debug.Log($"Down Return Key" + new string('+', 15));
        // }
        selectScene.SceneUpdate();
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        // Debug.Log($"Select:OnExit");
        // Debug.Log(new string('=', 30));
        selectScene.SceneExit();
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

    public void EntryTeampop()
    {
        stateMachine.SetMoveFlag(MenuStateID.Teampop);
        // Debug.Log($"Down Space Key" + new string('+', 15));
    }

    public void EntrySetting()
    {
        stateMachine.SetMoveFlag(MenuStateID.Setting, true);
        // Debug.Log($"Down Return Key" + new string('+', 15));
    }
}