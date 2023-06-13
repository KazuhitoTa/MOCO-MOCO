using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLaboratoryState : State<MenuStateID, MenuStateMachine>
{
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Laboratory:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Laboratory:OnUpdate");
        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.SetMoveFlag(MenuStateID.Upgrade);
            Debug.Log($"Down A Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            stateMachine.SetMoveFlag(MenuStateID.Collection);
            Debug.Log($"Down D Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateMachine.SetMoveFlag(MenuStateID.Select);
            Debug.Log($"Down Escape Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            stateMachine.SetMoveFlag(MenuStateID.Setting, true);
            Debug.Log($"Down Return Key" + new string('+', 15));
        }
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Laboratory:OnExit");
        Debug.Log(new string('=', 30));
    }
}