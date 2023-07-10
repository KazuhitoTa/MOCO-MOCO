using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameLoadingState : State<InGameLoadStateID, InGameloadStateMachine>
{
    [SerializeField] private GameObject ui;
    private AsyncOperation asyncLoad;
    [SerializeField]UnitDataBase unitDataBase;
    void Start()
    {
        ui.SetActive(false);
        // asyncLoad = SceneManager.LoadSceneAsync("InGame");
        // 
        unitDataBase.Load();
    }
    public override void OnEntry()
    {
        ui.SetActive(true);
        
        Debug.Log($"Loading:OnEntry");
    }
    public override void OnUpdate()
    {
        Debug.Log($"Loading:OnUpdate");
        // Debug.Log(asyncLoad.isDone);
        // 
        if (!unitDataBase.IsLoading)SceneManager.LoadScene("InGame");
    }
    public override void OnExit()
    {
        ui.SetActive(false);
        Debug.Log($"Loading:OnExit");
        Debug.Log(new string('=', 30));
    }
}