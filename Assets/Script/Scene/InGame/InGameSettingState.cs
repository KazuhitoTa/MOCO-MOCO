using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSettingState : State<InGameStateID, InGameStateMachine>
{
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Setting:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Setting:OnUpdate");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetMoveFlag(InGameStateID.Play);
            Debug.Log($"Down Space Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateMachine.SetBackFlag();
            Debug.Log($"Down Escape Key" + new string('+', 15));
        }
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Setting:OnExit");
        Debug.Log(new string('=', 30));
    }
}
