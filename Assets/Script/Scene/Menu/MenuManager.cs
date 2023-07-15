using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    //<SelectScene<ステージ選択シーン>>
    protected int StagePageNum = 2;
    //<CollectionScene<研究ノートシーン>>
    protected bool CreateHalfCoverPageTexture = false;
    protected bool CreateHalfPageTexture = true;
    //<共通>
    [SerializeField] protected MenuStateID SceneID = (MenuStateID)Enum.ToObject(typeof(MenuStateID), (Enum.GetNames(typeof(MenuStateID)).Length) + 1);
    public enum GameMode
    {
        Game,
        Debug
    }
    [SerializeField] public GameMode Mode = GameMode.Game;
    public enum UnitKind
    {
        blue,
        green,
        red,
        yellow
    }
    public int UnitNum = (Enum.GetNames(typeof(UnitKind)).Length);
    public class GameItem
    {
        
    }
    public enum ItemKind
    {
        a,
        b,
        c
    }

    public int ItemNum = (Enum.GetNames(typeof(ItemKind)).Length);

    public virtual void SceneInit()
    {
    }

    public virtual void SceneEntry()
    {

    }

    public virtual void SceneUpdate()
    {
        Escape();
        ModeChanege();
    }

    public virtual void SceneExit()
    {

    }


    public void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }

    public void ModeChanege()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Mode = GameMode.Game;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Mode = GameMode.Debug;
        }
        //if(Mode == GameMode.Game)Debug.Log("s");
    }

    public void SetID(MenuStateID ID)
    {
        SceneID = ID;
    }

    public void SetMode(GameMode gameMode)
    {
        Mode = gameMode;
    }
}