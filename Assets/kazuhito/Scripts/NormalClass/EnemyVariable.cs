using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVariable
{
    public Collider2D myCollider;
    public int HP;
    public GameObject hpBar;
    public Transform barPos;
    public Image hpBarImage;
    public List<GameObject> unitGameObjects=new List<GameObject>();
    public List<GreenUnitStatus> greenUnitMethods=new List<GreenUnitStatus>();
    public List<YellowUnitStatus> yellowUnitMethods=new List<YellowUnitStatus>();
    public int strage;

    //攻撃をしたとき
    public Collider2D enemyCollider;
    public int Attack;
    public float AttackSpeed;
    public BulletManager bulletManager;

    //それ以外
    public Animator animator;
    public EnemyState enemyState;
    public EnemyMethod enemyMethod;
    public int kindNumber;

    public enum EnemyState
    {
        stay,
        attack,
        death
    }
}
