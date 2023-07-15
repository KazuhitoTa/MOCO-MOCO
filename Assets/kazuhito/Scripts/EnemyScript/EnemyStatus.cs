using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private EnemyVariable enemyVariable;
    private Enemyes enemyes;

    public void Start()
    {
        var enemyState=enemyes.EnemyDefaultStatuses[enemyVariable.kindNumber];
        enemyVariable.HP=enemyState.HP;
        enemyVariable.Attack=enemyState.Attack;
        enemyVariable.AttackSpeed=enemyState.AttackSpeed;
        enemyVariable.enemyState=EnemyVariable.EnemyState.stay;
    }
    void Update()
    {
        switch (enemyVariable.enemyState)
        {
            case EnemyVariable.EnemyState.stay:
            Debug.Log("Stay");
            break;
            case EnemyVariable.EnemyState.attack:
            
            break;
            case EnemyVariable.EnemyState.death:
            Debug.Log("Death");
            break;
        }
    }

    //攻撃する処理
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //ユニットのStatusをattackに変更
        if (collider2D.gameObject.CompareTag("GreenUnit"))
        {
           enemyVariable.enemyState=EnemyVariable.EnemyState.attack;
           
        }

    }

}
