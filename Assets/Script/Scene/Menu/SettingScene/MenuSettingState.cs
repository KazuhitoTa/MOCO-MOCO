using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettingState : State<MenuStateID, MenuStateMachine>
{
    public SettingScene settingScene;
    [SerializeField] private GameObject ui;
    void Start()
    {
        ui.SetActive(false);
        settingScene = GetComponent<SettingScene>();
        settingScene.SceneInit();
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Setting:OnEntry");
        settingScene.SceneEntry();
    }
    public override void OnUpdate()
    {
        //Debug.Log($"Setting:OnUpdate");
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     stateMachine.SetBackFlag();
        //     Debug.Log($"Down Escape Key" + new string('+', 15));
        // }
        settingScene.SceneUpdate();
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        // Debug.Log($"Setting:OnExit");
        // Debug.Log(new string('=', 30));
        settingScene.SceneExit();
    }

    public void EntryBackFlag()
    {
        stateMachine.SetBackFlag();
        // Debug.Log($"Down Escape Key" + new string('+', 15));
    }
}