using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryScene : MenuManager
{
    public MenuLaboratoryState menuLaboratoryState;
    public Menu menu;
    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        menuLaboratoryState = GetComponent<MenuLaboratoryState>();
        SetID(MenuStateID.Laboratory);
    }

    public override void SceneEntry()
    {
        base.SceneEntry();
        SetMode(menu.GameMode);
        menu.LoadMenuStatus(SceneID);
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

    public void StartUpgrade()
    {
        menuLaboratoryState.EntryUpgrade();
    }

    public void StartCollection()
    {
        menuLaboratoryState.EntryCollection();
    }

    public void StartSelect()
    {
        menuLaboratoryState.EntrySelect();
    }

    public void StartSetting()
    {
        menuLaboratoryState.EntrySetting();
    }
}
