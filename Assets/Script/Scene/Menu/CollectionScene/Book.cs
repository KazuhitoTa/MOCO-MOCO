using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Book : MonoBehaviour
{
    public CollectionScene collectionScene;
    public GameObject CollectionBook;
    public Quaternion TargetRotationForward;
    public Quaternion TargetRotationBack;

    public GameObject ConcatenatedPageObj;
    public GameObject LeftPageContent;
    public GameObject RightPageContent;
    public GameObject[] ConcatenatedPage;
    public GameObject[] Page;

    public GameObject OpenPageLeft;
    public GameObject OpenPageRight;
    public GameObject MovePage;

    public int OpenPageNum;
    public int OpenPageLeftNum;
    public int OpenPageRightNum;
    public float PageEular;
    public float EularPlusValue;
    public bool PageMove;
    public int Direction = 0;


    public void CreatePage(int PageNum)
    {
        Array.Resize(ref Page, PageNum * 2 + 2);
        Array.Resize(ref ConcatenatedPage, PageNum + 1);
        CreatePage();
    }

    public void CreatePage()
    {
        for (int i = 0; i < ConcatenatedPage.Length; i++)
        {
            GameObject Create = Instantiate(ConcatenatedPageObj, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(CollectionBook.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            Create.transform.localScale = Vector3.one;
            //Create.transform.localPosition = new Vector3(400,0,0);
            Create.transform.GetChild(0).transform.localScale = new Vector3(800, 900, 1);
            Create.transform.GetChild(1).transform.localScale = new Vector3(800, 900, 1);
            Create.transform.GetChild(0).transform.localPosition = new Vector3(400, 0, 0);
            Create.transform.GetChild(1).transform.localPosition = new Vector3(400, 0, 0);
            Create.name = "ConcatenatedPage" + i;
            Create.transform.GetChild(0).gameObject.name = "Page" + (i * 2);
            Create.transform.GetChild(1).gameObject.name = "Page" + (i * 2 + 1);
            Page[i * 2] = Create.transform.GetChild(0).gameObject;
            Page[i * 2 + 1] = Create.transform.GetChild(1).gameObject;
            ConcatenatedPage[i] = Create;
            if(i != 0)ConcatenatedPage[i].transform.localPosition = new Vector3(0,0,1);
        }

        PageContentInput();
    }

    public void PageContentInput()
    {
        CollectionScene.UnitKind kind = CollectionScene.UnitKind.blue;
        for (int i = 0; i < ConcatenatedPage.Length; i++)
        {
            if (i != ConcatenatedPage.Length - 1)
            {
                GameObject Create = Instantiate(LeftPageContent, Vector3.zero, Quaternion.identity);
                Create.transform.SetParent(ConcatenatedPage[i].transform, false);
                RectTransform rect = Create.GetComponent<RectTransform>();
                rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                Create.transform.localPosition = new Vector3(400, 0, 0.1f);
                Create.transform.rotation = Quaternion.Euler(0, 180, 0);
                Create.transform.localScale = Vector3.one;
                collectionScene.LeftPageContentEdit(Create,kind);
            }
            if (i != 0)
            {
                GameObject Create = Instantiate(RightPageContent, Vector3.zero, Quaternion.identity);
                Create.transform.SetParent(ConcatenatedPage[i].transform, false);
                RectTransform rect = Create.GetComponent<RectTransform>();
                rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                Create.transform.localPosition = new Vector3(400, 0, -0.1f);
                Create.transform.rotation = Quaternion.Euler(0, 0, 0);
                Create.transform.localScale = Vector3.one;
                collectionScene.RightPageContentEdit(Create);
            }
            kind++;
        }
    }

    public void BookInit()
    {
        OpenPageLeftNum = -1;
        OpenPageRightNum = 0;
        OpenPageNum = 0;
        OpenPageRight = ConcatenatedPage[OpenPageRightNum];

        TargetRotationForward = Quaternion.Euler(0, 180, 0);
        TargetRotationBack = Quaternion.Euler(0, 0, 0);
        PageMove = false;
    }

    public void BookUpdate()
    {
        if (Direction == 1)
        {
            PageEular += EularPlusValue;
            MovePage.transform.rotation = Quaternion.Euler(0, PageEular, 0);
            if (PageEular > 180)
            {
                MovePage.transform.rotation = Quaternion.Euler(0, 180, 0);
                Direction = 0;
                PageMove = false;
            }
        }
        else if (Direction == -1)
        {
            PageEular -= EularPlusValue;
            MovePage.transform.rotation = Quaternion.Euler(0, PageEular, 0);
            if (PageEular < 0)
            {
                MovePage.transform.rotation = Quaternion.Euler(0, 0, 0);
                Direction = 0;
                PageMove = false;
            }
        }
    }

    public void PageForward()
    {
        if (PageMove) return;
        if (OpenPageRightNum == ConcatenatedPage.Length) return;
        if (OpenPageLeftNum != -1) OpenPageLeft.transform.localPosition = new Vector3(0, 0, 1);
        //ConcatenatedPage[OpenPageRightNum].transform.localRotation = TargetRotationForward;
        PageMove = true;
        PageEular = 0;
        Direction = 1;
        MovePage = OpenPageRight;
        OpenPageLeftNum++;
        OpenPageRightNum++;
        OpenPageNum++;
        OpenPageLeft = ConcatenatedPage[OpenPageLeftNum];
        if (OpenPageLeftNum - 2 >= 0) ConcatenatedPage[OpenPageLeftNum - 2].transform.localPosition = new Vector3(0, 0, 2);
        if (OpenPageRightNum == ConcatenatedPage.Length) return;
        OpenPageRight = ConcatenatedPage[OpenPageRightNum];
        OpenPageRight.transform.localPosition = new Vector3(0,0,0);
    }

    public void PageBack()
    {
        if (PageMove) return;
        if (OpenPageLeftNum == -1) return;
        if (OpenPageRightNum != ConcatenatedPage.Length)OpenPageRight.transform.localPosition = new Vector3(0,0,1);
        OpenPageLeft.transform.localPosition = new Vector3(0, 0, 0);
        ConcatenatedPage[OpenPageLeftNum].transform.localRotation = TargetRotationBack;
        if (OpenPageLeftNum - 1 != -1) ConcatenatedPage[OpenPageLeftNum - 1].transform.localPosition = new Vector3(0, 0, 1);
        PageMove = true;
        PageEular = 180;
        Direction = -1;
        MovePage = OpenPageLeft;
        OpenPageLeftNum--;
        OpenPageRightNum--;
        OpenPageNum--;
        OpenPageRight = ConcatenatedPage[OpenPageRightNum];
        if (OpenPageLeftNum == -1) return;
        OpenPageLeft = ConcatenatedPage[OpenPageLeftNum];
    }
}
