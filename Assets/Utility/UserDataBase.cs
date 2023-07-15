using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Utility/UserDataBase.cs
[Serializable]
public class StageRecord
{
    [SerializeField] private string idString;
    [SerializeField] public StageID ID;
    [SerializeField] public bool Flag;
    [SerializeField] public int HighScore;
    [SerializeField] public int Stars;
    public StageRecord(StageID id, bool flag, int highScore, int stars)
    {
        ID = id;
        idString = id.ToString();
        Flag = flag;
        HighScore = highScore;
        Stars = stars;
    }
}

public class UserDataBase : DataBase<UserDataBase>
{
    protected override string FileName => "user_data.bin";
    protected override string Path => @$"{Application.dataPath}/{FileName}";
    protected override string Key => "01234567890123456789012345678912";
    [Serializable]
    private class Wrap
    {
        public float bgmVolume = 0.5f;
        public float seVolume = 0.5f;
        public bool isShowDamage = true;
        public string selectedUnit1 = UnitGroup.Green.ToString();
        public string selectedUnit2 = UnitGroup.Green.ToString();
        public List<StageRecord> rowList;
    }
    private Wrap wrap;

    /// <summary>
    /// BGM音量
    /// </summary>
    /// <value></value>
    public float BGMVolume
    {
        get => wrap.bgmVolume;
        set { wrap.bgmVolume = Math.Clamp(value, 0, 1); isEdited = true; }
    }
    
    /// <summary>
    /// SE音量
    /// </summary>
    /// <value></value>
    public float SEVolume
    {
        get => wrap.seVolume;
        set { wrap.seVolume = Math.Clamp(value, 0, 1); isEdited = true; }
    }
    
    /// <summary>
    /// ダメージ表示
    /// </summary>
    /// <value></value>
    public bool IsShowDamage
    {
        get => wrap.isShowDamage;
        set { wrap.isShowDamage = value; isEdited = true; }
    }

    /// <summary>
    /// 選択Unit1
    /// </summary>
    /// <value></value>
    public UnitGroup SelectedUnit1
    {
        get => EnumConverter.ConvertUnitGroup(wrap.selectedUnit1);
        set { wrap.selectedUnit1 = value.ToString(); isEdited = true; }
    }
    
    /// <summary>
    /// 選択Unit2
    /// </summary>
    /// <value></value>
    public UnitGroup SelectedUnit2
    {
        get => EnumConverter.ConvertUnitGroup(wrap.selectedUnit2);
        set { wrap.selectedUnit2 = value.ToString(); isEdited = true; }
    }
    
    private Dictionary<StageID, int> stageIDToIndex;
    void Start()
    {
        wrap = new Wrap();
        wrap.rowList = new();
        stageIDToIndex = new();
        isDump = true;
        //step=-1;
    }

    void Update()
    {
        #if UNITY_EDITOR
        switch (step)
        {
            case -1:
                foreach (StageID id in Enum.GetValues(typeof(StageID)))
                {
                    wrap.rowList.Add(new StageRecord(id, (id == StageID.Stage1), 0, 0));
                }
                step++;
                break;
            case 0:
                SaveAsync();
                step++;
                break;
            case 1:
                if (!IsSaveing) step++;
                break;
            case 2:
                LoadAsync();
                step++;
                break;
            case 3:
                if (!IsLoading) step++;
                break;
            default:
                // Debug.Log(GetFlag(StageID.Stage1));
                // Debug.Log(GetFlag(StageID.Stage2));
                // Debug.Log(GetHighScore(StageID.Stage1));
                // Debug.Log(GetHighScore(StageID.Stage2));
                // Debug.Log(GetStars(StageID.Stage1));
                // Debug.Log(GetStars(StageID.Stage2));
                break;
        }
        #endif
    }
    
    /// <summary>
    /// 非同期でユーザーデータを読み込み
    /// </summary>
    /// <returns></returns>
    public override async void LoadAsync()
    {
        string json = await loadAsync();
        wrap = JsonUtility.FromJson<Wrap>(json);
        int index = 0;
        foreach (var data in wrap.rowList)
        {
            if (stageIDToIndex.ContainsKey(data.ID)) continue;
            stageIDToIndex.Add(data.ID, index++);
        }
    }

    /// <summary>
    /// 非同期でユーザーデータを書き込む
    /// </summary>
    /// <returns></returns>
    public override async void SaveAsync()
    {
        string json = JsonUtility.ToJson(wrap);
        await saveAsync(json);
    }

    public bool GetFlag(StageID id) => wrap.rowList[stageIDToIndex[id]].Flag;
    public void SetFlag(StageID id, bool flg) => wrap.rowList[stageIDToIndex[id]].Flag = flg;
    public int GetHighScore(StageID id) => wrap.rowList[stageIDToIndex[id]].HighScore;
    public void SetHighScore(StageID id, int score) => wrap.rowList[stageIDToIndex[id]].HighScore = score;
    public int GetStars(StageID id) => wrap.rowList[stageIDToIndex[id]].Stars;
    public void SetStars(StageID id, int stars) => wrap.rowList[stageIDToIndex[id]].Stars = stars;
=======
/// <summary>
/// 
/// </summary>
public sealed class UserDataBase : DataBase<UserDataBase>
{
    protected override string FileName => "user_data.json";
    protected override string Path => Application.dataPath + "\\" + FileName;
    protected override string Key => "";
    protected override string IV => "";
>>>>>>> parent of add5387 (Merge pull request #2 from KazuhitoTa/vbgh):Assets/Script/Utility/UserDataBase.cs
}