using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SelectScene : MenuManager
{
    public MenuSelectState menuSelectState;
    public Menu menu;
    public GameObject Stage;
    public MenuStateID a;
    [SerializeField] GameObject PageForwardButton;
    [SerializeField] GameObject PageBackButton;
    [SerializeField] public GameObject StagePageObj;
    Vector2 cursorStartPos;
    [SerializeField] int PageSlidePower;
    [SerializeField] public GameObject OpenStagePage;
    [SerializeField] public int OpenStagePageNum;
    [SerializeField] GameObject[] Page;
    [SerializeField] int[] PageStopPos = new int[0];
    [SerializeField] int PageMoveSpeedPower;
    [SerializeField] int direction;
    /////////////////////////////////////////
    //test変数
    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        menuSelectState = GetComponent<MenuSelectState>();
        PageInit();
        SetID(MenuStateID.Select);
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
        PageUpdate();
        SetButton();
    }

    public override void SceneExit()
    {
        base.SceneExit();
        menu.SaveMenuStatus(Mode);
    }

    private void PageInit()
    {
        Array.Resize(ref Page, StagePageNum);
        for (int i = 0; i < StagePageNum; i++)
        {
            GameObject Create = Instantiate(StagePageObj, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(Stage.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            Create.transform.localScale = Vector3.one;
            Create.transform.localPosition = new Vector3(1920 * i, 0, 0);
            Create.name = "StagePage" + (i + 1);
            Page[i] = Create;

            for (int j = 0; j < 5; j++)
            {
                int num = j;
                Create.transform.GetChild(j).GetComponent<Button>().onClick.AddListener(() => { StageButton(num); });
                Create.transform.GetChild(j).gameObject.name = "Stage" + ((j + 1) + i * 5);
                Create.transform.GetChild(j).transform.GetChild(0).GetComponent<TMP_Text>().text = ((j + 1) + i * 5).ToString();
            }
        }
        Array.Resize(ref PageStopPos, Page.Length);
        for (int i = 0; i < PageStopPos.Length; i++)
        {
            PageStopPos[i] = 1920 * i;
        }

        direction = 0;
        OpenStagePageNum = 0;
        OpenStagePage = Page[OpenStagePageNum];
    }

    private void PageUpdate()
    {
        if (direction == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cursorStartPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector2 cursorFinishPos;
                cursorFinishPos = Input.mousePosition;

                float swipeLength = cursorFinishPos.x - this.cursorStartPos.x;
                // if (swipeLength < -PageSlidePower)
                // {
                //     if (OpenStagePageNum == Page.Length - 1) return;
                //     OpenStagePageNum++;
                //     OpenStagePage = Page[OpenStagePageNum];
                //     direction = -1;
                //     for (int i = 0; i < PageStopPos.Length; i++)
                //     {
                //         PageStopPos[i] -= 1920;
                //         Page[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-PageMoveSpeedPower, 0));
                //     }
                // }
                // else if (swipeLength > PageSlidePower)
                // {
                //     if (OpenStagePageNum == 0) return;
                //     OpenStagePageNum--;
                //     OpenStagePage = Page[OpenStagePageNum];
                //     direction = 1;
                //     for (int i = 0; i < PageStopPos.Length; i++)
                //     {
                //         PageStopPos[i] += 1920;
                //         Page[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(PageMoveSpeedPower, 0));
                //     }
                // }

                cursorFinishPos = new Vector2(0, 0);
                cursorStartPos = new Vector2(0, 0);
            }
        }
        else if (direction > 0)
        {
            for (int i = 0; i < Page.Length; i++)
            {
                if (Page[i].transform.localPosition.x > PageStopPos[i])
                {
                    Page[i].transform.localPosition = new Vector3(PageStopPos[i], 0, 0);
                    Vector2 zero = new Vector2(0, 0);
                    Page[i].GetComponent<Rigidbody2D>().velocity = zero;
                }
            }
            bool allstop = true;

            for (int i = 0; i < Page.Length; i++)
            {
                if (Page[i].transform.localPosition.x != PageStopPos[i])
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
            for (int i = 0; i < Page.Length; i++)
            {
                if (Page[i].transform.localPosition.x < PageStopPos[i])
                {
                    Page[i].transform.localPosition = new Vector3(PageStopPos[i], 0, 0);
                    Vector2 zero = new Vector2(0, 0);
                    Page[i].GetComponent<Rigidbody2D>().velocity = zero;
                }
            }
            bool allstop = true;

            for (int i = 0; i < Page.Length; i++)
            {
                if (Page[i].transform.localPosition.x != PageStopPos[i])
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
        if(OpenStagePageNum == 0)PageBackButton.SetActive(false);
        else PageBackButton.SetActive(true);
        if(OpenStagePageNum == StagePageNum-1)PageForwardButton.SetActive(false);
        else PageForwardButton.SetActive(true);
    }
    /////////////////////////////////////////
    
    public void StartCollection()
    {
        menuSelectState.EntryCollection();
    }

    public void StartUpgrade()
    {
        menuSelectState.EntryUpgrade();
    }

    public void StartTeampop()
    {
        menuSelectState.EntryTeampop();
    }

    public void StartSetting()
    {
        menuSelectState.EntrySetting();
        menu.settingScene.BackID = SceneID;
    }

    /// <summary>
    /// ボタン関数//test用
    /// </summary>
    /// <param name="Stage"></param>
    public void StageButton(int Stage)
    {
        menu.SelectStageNum = Stage + 1 + OpenStagePageNum * 5;
        StartTeampop();
    }
    /// <summary>
    /// ボタン関数->ページを進める  
    /// </summary>
    public void PageForward()
    {
        if (direction == 0)
        {
            if (OpenStagePageNum == Page.Length - 1) return;
            OpenStagePageNum++;
            OpenStagePage = Page[OpenStagePageNum];
            direction = -1;
            for (int i = 0; i < PageStopPos.Length; i++)
            {
                PageStopPos[i] -= 1920;
                Page[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-PageMoveSpeedPower, 0));
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
            if (OpenStagePageNum == 0) return;
            OpenStagePageNum--;
            OpenStagePage = Page[OpenStagePageNum];
            direction = 1;
            for (int i = 0; i < PageStopPos.Length; i++)
            {
                PageStopPos[i] += 1920;
                Page[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(PageMoveSpeedPower, 0));
            }
        }
    }
}
