using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
    public sealed class InnerUnitData
    {
        [SerializeField] private string name;
        public readonly UnitGroup Name;
        [SerializeField] private int level;
        public readonly int Level;
        public InnerUnitData(UnitGroup name, int level)
        {
            Name = name;
            this.name = name.ToString();
            Level = level;
            this.level = level;
        }
    }

[Serializable]
public class UnitDataBase : DataBase<UnitDataBase>
{
    protected override string FileName => "unit_data.bin";
    protected override string Path => @$"{Application.dataPath}/{FileName}";
    protected override string Key => "01234567890123456789012345678912";
    [SerializeField] private List<InnerUnitData> db;
    private Dictionary<UnitGroup, int> indices;
    

    void Start()
    {
        db = new();
        //db.Add(new InnerUnitData(UnitName.Green, 1));
        foreach (UnitGroup group in Enum.GetValues(typeof(UnitGroup)))
        {
            db.Add(new InnerUnitData(group, 1));
        }
        indices = new ();
        step=0;
    }

    void Update()
    {
        
        #if UNITY_EDITOR
        switch (step)
        {
            case 0:
                Save();
                step++;
                break;
            case 1:
                if (!IsSaveing) step++;
                break;
            case 2:
                Load();
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

    public override async void Load()
    {
        db = new();
        indices = new ();
        string json = await load();
        JsonUtility.FromJson<List<InnerUnitData>>(json);
        Debug.Log("load");
        int index = 0;
        foreach (var data in db)
        {
            indices.Add(data.Name, index++);
        }
    }

    public override async void Save()
    {
        string json = JsonUtility.ToJson(db);
        await save(json);
    }

    public T GetDataInstance<T>(UnitGroup name)
    {
        GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/"+$"{name}"+"UnitData"));
        return temp.GetComponent<T>();
    }

    public int GetGrowthLevel(UnitGroup name)
    {
        Debug.Log(db.Count);
        
        Assert.IsTrue(indices.ContainsKey(name), $"{name} is not exist in UnitDataBase");
        return db[indices[name]].Level;
    }

    public static int i = 0;
}