using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatus
{
    //攻撃をされたとき
    public Collider2D myCollider;
    public int HP;
    public GameObject hpBar;
    public Transform barPos;
    public Image hpBarImage;
    public int currentHP;
    public List<EnemyMethod> enemyMethods=new List<EnemyMethod>();
    public int strage;

    //攻撃をしたとき
    public Collider2D enemyCollider;
    public int Attack;
    public float AttackSpeed;
    public BulletManager bulletManager;

    //それ以外
    public Animator animator;
    public UnitState unitState;
    public EnemyMethod enemyMethod;
    public int kindNumber;


    public enum UnitState
    {
        stay,
        attack,
        death
    }

    public int GetAttack(int level,int Rank)
    {
        //基礎値 + ((2 * レベル) + (5 * 2 ^ (ランク - 1))) * 0.5
        return (int)(10+((2*level)+(5*Mathf.Pow(2, Rank-1)))*0.5f);
    }
    public int GetHP(int level, int Rank)
    {
        //基礎値 + ((2 * レベル) + (5 * 2 ^ (ランク - 1))) * 0.8
        return (int)(10+((2*level)+(5*Mathf.Pow(2, Rank-1)))*0.8f);
    }
    public float GetAttackSpeed(int level)
    {
        return 1;
    }


}
