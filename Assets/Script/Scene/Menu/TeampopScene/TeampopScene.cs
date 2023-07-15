using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeampopScene : MenuManager
{
    public MenuTeampopState menuTeampopState;
    public Menu menu;
    /////////////////////////////////////////
    /// テスト変数
    public TMP_Text stagetext;
    public CollectionScene.UnitKind Unit1Kind, Unit2Kind;
    public GameObject Unit1, Unit2;
    public GameObject UnitEditor;
    public GameObject UnitEditorButton;
    public Text ErrorCode;

    public int UnitEditNum;

    /////////////////////////////////////////
    public override void SceneInit()
    {
        base.SceneInit();
        SetID(MenuStateID.Teampop);
        menuTeampopState = GetComponent<MenuTeampopState>();

        //UnitEditorInit();

        ///test
        Unit1Kind = CollectionScene.UnitKind.blue;
        Unit2Kind = CollectionScene.UnitKind.green;
    }

    public override void SceneEntry()
    {
        base.SceneEntry();
        SetMode(menu.GameMode);
        menu.LoadMenuStatus(SceneID);
        stagetext.text = "Stage:" + menu.selectScene.menu.SelectStageNum;
        UnitTex(Unit1, Unit1Kind);
        UnitTex(Unit2, Unit2Kind);
        //test
    }
    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }

    public override void SceneExit()
    {
        base.SceneExit();
        menu.SaveMenuStatus(Mode);
        UnitEditorExit();
    }

    private void UnitTex(GameObject Button, CollectionScene.UnitKind kind)
    {
        Button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Unit/" + kind.ToString() + 1);
    }

    public void Unit1Edit()
    {
        UnitEditor.gameObject.SetActive(true);
        UnitEditor.gameObject.transform.localPosition = new Vector3(-150, 0, 0);
        Unit2.gameObject.SetActive(false);
        UnitEditNum = 1;
    }
    public void Unit2Edit()
    {
        UnitEditor.gameObject.SetActive(true);
        UnitEditor.gameObject.transform.localPosition = new Vector3(150, 0, 0);
        Unit1.gameObject.SetActive(false);
        UnitEditNum = 2;
    }

    public void UnitEditorExit()
    {
        UnitEditor.gameObject.SetActive(false);
        Unit1.gameObject.SetActive(true);
        Unit2.gameObject.SetActive(true);
        UnitEditNum = 0;
    }

    public void UnitEditorInit()
    {
        UnitEditor.SetActive(true);

        UnitEditor.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(304, 300 * UnitNum);
        UnitKind kind = 0;
        for (int i = 0; i < UnitNum; i++)
        {
            GameObject Create = Instantiate(UnitEditorButton, Vector3.zero, Quaternion.identity);
            Create.transform.SetParent(UnitEditor.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform, false);
            RectTransform rect = Create.GetComponent<RectTransform>();
            rect.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1.0f);
            rect.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1.0f);
            Create.transform.localPosition = new Vector3(150, (-150 - (300 * i)), 0);
            Create.transform.localScale = Vector3.one;
            Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("Unit/" + kind.ToString() + 1);
            int num = i;
            UnitKind Kind = kind;
            Create.GetComponent<Button>().onClick.AddListener(() => { UnitAdd(Kind); });

            kind++;
        }

        UnitEditor.SetActive(false);
    }
    public void UnitAdd(UnitKind kind)
    {
        Debug.Log(kind);
        if(UnitEditNum == 1)
        {
            if(Unit2Kind == kind)
            {
                StartCoroutine(CannotAdd());
            }
            else
            {
                UnitTex(Unit1,kind);
                Unit1Kind = kind;
            }
        }
        else if(UnitEditNum == 2)
        {
            if(Unit1Kind == kind)
            {
                StartCoroutine(CannotAdd());
            }
            else
            {
                UnitTex(Unit2,kind);
                Unit2Kind = kind;
            }
        }

        UnitEditorExit();
    }

    public IEnumerator CannotAdd()
    {
        ErrorCode.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ErrorCode.gameObject.SetActive(false);
    }
    /////////////////////////////////////////

    public void StartInGameLoad()
    {
        menuTeampopState.EntryInGameLoad();
    }

    public void StartSelect()
    {
        menuTeampopState.EntrySelect();
    }
}
