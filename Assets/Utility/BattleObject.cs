using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleObject : ManagedMonoBehaviour
{
    protected int hp;
    public bool IsDead => hp <= 0;
    protected abstract string prefabName { get; }
    public virtual DamageParam Attack() { return default(DamageParam); }
    public virtual void Damaged(DamageParam damage) { }
}