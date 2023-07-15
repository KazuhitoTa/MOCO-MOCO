using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyMethod : MonoBehaviour
{
    Vector3 target;
    bool test;
    private int Hp;
    private int Atk;
    private int Def;
    private float AtkSpeed;
    private float Speed;
    public float currentHP;
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;
    private Image hpBarImage;
    [SerializeField]EnemyStatusSO enemyStatusSO;
    [SerializeField]Enemyes enemyes;
    Animator animator;
    private UnitMethod unitScriptTemp;
    private Tower towerScriptTemp;
    private Coroutine enemyAttackCoroutine; 
    private Collider2D collsionTemp;
    public List<Vector3> unitMove=new List<Vector3>();

    DateTime dt;

    // 初期化
	public void Init(int enemyNumber)
	{
        //Hp=enemyStatusSO.enemyStatusList[enemyNumber].HP;
        Hp=enemyes.EnemyDefaultStatuses[enemyNumber].HP;
        //Atk=enemyStatusSO.enemyStatusList[enemyNumber].Attack;
        Atk=enemyes.EnemyDefaultStatuses[enemyNumber].Attack;
        //AtkSpeed=enemyStatusSO.enemyStatusList[enemyNumber].AttackSpeed;
        AtkSpeed=enemyes.EnemyDefaultStatuses[enemyNumber].AttackSpeed;
        Speed=enemyes.EnemyDefaultStatuses[enemyNumber].MoveSpeed;
        CreateHealthBar();
        animator = GetComponent<Animator>();

        unitMove.Add(new Vector3(-6.1f,2.5f,0f));
        unitMove.Add(new Vector3(-0.1f,2.5f,0f));
        unitMove.Add(new Vector3(5.8f,2.5f,0f));
       
        target=unitMove[1];
	}

	// Updateの呼び出しを制御する
	public void ManagedUpdate()
	{   
        if(towerScriptTemp==null&&unitScriptTemp==null)
        {
            if(Vector3.Distance(unitMove[1],transform.position)<0.1f)
            {
                if(Atk%2==0)target=unitMove[2];
                else target=unitMove[0];
            }
            
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        }
        
        hpBarImage.fillAmount=Mathf.Lerp(hpBarImage.fillAmount,currentHP/Hp,Time.deltaTime*10f);
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
            
            EnemyManager.Instance.RemoveEnemy(this);
        }
    }
    public bool DeathCheck()
    {
        if(currentHP<=0)
        {
            return true;
        }
        return false;
    }

    private IEnumerator EnemyAttackRoutine()
    {
        if(unitScriptTemp!=null)
        {
            while (unitScriptTemp.unitStatus.currentHP > 0)
            {
                unitScriptTemp.ReduceHP(Atk);
                yield return new WaitForSeconds(AtkSpeed);
            }
        }
        else
        {
            while (towerScriptTemp.currentHP > 0)
            {
                towerScriptTemp.ReduceHP(Atk);
                yield return new WaitForSeconds(AtkSpeed);
            }
        }
        
    }

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("Hit");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            unitScriptTemp=collision.gameObject.GetComponent<UnitMethod>();
            StartCoroutine("EnemyAttackRoutine");      
        }

        if (collision.gameObject.CompareTag("Tower"))
        {
            towerScriptTemp=collision.gameObject.GetComponent<Tower>();
            StartCoroutine("EnemyAttackRoutine");
            //Debug.Log("hit");
        }

        
    }
    

    public void HitEnemy(Collider2D collider2D)
    {
       
    }
    public void ExitEnemy(Collider2D collider2D)
    {
        
    }
}
