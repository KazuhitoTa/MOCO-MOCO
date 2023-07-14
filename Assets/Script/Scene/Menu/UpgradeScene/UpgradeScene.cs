using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UpgradeScene : MenuManager
{
    //<必須変数
    public MenuUpgradeState menuUpgradeState;
    public Menu menu;
    public DateTime NowTime;

    [SerializeField] GameObject PageForwardButton;
    [SerializeField] GameObject PageBackButton;
    public GameObject UpgradeObj;
    public GameObject UpgradeView;
    public GameObject UnitEditor;
    public GameObject ItemEditor;
    public GameObject EditorButton;
    public GameObject[] UpgradeMachine;
    public UpgradeMachine[] upgradeMachine;
    public int UpgradeMachineNum;
    public int UpgradePageNum;
    public int OpenUpgradePageNum;
    public GameObject[] UpgradePage;
    public GameObject OpenUpgradePage;
    public int SelectUpgradeMachine;
    public bool[] AlreadySetUnit;
    [SerializeField] int PageSlidePower;
    [SerializeField] int[] PageStopPos = new int[0];
    [SerializeField] int PageMoveSpeedPower;
    [SerializeField] int direction;

    public enum UpgradeMachineStatus
    {
        UnReleased,
        UnUnitSet,
        UpgradeNow,
        UpgradeFinish,
    }

    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        SetID(MenuStateID.Upgrade);
        NowTime = DateTime.Now;
        menuUpgradeState = GetComponent<MenuUpgradeState>();
        UpgradePageInit();
        //UnitEditorInit();
        //ItemEditorInit();
        UpgradeMachineLoad();

        UpgradeMachineInit();    //仮置き
    }

    public override void SceneEntry()
    {
        base.SceneEntry();
        SetMode(menu.GameMode);
        menu.LoadMenuStatus(SceneID);
        UnitEditor.SetActive(false);
        ItemEditor.SetActive(false);
        NowTime = DateTime.Now;

        UpgradeMachineRestart();

        LockUnitButton();
    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
        //<時間取得>//
        NowTime = DateTime.Now;
        UpgradeMachineUpdate();
        PageUpdate();
        SetButton();
    }

    public override void SceneExit()
    {
        base.SceneExit();
        menu.SaveMenuStatus(Mode);
        UpgradeMachineQuit();
        UnitEditorQuit();
        ItemEditorQuit();
    }

    private void UpgradePageInit()
    {
        if (UpgradePageNum < 1) UpgradePageNum = 1;
        UpgradeMachineNum = UpgradePageNum * 3;
        Array.Resize(ref UpgradePage, UpgradePageNum);
        Array.Resize(ref UpgradeMachine, UpgradeMachineNum + 1);
        Array.Resize(ref upgradeMachine, UpgradeMachineNum + 1);
        for (int i = 0; i < UpgradePageNum; i++)
        {
            GameObject Create = Instantiate(UpgradeObj, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(UpgradeView.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            Create.transform.localPosition = new Vector3(0 + i * 1920, 0, 0);
            Create.transform.rotation = Quaternion.Euler(0, 0, 0);
            Create.transform.localScale = Vector3.one;
            UpgradePage[i] = Create;
            for (int j = 0; j < 3; j++)
            {
                int num = i * 3 + j + 1;
                Create.transform.GetChild(j).transform.localPosition = new Vector3(-500 + 500 * j, -200, 0);
                Create.transform.GetChild(j).gameObject.name = "UpgradeMachine" + (num);
                UpgradeMachine[num] = Create.transform.GetChild(j).gameObject;
                upgradeMachine[num] = Create.transform.GetChild(j).gameObject.GetComponent<UpgradeMachine>();
                upgradeMachine[num].upgradeScene = this;
                upgradeMachine[num].Start.onClick.AddListener(() => { StartButton(num); });
                upgradeMachine[num].Reset.onClick.AddListener(() => { ResetButton(num); });
                upgradeMachine[num].Shoten.onClick.AddListener(() => { ShotenButton(num); });
                upgradeMachine[num].UnitButton.onClick.AddListener(() => { AddUnit(num); });
                upgradeMachine[num].UnitButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UpgradeScene/plus");
            }
        }

        Array.Resize(ref PageStopPos, UpgradePage.Length);
        for (int i = 0; i < PageStopPos.Length; i++)
        {
            PageStopPos[i] = 1920 * i;
        }

        direction = 0;
        OpenUpgradePageNum = 0;
        OpenUpgradePage = UpgradePage[OpenUpgradePageNum];
    }

    private void UpgradeMachineInit()
    {
        for (int i = 1; i <= UpgradeMachineNum; i++)
        {
            upgradeMachine[i].MachineInit();
        }
    }

    private void UpgradeMachineRestart()
    {
        for (int i = 1; i <= UpgradeMachineNum; i++)
        {
            upgradeMachine[i].UpgradeReStart();
        }
    }

    private void UpgradeMachineUpdate()
    {
        for (int i = 1; i <= UpgradeMachineNum; i++)
        {
            upgradeMachine[i].UpgradeUpdate();
        }
    }

    private void UpgradeMachineQuit()
    {
        for (int i = 1; i <= UpgradeMachineNum; i++)
        {
            upgradeMachine[i].MachineQuit();
        }
    }

    public void UnitEditorInit()
    {
        UnitEditor.SetActive(true);

        UnitEditor.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(304, 300 * UnitNum);
        UnitKind kind = 0;
        for (int i = 0; i < UnitNum; i++)
        {
            GameObject Create = Instantiate(EditorButton, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(UnitEditor.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1.0f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1.0f);
            Create.transform.localPosition = new Vector3(150, (-150 - (300 * i)), 0);
            Create.transform.localScale = Vector3.one;
            Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("Unit/" + kind.ToString() + 1);
            UnitKind Kind = kind;
            Create.GetComponent<Button>().onClick.AddListener(() => { UnitButton(Kind); });

            kind++;
        }

        UnitEditor.SetActive(false);
    }

    public void ItemEditorInit()
    {
        ItemEditor.SetActive(true);
        ItemEditor.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(304, 300 * ItemNum);
        ItemKind kind = 0;
        for (int i = 0; i < ItemNum; i++)
        {
            GameObject Create = Instantiate(EditorButton, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(ItemEditor.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1.0f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1.0f);
            Create.transform.localPosition = new Vector3(150, (-150 - (300 * i)), 0);
            Create.transform.localScale = Vector3.one;
            int num =  i + 1;
            Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/item" + (num));
            ItemKind Kind = kind;
            Create.GetComponent<Button>().onClick.AddListener(() => { ItemButton(Kind); });

            kind++;
        }

        ItemEditor.SetActive(false);
    }

    private void UnitEditorQuit()
    {
        UnitEditor.gameObject.SetActive(false);
        SelectUpgradeMachine = 0;
    }
    private void ItemEditorQuit()
    {
        ItemEditor.gameObject.SetActive(false);
        SelectUpgradeMachine = 0;
    }

    private void LockUnitButton()
    {
        Array.Resize(ref AlreadySetUnit, UnitNum);
        for (int i = 1; i <= UpgradeMachineNum; i++)
        {
            if (upgradeMachine[i].SetUnit)
            {
                AlreadySetUnit[(int)upgradeMachine[i].UpgradeUnitKind] = true;
            }
        }
    }

    public void TestInit()
    {
        upgradeMachine[1].testInit_Type1(); ////test
        upgradeMachine[2].testInit_Type1(); ////test
    }

    public void UpgradeMachineLoad()
    {
        TestInit();
    }



    /// <summary>
    /// アップグレードでレベルアップさせたいに能力変化させるステータスを入れる
    /// </summary>
    public void StatusUpgrade()
    {

    }







    private void PageUpdate()
    {
        if (direction == 0)
        {
            
        }
        else if (direction > 0)
        {
            for (int i = 0; i < UpgradePage.Length; i++)
            {
                if (UpgradePage[i].transform.localPosition.x > PageStopPos[i])
                {
                    UpgradePage[i].transform.localPosition = new Vector3(PageStopPos[i], 0, 0);
                    Vector2 zero = new Vector2(0, 0);
                    UpgradePage[i].GetComponent<Rigidbody2D>().velocity = zero;
                }
            }
            bool allstop = true;

            for (int i = 0; i < UpgradePage.Length; i++)
            {
                if (UpgradePage[i].transform.localPosition.x != PageStopPos[i])
                {
                    allstop = false;
                }
            }

            if (allstop)
            {
                direction = 0;
            }
        }
        else
        {
            for (int i = 0; i < UpgradePage.Length; i++)
            {
                if (UpgradePage[i].transform.localPosition.x < PageStopPos[i])
                {
                    UpgradePage[i].transform.localPosition = new Vector3(PageStopPos[i], 0, 0);
                    Vector2 zero = new Vector2(0, 0);
                    UpgradePage[i].GetComponent<Rigidbody2D>().velocity = zero;
                }
            }
            bool allstop = true;

            for (int i = 0; i < UpgradePage.Length; i++)
            {
                if (UpgradePage[i].transform.localPosition.x != PageStopPos[i])
                {
                    allstop = false;
                }
            }

            if (allstop)
            {
                direction = 0;
            }
        }
    }

    private void SetButton()
    {
        if(OpenUpgradePageNum == 0)PageBackButton.SetActive(false);
        else PageBackButton.SetActive(true);
        if(OpenUpgradePageNum == UpgradePageNum-1)PageForwardButton.SetActive(false);
        else PageForwardButton.SetActive(true);
    }

    /////////////////////////////////////////

    public void StartLaboratory()
    {
        menuUpgradeState.EntryLaboratory();
    }

    public void StartCollection()
    {
        menuUpgradeState.EntryCollection();
    }

    public void StartSelect()
    {
        menuUpgradeState.EntrySelect();
    }

    public void StartSetting()
    {
        menuUpgradeState.EntrySetting();
        menu.settingScene.BackID = MenuStateID.Upgrade;
    }


    /////////////////////////////////////////
    /// 

    public void UnitEditorExit()
    {
        UnitEditorQuit();
    }

    public void ItemEditorExit()
    {
        ItemEditorQuit();
    }

    public void StartButton(int num)
    {
        upgradeMachine[num].UpgradeStart();
    }

    public void ResetButton(int num)
    {
        upgradeMachine[num].UpgradeReset();
    }

    public void ShotenButton(int num)
    {
        upgradeMachine[num].UpgradeShoten(5);
    }

    public void AddUnit(int num)
    {
        if (upgradeMachine[num].MachineStatus == UpgradeMachineStatus.UnUnitSet)
        {
            UnitEditor.gameObject.SetActive(true);
            UnitEditor.gameObject.transform.localPosition = new Vector3(upgradeMachine[num].transform.localPosition.x + 150, 0, 0);
            SelectUpgradeMachine = num;

            upgradeMachine[num].AddUnitState();
        }
        else if (upgradeMachine[num].MachineStatus == UpgradeMachineStatus.UpgradeNow)
        {
            ItemEditor.gameObject.SetActive(true);
            ItemEditor.gameObject.transform.localPosition = new Vector3(upgradeMachine[num].transform.localPosition.x + 150, 0, 0);
            SelectUpgradeMachine = num;
        }
        else if (upgradeMachine[num].MachineStatus == UpgradeMachineStatus.UpgradeFinish)
        {
            upgradeMachine[num].Havest();
            upgradeMachine[num].UnitButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UpgradeScene/plus");
        }
    }

    public void UnitButton(UnitKind kind)
    {
        //if (upgradeMachine[SelectUpgradeMachine].SetUnit) AlreadySetUnit[(int)upgradeMachine[SelectUpgradeMachine].UpgradeUnitKind] = false;
        if (AlreadySetUnit[(int)kind]) return;
        Debug.Log(kind);
        upgradeMachine[SelectUpgradeMachine].UnitImage.sprite = Resources.Load<Sprite>("Unit/" + kind.ToString() + 1);
        upgradeMachine[SelectUpgradeMachine].UnitButton.transform.GetChild(0).gameObject.SetActive(false);
        upgradeMachine[SelectUpgradeMachine].SetUnit = true;
        upgradeMachine[SelectUpgradeMachine].UpgradeUnitKind = kind;
        upgradeMachine[SelectUpgradeMachine].Start.gameObject.SetActive(true);
        upgradeMachine[SelectUpgradeMachine].Shoten.gameObject.SetActive(true);
        upgradeMachine[SelectUpgradeMachine].Reset.gameObject.SetActive(true);
        upgradeMachine[SelectUpgradeMachine].UpgradeStart();
        AlreadySetUnit[(int)kind] = true;
        UnitEditorExit();
    }

    public void ItemButton(ItemKind kind)
    {
        if(menu.Items[(int)kind].num <= 0)return;
        upgradeMachine[SelectUpgradeMachine].UpgradeShoten(2*(1+(int)kind));
        menu.Items[(int)kind].num--;
        ItemEditorExit();
    }

    /// <summary>
    /// ボタン関数->ページを進める  
    /// </summary>
    public void PageForward()
    {
        if (direction == 0)
        {
            if (OpenUpgradePageNum == UpgradePage.Length - 1) return;
            OpenUpgradePageNum++;
            OpenUpgradePage = UpgradePage[OpenUpgradePageNum];
            direction = -1;
            for (int i = 0; i < PageStopPos.Length; i++)
            {
                PageStopPos[i] -= 1920;
                UpgradePage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-PageMoveSpeedPower, 0));
            }
        }
    }

    /// <summary>
    /// ボタン関数->ページを戻す  
    /// </summary>
    public void PageBack()
    {
        if (direction == 0)
        {
            if (OpenUpgradePageNum == 0) return;
            OpenUpgradePageNum--;
            OpenUpgradePage = UpgradePage[OpenUpgradePageNum];
            direction = 1;
            for (int i = 0; i < PageStopPos.Length; i++)
            {
                PageStopPos[i] += 1920;
                UpgradePage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(PageMoveSpeedPower, 0));
            }
        }
    }
}
