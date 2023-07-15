using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuStatus : MonoBehaviour
{
    [SerializeField] public CollectionScene collectionScene;
    [SerializeField] public LaboratoryScene laboratoryScene;
    [SerializeField] public SelectScene selectScene;
    [SerializeField] public SettingScene settingScene;
    [SerializeField] public TeampopScene teampopScene;
    [SerializeField] public UpgradeScene upgradeScene;
    [SerializeField]protected int DebugMoney = 100000;
    [SerializeField]public int SelectStageNum = 0;
    [SerializeField]public MenuManager.GameMode GameMode;
    [SerializeField]protected MenuStateID OpenSceneID = (MenuStateID)Enum.ToObject(typeof(MenuStateID), (Enum.GetNames(typeof(MenuStateID)).Length) + 1);
    [Serializable]
    public class UnitStatus
    {
        public string name;
        public int lv;
        public int hp;
        public int GrowTime;
        public Sprite[] sprites = new Sprite[5];
    }

    [SerializeField]public UnitStatus[] Units = new UnitStatus[(Enum.GetNames(typeof(MenuManager.UnitKind)).Length)];

    [Serializable]
    public class ItemStatus
    {
        public string name;
        public int num;
        public Sprite sprite;
    }

    [SerializeField]public ItemStatus[] Items = new ItemStatus[(Enum.GetNames(typeof(MenuManager.ItemKind)).Length)];
}
