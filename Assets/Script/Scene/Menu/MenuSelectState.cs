using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectState : State<MenuStateID, MenuStateMachine>
{
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Select:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Select:OnUpdate");
        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.SetMoveFlag(MenuStateID.Upgrade);
            Debug.Log($"Down A Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetMoveFlag(MenuStateID.Teampop);
            Debug.Log($"Down Space Key" + new string('+', 15));
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
        Debug.Log($"Select:OnExit");
        Debug.Log(new string('=', 30));
    }
}