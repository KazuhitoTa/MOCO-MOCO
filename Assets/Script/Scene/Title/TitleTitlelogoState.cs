using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTitlelogoState : State<TitleStateID, TitleStateMachine>
{
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Titlelogo:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Titlelogo:OnUpdate");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MenuLoad");
            Debug.Log($"Down Space Key" + new string('+', 15));
        }
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Titlelogo:OnExit");
        Debug.Log(new string('=', 30));
    }
}