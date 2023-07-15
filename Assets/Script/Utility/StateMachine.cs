using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// 汎用ステートマシンの抽象クラス
/// </summary>
/// <typeparam name="TStateID">ステートID</typeparam>
/// <typeparam name="TStateMachine">ステートマシン</typeparam>
public abstract class StateMachine<TStateID, TStateMachine> : MonoBehaviour
where TStateID : Enum
where TStateMachine : StateMachine<TStateID, TStateMachine>
{
    [Serializable]
    private class Pair
    {
        public TStateID ID => id;
        [SerializeField] private TStateID id;
        public State<TStateID, TStateMachine> State => state;
        [SerializeField] private State<TStateID, TStateMachine> state;
    }
    [SerializeField] private TStateID firstStateID;
    [SerializeField] private List<Pair> transTableView;
    private Dictionary<TStateID, State<TStateID, TStateMachine>> transTable;
    private Stack<State<TStateID, TStateMachine>> stateStack;
    private enum Step
    {
        OnEntry,
        OnUpdate,
        OnExit,
    }
    private bool willPop;
    private bool willPush;
    private bool willRecord;
    private string nextSceen;
    private Step nextStep;
    private State<TStateID, TStateMachine> nextState;

    void Awake()
    {
        transTable = new();
        foreach (var pair in transTableView)
        {
            Assert.IsFalse(transTable.ContainsKey(pair.ID), $"{pair.ID} is already exist");
            Assert.IsFalse(transTable.ContainsValue(pair.State), $"{pair.State} is already exist");
            transTable.Add(pair.ID, pair.State);
        }
        _ = transTableView;

        stateStack = new();
        stateStack.Push(transTable[firstStateID]);

        willPop = false;
        willPush = false;
        willRecord = false;

        nextStep = Step.OnEntry;
    }
        
    void Update()
    {
        switch (nextStep)
        {
            case Step.OnEntry:
                stateStack.Peek().OnEntry();
                nextStep = Step.OnUpdate;
                break;
            case Step.OnUpdate:
                stateStack.Peek().OnUpdate();
                if (willPop || willPush)
                {
                    nextStep = Step.OnExit;
                }
                break;
            case Step.OnExit:
                stateStack.Peek().OnExit();
                if (willPop)
                {
                    _ = stateStack.Pop();
                    willPop = !willPop;
                }
                if (willPush)
                {
                    if (!willRecord)
                    {
                        Assert.IsTrue(stateStack.Count >= 1, "State can not back, History is empty");
                        _ = stateStack.Pop();
                        willRecord = !willRecord;
                    }
                    stateStack.Push(nextState);
                    nextState = null;
                    willPush = !willPush;
                }
                nextStep = Step.OnEntry;
                break;
        }
    }

    /// <summary>
    /// ステートを戻すフラグを立てる <br/>
    /// 次のフレームで退場処理がされてから遷移する
    /// </summary>
    public void SetBackFlag()
    {
        Assert.IsTrue(stateStack.Count > 1, "State can not back, History is empty");
        willPop = true;
    }

    /// <summary>
    /// ステートを進むフラグを立てる <br/>
    /// 次のフレームで退場処理がされてから遷移する
    /// </summary>
    /// <param name="nextStateID">次のステートID</param>
    /// <param name="willRecord">スタックに記録するフラグ</param>
    public void SetMoveFlag(TStateID nextStateID, bool willRecord = false)
    {
        Assert.IsTrue(transTable.ContainsKey(nextStateID), $"{nextStateID} is not registerd");
        this.nextState = transTable[nextStateID];
        this.willRecord = willRecord;
        willPush = true;
    }
}