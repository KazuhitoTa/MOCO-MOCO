using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UpgradeMachine : MonoBehaviour
{
    public UpgradeScene upgradeScene;
    public bool UpgradeMachineReleased;
    public bool SetUnit;
    public int UpgradeTime;
    public bool UpgradeFlag;
    public bool UpgradeExcessCheckFlag;
    public int UpgradeShotenTime;
    public Slider Timegage;
    public Text RemainTime;
    public Button Start;
    public Button Reset;
    public Button Shoten;
    public Button UnitButton;
    public Button ErrorReleased;
    public Image UnitImage;
    public DateTime StartTime;
    public DateTime FinishTime;
    public CollectionScene.UnitKind UpgradeUnitKind;
    public string FinishTimeStr;
    public int Lv;
    public TMP_Text Lv_Text;
    public string Time1;
    public string Time2;
    public UpgradeScene.UpgradeMachineStatus MachineStatus;

    public void MachineInit()
    {
        SetStatus_UnReleased();

        if (!this.UpgradeMachineReleased)
        {
            return;
        }
        else
        {
            SetStatus_UnUnitSet();
        }
        if (this.UpgradeFlag)
        {
            testupgradeInit();this.MachineStatus = UpgradeScene.UpgradeMachineStatus.UpgradeNow;
            SetStatus_Update();
        }
        else
        {
            testupgradeInit();
            SetStatus_UpdateFinish();
        }
    }


    private bool Time(DateTime ComparerTime1, DateTime CompareTime2)
    {
        if (ComparerTime1.Hour == CompareTime2.Hour && ComparerTime1.Minute == CompareTime2.Minute && ComparerTime1.Second == CompareTime2.Second)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 時間差分A-B
    /// </summary>
    /// <param name="TimeA">＊A時間</param>
    /// <param name="TimeB">＊B時間</param>
    /// <returns></returns>
    private int TimeDifference(DateTime TimeA, DateTime TimeB)
    {
        int RemainTimeValue;
        TimeSpan timeSpan;
        timeSpan = TimeA - TimeB;

        RemainTimeValue = timeSpan.Days * 86400 + timeSpan.Hours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds;
        return RemainTimeValue;
    }

    public void UpgradeReStart()
    {
        if(!this.UpgradeMachineReleased)return;
        if (!this.UpgradeFlag)
        {
            SetStatus_UnUnitSet();
            return;
        }
        this.UpgradeExcessCheckFlag = true;
        DateTime.TryParse(this.FinishTimeStr, out this.FinishTime);    //仮代入
        if (TimeDifference(this.FinishTime, this.upgradeScene.NowTime) < 0)
        {
            SetStatus_UpdateFinish();
        }
        else
        {
            SetStatus_Update();
        }
    }

    public void UpgradeStart()
    {
        if (!this.UpgradeFlag)
        {
            this.StartTime = DateTime.Now;
            this.FinishTime = this.StartTime.AddSeconds(this.UpgradeTime);
            this.UpgradeFlag = true;
            SetStatus_Update();
        }
    }

    public void UpgradeUpdate()
    {
        MachineUISetActive();
        this.Lv_Text.text = this.Lv.ToString();
        if (this.UpgradeFlag)
        {
            if (Time(upgradeScene.NowTime, this.FinishTime) || TimeDifference(FinishTime,upgradeScene.NowTime) < 0)
            {
                SetStatus_UpdateFinish();
            }
        }
    }

    public void UpgradeShoten(int ShotenTime)
    {
        this.FinishTime = this.FinishTime.AddSeconds(-ShotenTime);
    }

    public void UpgradeReset()
    {
        this.UpgradeFlag = false;
        SetStatus_UnUnitSet();
    }

    public void TimeGage()
    {
        this.Timegage.maxValue = this.UpgradeTime;
        this.Timegage.value = this.UpgradeTime - TimeDifference(this.FinishTime, this.upgradeScene.NowTime);
        this.RemainTime.text = "残り" + TimeDifference(this.FinishTime, this.upgradeScene.NowTime) + "秒";
    }

    /// <summary>
    /// アップグレードでレベルアップさせたいに能力変化させるステータスを入れる
    /// </summary>
    public void StatusUpgrade()
    {
        this.Lv++;
    }

    public void AddUnitState()
    {

    }

    public void MachineQuit()
    {
        if (this.UpgradeFlag) this.FinishTimeStr = this.FinishTime.ToString();
    }

    public void Havest()
    {
        upgradeScene.AlreadySetUnit[(int)this.UpgradeUnitKind] = false;
        Debug.Log((int)this.UpgradeUnitKind);
        this.StatusUpgrade();
        this.UpgradeReset();
        Debug.Log((int)this.UpgradeUnitKind);
    }







    public void testInit_Type1()
    {
        this.UpgradeMachineReleased = true;
        this.UpgradeFlag = false;
    }

    public void testupgradeInit()
    {
        this.UpgradeTime = 10;
        this.UpgradeShotenTime = 5;
    }

    public void MachineUISetActive()
    {
        if (this.MachineStatus == UpgradeScene.UpgradeMachineStatus.UnReleased)
        {
            SetStatus_UnReleased();
        }
        else if (this.MachineStatus == UpgradeScene.UpgradeMachineStatus.UnUnitSet)
        {
            SetStatus_UnUnitSet();
        }
        else if (this.MachineStatus == UpgradeScene.UpgradeMachineStatus.UpgradeNow)
        {
            SetStatus_Update();
        }
        else if (this.MachineStatus == UpgradeScene.UpgradeMachineStatus.UpgradeFinish)
        {
            SetStatus_UpdateFinish();
        }
    }














    public void SetStatus_UnReleased()
    {
        this.ErrorReleased.gameObject.SetActive(true);
        this.Start.gameObject.SetActive(false);
        this.Reset.gameObject.SetActive(false);
        this.Shoten.gameObject.SetActive(false);
        this.Timegage.gameObject.SetActive(false);
        this.RemainTime.gameObject.SetActive(false);
        this.UnitButton.gameObject.SetActive(false);

        this.MachineStatus = UpgradeScene.UpgradeMachineStatus.UnReleased;
        


        int num = (Enum.GetNames(typeof(CollectionScene.UnitKind)).Length);
        this.UpgradeUnitKind = (CollectionScene.UnitKind)Enum.ToObject(typeof(CollectionScene.UnitKind), num + 1);
    }

    public void SetStatus_UnUnitSet()
    {
        this.ErrorReleased.gameObject.SetActive(false);
        this.Start.gameObject.SetActive(false);
        this.Reset.gameObject.SetActive(false);
        this.Shoten.gameObject.SetActive(false);
        this.Timegage.gameObject.SetActive(false);
        this.RemainTime.gameObject.SetActive(false);
        this.UnitButton.gameObject.SetActive(true);

        this.MachineStatus = UpgradeScene.UpgradeMachineStatus.UnUnitSet;

        int num = (Enum.GetNames(typeof(CollectionScene.UnitKind)).Length);
        this.UpgradeUnitKind = (CollectionScene.UnitKind)Enum.ToObject(typeof(CollectionScene.UnitKind), num + 1);
    }

    public void SetStatus_Update()
    {
        this.ErrorReleased.gameObject.SetActive(false);
        this.Start.gameObject.SetActive(false);
        this.Reset.gameObject.SetActive(true);
        this.Shoten.gameObject.SetActive(true);
        this.Timegage.gameObject.SetActive(true);
        this.RemainTime.gameObject.SetActive(true);
        this.UnitButton.gameObject.SetActive(true);

        this.MachineStatus = UpgradeScene.UpgradeMachineStatus.UpgradeNow;

        TimeGage();
    }

    public void SetStatus_UpdateFinish()
    {
        this.ErrorReleased.gameObject.SetActive(false);
        this.Start.gameObject.SetActive(false);
        this.Reset.gameObject.SetActive(false);
        this.Shoten.gameObject.SetActive(false);
        this.Timegage.gameObject.SetActive(false);
        this.RemainTime.gameObject.SetActive(false);
        this.UnitButton.gameObject.SetActive(true);

        this.MachineStatus = UpgradeScene.UpgradeMachineStatus.UpgradeFinish;
    }
}
