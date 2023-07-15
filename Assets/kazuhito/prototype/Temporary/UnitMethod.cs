using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitMethod : MonoBehaviour
{
    public UnitStatus unitStatus=new UnitStatus();
    [SerializeField] UnitDataBase db;
    private int Level;
    
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;
    private GameObject test;

    /// <summary>
    /// このユニットのStatusの初期化
    /// </summary>
    public void Init(int kindNumber)
    {
        //test=GameObject.FindGameObjectWithTag("UnitDataBase");
        //Debug.Log(test);
        //db=test.GetComponent<UnitDataBase>();
        //Debug.Log(db);
        //まじでゴミ
        if(kindNumber>4)unitStatus.kindNumber=kindNumber-5;
        else unitStatus.kindNumber=kindNumber;


        InitState();
        //Debug.Log(db.GetGrowthLevel(UnitGroup.Green));
        //Level = db.GetGrowthLevel(UnitGroup.Green);
        CreateHealthBar();
        unitStatus.kindNumber=kindNumber;
        //Debug.Log(unitStatus.kindNumber);

        var obj =GameObject.FindGameObjectWithTag("GameController");
        unitStatus.bulletManager=obj.GetComponent<BulletManager>(); 
    }

    /// <summary>
    /// Update
    /// </summary>
    public void ManagedUpdate()
    {
        
        if(unitStatus.hpBarImage!=null)unitStatus.hpBarImage.fillAmount=Mathf.Lerp(unitStatus.hpBarImage.fillAmount,((float)unitStatus.currentHP/unitStatus.HP),Time.deltaTime*10f);
        //unitStatus.enemyMethodsにnullがあるならその要素を削除
        foreach (var item in unitStatus.enemyMethods.ToList())
        {
            if(item==null)unitStatus.enemyMethods.Remove(item);
        }
       
        
        //Unitの状態を管理
        switch (unitStatus.unitState)
        {
            case UnitStatus.UnitState.stay:
            //Debug.Log("Stay");
            break;
            case UnitStatus.UnitState.attack:
            if(unitStatus.enemyMethods.Count==0)unitStatus.unitState=UnitStatus.UnitState.stay;
            else
            {
                StartCoroutine(AttackCoroutine(1.5f));
                unitStatus.unitState=UnitStatus.UnitState.stay;
            }
            break;
            case UnitStatus.UnitState.death:
            Debug.Log("Death");
            break;
        }
    }

    private void InitState()
    {
        //Level=db.GetGrowthLevel(UnitGroup.Green);
        //Debug.Log(Level);
        //data=db.GetDataInstance<GreenUnitData>(UnitGroup.Green);
        unitStatus.HP=unitStatus.GetHP(3,unitStatus.kindNumber+1);
        Debug.Log(unitStatus.HP);
        unitStatus.Attack=unitStatus.GetAttack(3,unitStatus.kindNumber+1);
        unitStatus.AttackSpeed=unitStatus.GetAttackSpeed(1);
        //unitStatus.kindNumber=1;
        unitStatus.strage=2;
        unitStatus.animator=gameObject.AddComponent<Animator>();
        unitStatus.unitState=UnitStatus.UnitState.stay;
        //unitStatus.kindNumber=1;
        unitStatus.enemyMethods.Clear();
        unitStatus.animator=gameObject.GetComponent<Animator>();
    }

    private void CreateHealthBar()
    {
        GameObject newBar=Instantiate(hpBar,barPos.position,Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHPBar healthBar=newBar.GetComponent<EnemyHPBar>();
        unitStatus.hpBarImage=healthBar.hpBarImage; 
        unitStatus.currentHP=unitStatus.HP;   
    }

    public void ReduceHP(int damage)
    {
        unitStatus.currentHP-=damage;
        DeathChack();
    }

     private void DeathChack()
    {
        if(unitStatus.currentHP<=0)
        {
            unitStatus.currentHP=0;
            //gameObject.SetActive(false);
            UnitManager.Instance.RemoveUnit(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //ユニットのStatusをattackに変更
        if (collider2D.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("hit");
            Bullet temp=collider2D.GetComponent<Bullet>();

        }
    }

    IEnumerator AttackCoroutine(float createInterval)
    {
        if (unitStatus.enemyMethods.Count > 0)
        {
            EnemyMethod enemyMethod = unitStatus.enemyMethods.First();
            while (unitStatus.enemyMethods.Count > 0 && enemyMethod.currentHP > 0 && enemyMethod == unitStatus.enemyMethods[0])
            {
                BulletCreate(enemyMethod);
                if (unitStatus.enemyMethods.Count <= 0) break;
                unitStatus.animator.SetTrigger("Attack");
                yield return new WaitForSeconds(createInterval);
            }
        }

    }



    private void BulletCreate(EnemyMethod enemyMethod)
    {
        Bullet bulletScriptTemp = unitStatus.bulletManager.CreateBullet(unitStatus.kindNumber,transform.position); 
        bulletScriptTemp.GetDamage(unitStatus.Attack);
        bulletScriptTemp.GetInformation(enemyMethod,unitStatus.kindNumber);  
    }

    public void HitEnemy(Collider2D collider2D)
    {
        unitStatus.enemyMethods.Add(collider2D.GetComponent<EnemyMethod>());
        if(unitStatus.enemyMethods.Count==1)unitStatus.unitState=UnitStatus.UnitState.attack;
    }
    public void ExitEnemy(Collider2D collider2D)
    {
        unitStatus.enemyMethods.Remove(collider2D.gameObject.GetComponent<EnemyMethod>());
        if(unitStatus.enemyMethods.Count>0)unitStatus.unitState=UnitStatus.UnitState.attack;
    }

    public bool AttackCheck()
    {
        return unitStatus.enemyMethods.Count < unitStatus.strage;
    }

    

}
