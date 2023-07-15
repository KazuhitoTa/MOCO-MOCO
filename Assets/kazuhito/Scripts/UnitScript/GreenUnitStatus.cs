using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GreenUnitStatus : MonoBehaviour
{
    private UnitStatus unitStatus=new UnitStatus();
    [SerializeField] UnitDataBase db;
    //GreenUnitData data;
    public GameObject bulletGameObject;
    [SerializeField]private GameObject hpBar;
    [SerializeField]private Transform barPos;

    /// <summary>
    /// このユニットのStatusの初期化
    /// </summary>
    public void ManagedStart()
    {
        initState();

        CreateHealthBar();

        var obj =GameObject.FindGameObjectWithTag("GameController");
        unitStatus.bulletManager=obj.GetComponent<BulletManager>(); 
    }

    /// <summary>
    /// Update
    /// </summary>
    public void ManagedUpdate()
    {
        if(unitStatus.hpBarImage!=null)unitStatus.hpBarImage.fillAmount=Mathf.Lerp(unitStatus.hpBarImage.fillAmount,unitStatus.currentHP/unitStatus.HP,Time.deltaTime*10f);
        //unitStatus.enemyMethodsにnullがあるならその要素を削除
        foreach (var item in unitStatus.enemyMethods.ToList())
        {
            if(item==null)unitStatus.enemyMethods.Remove(item);
        }
        //Unitの状態を管理
        switch (unitStatus.unitState)
        {
            case UnitStatus.UnitState.stay:
            Debug.Log("Stay");
            break;
            case UnitStatus.UnitState.attack:
            if(unitStatus.enemyMethods.Count==0)unitStatus.unitState=UnitStatus.UnitState.stay;
            break;
            case UnitStatus.UnitState.death:
            Debug.Log("Death");
            break;
        }
    }

    private void initState()
    {
        //data=db.GetDataInstance<GreenUnitData>(UnitGroup.Green);
        unitStatus.HP=10;
        unitStatus.Attack=2;
        unitStatus.AttackSpeed=1;
        unitStatus.kindNumber=1;
        unitStatus.strage=2;
        unitStatus.animator=gameObject.AddComponent<Animator>();
        unitStatus.unitState=UnitStatus.UnitState.stay;
        unitStatus.kindNumber=1;
        unitStatus.enemyMethods.Clear();
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
            gameObject.SetActive(false);
            //UnitManager.Instance.RemoveUnit(this);
        }
    }


    /// <summary>
    /// 当たった時実行
    /// </summary>
    /// <param name="collider2D">当たったオブジェクトのコライダー</param>
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //ユニットのStatusをattackに変更
        if (collider2D.gameObject.CompareTag("Bullet"))
        {
            UnitBullet temp=collider2D.GetComponent<UnitBullet>();
            ReduceHP(temp.Damage());
        }
    }

    /// <summary>
    /// 銃弾を生成するメソッド
    /// </summary>
    /// <returns></returns>
    public GameObject CreateBullet(EnemyMethod enemyMethod)
    {
        return Instantiate(bulletGameObject,transform.position,Quaternion.identity); 
    }

    IEnumerator AttackCoroutine(float createInterval)
    {
        while (unitStatus.enemyMethods.First().currentHP>0)
        {
             
            yield return new WaitForSeconds(createInterval);

            // 敵が存在しなくなったらCoroutineを終了する
            if (!IsEnemyAlive())
            {
                yield break;
            }
        }
    }

    bool IsEnemyAlive()
    {
        return true;
    }

    public void HitEnemy(Collider2D collider2D)
    {
        unitStatus.enemyMethods.Add(collider2D.GetComponent<EnemyMethod>());
    }

    public bool AttackCheck()
    {
        return unitStatus.enemyMethods.Count < unitStatus.strage;
    }


    

}
