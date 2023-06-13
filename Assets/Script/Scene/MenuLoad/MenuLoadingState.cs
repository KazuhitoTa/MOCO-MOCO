using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadingState : State<MenuLoadStateID, MenuloadStateMachine>
{
    [SerializeField] private GameObject ui;
    private AsyncOperation asyncLoad;
    void Start()
    {
        ui.SetActive(false);
        asyncLoad = SceneManager.LoadSceneAsync("Menu");
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        Debug.Log($"Loading:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Loading:OnUpdate");
        Debug.Log(asyncLoad.isDone);
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Loading:OnExit");
        Debug.Log(new string('=', 30));
    }
}