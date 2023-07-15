using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Menu : MenuStatus
{

    void Start()
    {
        LoadUnitStatus();
        LoadItemStatus();
        upgradeScene.UnitEditorInit();
        upgradeScene.ItemEditorInit();
        teampopScene.UnitEditorInit();
    }

    private void LoadUnitStatus()
    {
        for(int i = 0; i < Units.Length; i++)
        {
            MenuManager.UnitKind kind = (MenuManager.UnitKind)Enum.ToObject(typeof(MenuManager.UnitKind), i);
            Units[i].name = kind.ToString();
            Units[i].lv = 0;
            Units[i].hp = 10;

            for(int j = 0; j < 5; j++)
            {
                Units[i].sprites[j] = Resources.Load<Sprite>("Unit/" + kind.ToString() + (j+1));
            }
        }
    }
    
    private void LoadItemStatus()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            MenuManager.ItemKind kind = (MenuManager.ItemKind)Enum.ToObject(typeof(MenuManager.ItemKind), i);
            Items[i].name = kind.ToString();
            Items[i].num = 0;
            Items[i].sprite = Resources.Load<Sprite>("Item/item" + (i+1));
        }
    }

    public void LoadMenuStatus(MenuStateID SceneID)
    {
        OpenSceneID = SceneID;
    }

    public void MenuUpdate(MenuManager.GameMode Mode)
    {
        GameMode = Mode;
    }

    public void SaveMenuStatus(MenuManager.GameMode Mode)
    {
        GameMode = Mode;
    }
}
