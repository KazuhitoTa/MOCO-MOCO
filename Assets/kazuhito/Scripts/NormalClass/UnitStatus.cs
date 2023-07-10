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

    //攻撃をしたとき
    public Collider2D enemyCollider;
    public int Attack;
    public int AttackSpeed;
    public BulletManager bulletManager;

    //それ以外
    public Animator animator;

}
