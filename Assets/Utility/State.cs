using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用ステートの抽象クラス
/// </summary>
/// <typeparam name="TStateID"></typeparam>
/// <typeparam name="TStateMachne"></typeparam>
[Serializable]
public abstract class State<TStateID, TStateMachne> : MonoBehaviour
where TStateID : Enum
where TStateMachne : StateMachine<TStateID, TStateMachne>
{
    [SerializeField] protected TStateMachne stateMachine;
    public virtual void OnEntry() {}
    public virtual void OnUpdate() {}
    public virtual void OnExit() {}
}