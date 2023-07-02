using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMethod : MonoBehaviour
{
    private int MyNumber;
    private int Hp;
    private int Atk;
    private float AtkSpeed;
    public float currentHP;
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;
    private Image hpBarImage;
    [SerializeField]UnitStatusSO unitStatusSO;
    private Coroutine bulletCreateCoroutine; 
    private BulletManager manager;
    private List<Collider2D> collsionTemp=new List<Collider2D>();

    // 初期化
	public void Init(int unitNumber)
	{
        MyNumber=unitNumber;
        defaultState();
        CreateHealthBar();
        var obj =GameObject.FindGameObjectWithTag("GameController");
        manager=obj.GetComponent<BulletManager>();
	}

	// Updateの呼び出しを制御する
	public void ManagedUpdate()
	{
        if(hpBarImage!=null)hpBarImage.fillAmount=Mathf.Lerp(hpBarImage.fillAmount,currentHP/Hp,Time.deltaTime*10f);
        if(collsionTemp.Count!=0)
        {
            foreach (var collsion in collsionTemp)
            {
                bulletCreateCoroutine = StartCoroutine(BulletCreateRoutine(collsion));
            }
            collsionTemp.Clear();
        }
	}
    
    public void defaultState()
    {
        Hp=unitStatusSO.unitStatusList[MyNumber].HP;
        Atk=unitStatusSO.unitStatusList[MyNumber].Attack;
        AtkSpeed=unitStatusSO.unitStatusList[MyNumber].AttackSpeed;
    }

    private void CreateHealthBar()
    {
        GameObject newBar=Instantiate(hpBar,barPos.position,Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHPBar healthBar=newBar.GetComponent<EnemyHPBar>();
        hpBarImage=healthBar.hpBarImage; 
        currentHP=Hp;   
    }

    public void ReduceHP(float damage)
    {
        currentHP-=damage;
        DeathChack();
    }
    
    private void DeathChack()
    {
        if(currentHP<=0)
        {
            currentHP=0;
            UnitManager.Instance.RemoveUnit(this);
        }
    }

    private IEnumerator BulletCreateRoutine(Collider2D collision)
    {
        EnemyMethod enemyScriptTemp = collision.gameObject.GetComponent<EnemyMethod>();
        while (enemyScriptTemp.currentHP > 0)
        {
            BulletCreate(enemyScriptTemp);
            yield return new WaitForSeconds(AtkSpeed);
        }
    }

    private void BulletCreate(EnemyMethod enemy)
    {
        Bullet bulletScriptTemp = manager.CreateBullet(MyNumber,transform.position);
        bulletScriptTemp.GetInformation(enemy,MyNumber);  
    }

    public void HitEnemy(Collider2D collision)
    {
        collsionTemp.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collsionTemp.Remove(collision);
    }



}
