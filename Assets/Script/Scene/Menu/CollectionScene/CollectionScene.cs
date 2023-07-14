using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectionScene : MenuManager
{
    public MenuCollectionState menuCollectionState;
    public Menu menu;
    public Book book;
    [SerializeField] GameObject PageForwardButton;
    [SerializeField] GameObject PageBackButton;
    [SerializeField]private Texture2D CoverPageTexture, BackCoverPageTexture;
    [SerializeField]private Texture2D SpreadCoverPageTexture;
    [SerializeField]private Texture2D[] SpreadPageTexture;
    [SerializeField]private Texture2D[] PageTexture;
    private int PageNum = (Enum.GetNames(typeof(UnitKind)).Length);

    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        menuCollectionState = GetComponent<MenuCollectionState>();
        PageTextureLoad();
        menuCollectionState.ui.SetActive(true);
        book.CreatePage(PageNum);
        PageInput();
        book.BookInit();
        menuCollectionState.ui.SetActive(false);
        SetID(MenuStateID.Collection);

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
        book.BookUpdate();
        SetButton();
    }

    public override void SceneExit()
    {
        base.SceneExit();
        menu.SaveMenuStatus(Mode);
    }
    
    private void PageTextureLoad()
    {
        if (CreateHalfCoverPageTexture) SpreadCoverPageTexture = Resources.Load<Texture2D>("CollectionScene/SpreadCoverPage");
        else
        {
            CoverPageTexture = Resources.Load<Texture2D>("CollectionScene/CoverPage");
            BackCoverPageTexture = Resources.Load<Texture2D>("CollectionScene/BackCoverPage");
        }
        if (CreateHalfPageTexture)
        {
            Array.Resize(ref SpreadPageTexture, PageNum);
            for (int i = 0; i < PageNum; i++)
            {
                SpreadPageTexture[i] = Resources.Load<Texture2D>("CollectionScene/SpreadPage" + (i+1));
            }
        }
        else
        {
            Array.Resize(ref PageTexture, PageNum * 2 + 2);
            for (int i = 1; i <= PageNum * 2; i++)
            {
                PageTexture[i] = Resources.Load<Texture2D>("CollectionScene/Page" + i);
            }
        }
    }

    public void PageInput()
    {
        if (CreateHalfCoverPageTexture)
        {
            if (CreateHalfPageTexture)
            {
                for (int i = 0; i < book.ConcatenatedPage.Length; i++)
                {
                    if (i == 0) book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadCoverPageTexture;
                    else book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadPageTexture[i - 1];
                    book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0.5f, 0f);
                    book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.5f, 1f);
                    if (i == book.ConcatenatedPage.Length - 1) book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadCoverPageTexture;
                    else book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadPageTexture[i];
                    book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0f);
                    book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.5f, 1f);
                }
            }
            else
            {
                for (int i = 0; i < book.ConcatenatedPage.Length; i++)
                {
                    if (i == 0) book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadCoverPageTexture;
                    else book.Page[i].GetComponent<Renderer>().material.mainTexture = PageTexture[i];
                    if (i == book.ConcatenatedPage.Length - 1) book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadCoverPageTexture;
                    else book.Page[i].GetComponent<Renderer>().material.mainTexture = PageTexture[i];
                }
            }
        }
        else
        {
            if (CreateHalfPageTexture)
            {
                for (int i = 0; i < book.ConcatenatedPage.Length; i++)
                {
                    if (i == 0)
                    {
                        book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = CoverPageTexture;
                    }
                    else
                    {
                        book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadPageTexture[i - 1];
                        book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0.5f, 0f);
                        book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.5f, 1f);
                    }
                    if (i == book.ConcatenatedPage.Length - 1)
                    {
                        book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = BackCoverPageTexture;
                    }
                    else
                    {
                        book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = SpreadPageTexture[i];
                        book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0f);
                        book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.5f, 1f);
                    }
                }
            }
            else
            {
                for (int i = 0; i < book.ConcatenatedPage.Length; i++)
                {
                    if (i == 0)
                    {
                        book.ConcatenatedPage[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = CoverPageTexture;
                    }
                    else
                    {
                        book.Page[i].GetComponent<Renderer>().material.mainTexture = PageTexture[i];
                    }
                    if (i == book.ConcatenatedPage.Length - 1)
                    {
                        book.ConcatenatedPage[i].transform.GetChild(1).gameObject.GetComponent<Renderer>().material.mainTexture = BackCoverPageTexture;
                    }
                    else
                    {
                        book.Page[i].GetComponent<Renderer>().material.mainTexture = PageTexture[i];
                    }
                }
            }
        }
    }

    private void SetButton()
    {
        if(book.OpenPageNum == 0)PageBackButton.SetActive(false);
        else PageBackButton.SetActive(true);
        if(book.OpenPageNum == PageNum+1)PageForwardButton.SetActive(false);
        else PageForwardButton.SetActive(true);
    }

    /// <summary>
    /// Prefab:PageLeftContentと一貫性を持たせること<br/>
    /// LeftPage.transform.GetChild(0).gameObjectでPageLeftContent子オブジェクト一位（最上位）参照<br/>
    /// gameObject省略可
    /// </summary>
    /// <param name="LeftPage">親オブジェクト</param>
    public void LeftPageContentEdit(GameObject LeftPage, UnitKind Kind)
    {
        for (int i = 0; i < 5; i++)
        {
            LeftPage.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(Kind.ToString() + (i + 1));
        }
    }

    /// <summary>
    /// Prefab:PageRightContentと一貫性を持たせること<br/>
    /// RightPage.transform.GetChild(0).gameObjectでPageRightContent子オブジェクト一位（最上位）参照<br/>
    /// gameObject省略可
    /// </summary>
    /// <param name="RightPage">親オブジェクト</param>
    public void RightPageContentEdit(GameObject RightPage)
    {

    }
    /////////////////////////////////////////

    public void StartUpgrade()
    {
        menuCollectionState.EntryUpgrade();
    }

    public void StartLaboratory()
    {
        menuCollectionState.EntryLaboratory();
    }

    public void StartSelect()
    {
        menuCollectionState.EntrySelect();
    }

    public void StartSetting()
    {
        menuCollectionState.EntrySetting();
        menu.settingScene.BackID = MenuStateID.Collection;
    }

    public void PageForward()
    {
        book.PageForward();
    }

    public void PageBack()
    {
        book.PageBack();
    }
}
