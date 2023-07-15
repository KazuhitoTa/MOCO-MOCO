using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingScene : MenuManager
{
    public MenuSettingState menuSettingState;
    public Menu menu;
    public GameObject BackButton;
    /////////////////////////////////////////
    /// テスト変数
    [SerializeField]public MenuStateID BackID;
    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        SetID(MenuStateID.Setting);
        menuSettingState = GetComponent<MenuSettingState>();
    }

    public override void SceneEntry()
    {
        base.SceneEntry();
        SetMode(menu.GameMode);
        menu.LoadMenuStatus(SceneID);
        BackButton.transform.GetChild(0).GetComponent<TMP_Text>().text = BackID.ToString();
    }
    
    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }

    public override void SceneExit()
    {
        base.SceneExit();
        menu.SaveMenuStatus(Mode);
    }
    /////////////////////////////////////////

    public void StartBackFlag()
    {
        menuSettingState.EntryBackFlag();
    }
}
