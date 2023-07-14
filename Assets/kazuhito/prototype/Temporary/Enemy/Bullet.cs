using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float atk;
    [SerializeField]private float moveSpeed=10f;
    [SerializeField]private float damagerDistance=10f;
    private EnemyMethod enemyTarget;
    private int bulletNumber;
    [SerializeField]private UnitStatusSO unitStatusSO;

    public void Init()
    {
        
    }
    public void ManagedUpdate()
    {
        //atk=unitStatusSO.unitStatusList[bulletNumber].Attack;
        if(enemyTarget!=null)
        {
            MoveBullet();
        }
        else BulletManager.Instance.RemoveBullet(this);
    }

    public void GetInformation(EnemyMethod enemy,int unitNumber)
    {
        enemyTarget=enemy;
        bulletNumber=unitNumber;
    }

    void MoveBullet()
    {
        //現在地からの移動
        transform.position=Vector2.MoveTowards(transform.position,enemyTarget.transform.position,moveSpeed*Time.deltaTime);

        //弾と敵の距離確認
        CheckDistance();
    }

    /// <summary>
    /// 弾と敵の位置を確認して近ければダメージ
    /// </summary>
    void CheckDistance()
    {
        float distanceToTarget=(enemyTarget.transform.position-transform.position).magnitude;

        if(distanceToTarget<damagerDistance)
        {
            enemyTarget.ReduceHP(atk);
            BulletManager.Instance.RemoveBullet(this);
        }

    }

    public void GetDamage(int attack)
    {
        atk=attack;
    }
}
