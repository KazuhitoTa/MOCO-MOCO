using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	[SerializeField]UnitStatusSO unitStatusSO;
	[SerializeField]MapManager mapManager;

	// 敵リスト
	private List<UnitMethod> _units = new List<UnitMethod>();
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


	// 敵を生成する
	public UnitMethod CreateUnit(int unitNumber,Vector3 pos)
	{
		pos.z+=pos.y/1000f;
		var obj = Instantiate(unitStatusSO.unitStatusList[unitNumber].UnitModel,pos,Quaternion.identity);

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
