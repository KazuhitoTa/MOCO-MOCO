using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyMethod : MonoBehaviour
{
    Transform target;
    bool test;
    private int Hp;
    private int Atk;
    private int Def;
    private float AtkSpeed;
    public float currentHP;
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;
    private Image hpBarImage;
    [SerializeField]EnemyStatusSO enemyStatusSO;
    Animator animator;
    private UnitMethod unitScriptTemp;
    private Tower towerScriptTemp;
    private Coroutine enemyAttackCoroutine; 
    private Collider2D collsionTemp;

    DateTime dt;

    // 初期化
	public void Init(int enemyNumber)
	{
        Hp=enemyStatusSO.enemyStatusList[enemyNumber].HP;
        Atk=enemyStatusSO.enemyStatusList[enemyNumber].Attack;
        AtkSpeed=enemyStatusSO.enemyStatusList[enemyNumber].AttackSpeed;
        CreateHealthBar();
        animator = GetComponent<Animator>();
	}

	// Updateの呼び出しを制御する
	public void ManagedUpdate()
	{
        var pos=gameObject.transform.position;
        pos.z=pos.y/1000f;
        gameObject.transform.position=new Vector3(pos.x,pos.y,pos.z);
        if(!test)
        {
            target = GameObject.FindWithTag("Tower").transform;
            test=!test;
        }
        if(unitScriptTemp!=null||towerScriptTemp!=null)
        {
            //
            //collsionTemp=null;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1.0f * Time.deltaTime);
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
