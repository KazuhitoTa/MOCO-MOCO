using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Linq;

public class UnitMethod : MonoBehaviour
{
    [SerializeField] UnitDataBase db;
    UnitData data;
    private int MyNumber;
    private float Hp;
    private int Atk;
    private float AtkSpeed;
    public float currentHP;
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;
    private Image hpBarImage;
    [SerializeField]UnitStatusSO unitStatusSO;
    private Coroutine bulletCreateCoroutine; 
    private BulletManager manager;
    [SerializeField]private EnemyMethod enemyMethod;
    private Animator animator;
    private Animator enemyAnimator;
    private bool enemyCheck=false;

   [SerializeField]private List<EnemyMethod>enemyMethods=new List<EnemyMethod>();
    //private List<Collider2D> collsionTemp=new List<Collider2D>();

    Collider2D collsionTemp;
    // 初期化
	public void Init(int unitNumber)
	{
        animator=GetComponent<Animator>();
        defaultState();
        CreateHealthBar();
        data=db.GetDataInstance<GreenUnitData>(UnitGroup.Green);
        Assert.IsNotNull<UnitDataBase>(db,"aaaaaaaaa");
        Assert.IsNotNull<UnitData>(data,"bbbbbbb");
	}

	// Updateの呼び出しを制御する
	public void ManagedUpdate()
	{
        if(enemyMethods.Count>0&&!enemyCheck)
        {
            enemyCheck=true;
            StartCoroutine("UnitAttackRoutine");
            //enemyMethods.Remove(enemyMethods.First());
            //if(enemyMethods.First().DeathCheck())enemyCheck=false;
            Debug.Log(enemyMethods.First().DeathCheck());
        }
        
        if(hpBarImage!=null)hpBarImage.fillAmount=Mathf.Lerp(hpBarImage.fillAmount,currentHP/Hp,Time.deltaTime*10f);  
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

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            if(!enemyMethods.Contains(collider2D.gameObject.GetComponent<EnemyMethod>()))enemyMethods.Add(collider2D.gameObject.GetComponent<EnemyMethod>());            
            
            // if(enemyMethod==null)
            // {
            //     enemyMethod=collider2D.gameObject.GetComponent<EnemyMethod>();
            //     StartCoroutine("UnitAttackRoutine");
            // }
            
        }
    }

    
    private IEnumerator UnitAttackRoutine()
    {
        while (enemyMethods.First() == null || (enemyMethods.First() != null && enemyMethods.First().currentHP > 0))
        {
            //enemyMethods.Remove(enemyMethods.First());
            if(currentHP<Hp)currentHP+=2;
            if (enemyMethods.First() != null)
            {
                animator.SetTrigger("Attack");
                enemyMethods.First().ReduceHP(Atk);
            }
            yield return new WaitForSeconds(AtkSpeed);
        }
        
    }
    
    



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))enemyMethod=null;
    }



}
