using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTeampopState : State<MenuStateID, MenuStateMachine>
{
    public TeampopScene teampopScene;
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
        teampopScene = GetComponent<TeampopScene>();
        teampopScene.SceneInit();
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        // Debug.Log($"Teampop:OnEntry");
        teampopScene.SceneEntry();
    }
    public override void OnUpdate()
    {
        //Debug.Log($"Teampop:OnUpdate");
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     SceneManager.LoadScene("InGameload");
        //     Debug.Log($"Down Space Key" + new string('+', 15));
        // }
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     stateMachine.SetMoveFlag(MenuStateID.Select);
        //     Debug.Log($"Down Escape Key" + new string('+', 15));
        // }
        teampopScene.SceneUpdate();
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        // Debug.Log($"Teampop:OnExit");
        // Debug.Log(new string('=', 30));
        teampopScene.SceneExit();
    }

    public void EntryInGameLoad()
    {
        SceneManager.LoadScene("InGameload");
        // Debug.Log($"Down Space Key" + new string('+', 15));
    }

    public void EntrySelect()
    {
        stateMachine.SetMoveFlag(MenuStateID.Select);
        // Debug.Log($"Down Escape Key" + new string('+', 15));
    }
}