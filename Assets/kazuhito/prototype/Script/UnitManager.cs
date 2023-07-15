using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	[SerializeField]UnitStatusSO unitStatusSO;
	[SerializeField]GreenUnitStatusSO greenUnitStatusSO;
	[SerializeField]YellowUnitStatusSO yellowUnitStatusSO;
	[SerializeField]List<GameObject> useUnitGameObjects=new List<GameObject>();
	[SerializeField]MapManager mapManager;

	// 敵リスト
	[SerializeField]private List<UnitMethod> _units = new List<UnitMethod>();
	private static UnitManager _instance;
    public static UnitManager Instance
    {
        get { return _instance; }
    }

    public void UnitAwake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

	public void _Start()
	{
		for(int i=0;i<5;i++)
		{
			useUnitGameObjects.Add(greenUnitStatusSO.greenUnitStatuses[i].UnitModel);
		}
		for(int i=0;i<5;i++)
		{
			useUnitGameObjects.Add(yellowUnitStatusSO.yellowUnitStatuses[i].UnitModel);
		}	
	}

	// 敵を生成する
	public UnitMethod CreateUnit(int unitNumber,Vector3 pos)
	{
		pos.z+=pos.y/1000f;
		var obj = Instantiate(useUnitGameObjects[unitNumber],pos,Quaternion.identity);

		var unit = obj.GetComponent<UnitMethod>();
		if (unit != null)
		{
			// 初期化
			unit.Init(unitNumber);

			// unitリストに追加する
			_units.Add(unit);

			return unit;
		}

		return null;
	}	
	public void RemoveUnit(UnitMethod unit)
    {
        _units.Remove(unit);
		mapManager.ResetMapData(unit.gameObject);
		Destroy(unit.gameObject);
    }

	public void UseItemHeal()
	{
		foreach (var item in _units.ToList())
		{
			item.unitStatus.currentHP+=10;
		}
	}

	// Updateの呼び出しを制御する
	public void ManagedUpdate()
	{
		// 生成されている敵のUpdateを呼び出し
		foreach (var unit in _units)
		{
			unit.ManagedUpdate();
		}
	}

}
