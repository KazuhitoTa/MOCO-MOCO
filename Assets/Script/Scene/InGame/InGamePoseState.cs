using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePoseState : State<InGameStateID, InGameStateMachine>
{
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Pose:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Pose:OnUpdate");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetMoveFlag(InGameStateID.Play);
            Debug.Log($"Down Space Key" + new string('+', 15));
        }
        if (Input.GetKeyDown(KeyCode.A))
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
        Debug.Log($"Pose:OnExit");
        Debug.Log(new string('=', 30));
    }
}
