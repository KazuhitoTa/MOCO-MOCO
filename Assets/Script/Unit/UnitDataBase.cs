using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public sealed class UserDataBase : DataBase<UserDataBase>
{
    protected override string FileName => "user_data";
    protected override string Path => Application.dataPath;
    protected override string Key => "01234567890123456789012345678912";
    /// <summary>
    /// BGM音量
    /// </summary>
    /// <value></value>
    public float BGMVolume
    {
        get => bgmVolume;
        set { bgmVolume = Math.Clamp(value, 0, 1); isEdited = true; }
    }
    [SerializeField] private float bgmVolume = 0.5f;
    /// <summary>
    /// SE音量
    /// </summary>
    /// <value></value>
    public float SEVolume
    {
        get => seVolume;
        set { seVolume = Math.Clamp(value, 0, 1); isEdited = true; }
    }
    [SerializeField] private float seVolume = 0.5f;
    /// <summary>
    /// ダメージ表示
    /// </summary>
    /// <value></value>
    public bool IsShowDamage
    {
        get => isShowDamage;
        set { isShowDamage = value; isEdited = true; }
    }
    [SerializeField] private bool isShowDamage = true;
    /// <summary>
    /// 選択Unit1
    /// </summary>
    /// <value></value>
    public UnitGroup SelectedUnit1
    {
        get => EnumConverter.ConvertUnitGroup(selectedUnit1);
        set { selectedUnit1 = value.ToString(); isEdited = true; }
    }
    [SerializeField] private string selectedUnit1 = UnitGroup.Green.ToString();
    /// <summary>
    /// 選択Unit2
    /// </summary>
    /// <value></value>
    public UnitGroup SelectedUnit2
    {
        get => EnumConverter.ConvertUnitGroup(selectedUnit2);
        set { selectedUnit2 = value.ToString(); isEdited = true; }
    }
    [SerializeField] private string selectedUnit2 = UnitGroup.Green.ToString();
    [Serializable]
    private sealed class InnerStageRecord
    {
        public readonly StageID ID;
        [SerializeField] private string id;
        [SerializeField] public bool Flag;
        [SerializeField] public int HighScore;
        [SerializeField] public int Stars;
        public InnerStageRecord(StageID id, bool flag, int highScore, int stars)
        {
            ID = id;
            this.id = id.ToString();
            Flag = flag;
            HighScore = highScore;
            Stars = stars;
        }
    }
    [SerializeField] private List<InnerStageRecord> rowList;
    private Dictionary<StageID, int> stageIDToIndex;
    void Start()
    {
        rowList = new();
        stageIDToIndex = new();
        #if UNITY_EDITOR
        switch (step)
        {
            case -1:
                foreach (StageID id in Enum.GetValues(typeof(StageID)))
                {
                    rowList.Add(new InnerStageRecord(id, (id == StageID.Stage1), 0, 0));
                }
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
        JsonUtility.FromJson<List<UnitData>>(json);
        int index = 0;
        foreach (var data in rowList)
        {
            stageIDToIndex.Add(data.ID, index++);
        }
    }

    /// <summary>
    /// 非同期でユーザーデータを書き込む
    /// </summary>
    /// <returns></returns>
    public override async void SaveAsync()
    {
        string json = JsonUtility.ToJson(this);
        await saveAsync(json);
    }

    public bool GetFlag(StageID id) => rowList[stageIDToIndex[id]].Flag;
    public void SetFlag(StageID id, bool flg) => rowList[stageIDToIndex[id]].Flag = flg;
    public int GetHighScore(StageID id) => rowList[stageIDToIndex[id]].HighScore;
    public void SetHighScore(StageID id, int score) => rowList[stageIDToIndex[id]].HighScore = score;
    public int GetStars(StageID id) => rowList[stageIDToIndex[id]].Stars;
    public void SetStars(StageID id, int stars) => rowList[stageIDToIndex[id]].Stars = stars;
}