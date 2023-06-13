using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class DamageParam
{
    public readonly int Attack;
    public DamageParam(int attack)
    {
        Attack = attack;
    }
}