using System;
using UnityEngine;
using R3;
using System.Collections.Generic;

/// <summary>
/// GOAPエージェントの信念（Belief）を生成するファクトリークラス
/// </summary>
public class BeliefFactory
{
    readonly GoapAgent _agent;
    readonly Dictionary<string, AgentBelief> _beliefs;

    /// <summary>
    /// BeliefFactoryのコンストラクタ
    /// </summary>
    /// <param name="agent">関連付けるGOAPエージェント</param>
    /// <param name="belief">信念を格納する辞書</param>
    public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> belief)
    {
        this._agent = agent;
        this._beliefs = belief;
    }

    /// <summary>
    /// 条件に基づく新しい信念を追加
    /// </summary>
    /// <param name="key">信念を識別するキー</param>
    /// <param name="condition">信念の条件を評価する関数</param>
    public void AddBelief(string key, Func<bool> condition)
    {
        _beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(condition)
            .Build());
    }

    /// <summary>
    /// 位置情報に基づく新しい信念を追加
    /// </summary>
    /// <param name="key">信念を識別するキー</param>
    /// <param name="distance">判定する距離</param>
    /// <param name="locationCondition">目標位置のTransform</param>
    public void AddBelief(string key, float distance, Transform locationCondition)
    {
        AddLocationBelief(key, distance, locationCondition.position);
    }

    /// <summary>
    /// Vector3位置情報に基づく新しい信念を追加
    /// </summary>
    /// <param name="key">信念を識別するキー</param>
    /// <param name="distance">判定する距離</param>
    /// <param name="locationCondition">目標位置のVector3</param>
    public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
    {
        _beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(() => InRangeOf(locationCondition, distance))
            .WithLocation(() => locationCondition)
            .Build());
    }

    /// <summary>
    /// エージェントが指定された位置の範囲内にいるかを判定
    /// </summary>
    /// <param name="pos">判定する位置</param>
    /// <param name="range">判定範囲</param>
    /// <returns>範囲内にいる場合はtrue</returns>
    bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(_agent.MyTransform.position, pos) < range;
}

/// <summary>
/// GOAPエージェントの単一の信念を表現するクラス
/// エージェントの現在の状態や位置に関する情報を保持する
/// </summary>
public class AgentBelief
{
    /// <summary>
    /// 信念の識別名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// エージェントの状態を評価する条件関数
    /// </summary>
    private Func<bool> _condition = () => false;
    /// <summary>
    /// 条件関数のゲッター
    /// </summary>
    public Func<bool> Condition => _condition;

    /// <summary>
    /// エージェントの観測位置を取得する関数
    /// </summary>
    private Func<Vector3> _observedLocation = () => Vector3.zero;
    /// <summary>
    /// 観測位置関数のゲッター
    /// </summary>
    public Func<Vector3> ObservedLocation => _observedLocation;

    /// <summary>
    /// エージェントの信念を初期化
    /// </summary>
    /// <param name="name">信念の名前</param>
    AgentBelief(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 現在の信念状態を評価
    /// </summary>
    /// <returns>条件が満たされている場合はtrue</returns>
    public bool Evaluate() => _condition();

    /// <summary>
    /// AgentBeliefのビルダークラス
    /// Builderパターンを使用して信念オブジェクトを構築
    /// </summary>
    public class Builder
    {
        readonly AgentBelief _belief;

        /// <summary>
        /// ビルダーを初期化
        /// </summary>
        /// <param name="name">作成する信念の名前</param>
        public Builder(string name)
        {
            _belief = new AgentBelief(name);
        }

        /// <summary>
        /// 信念の条件を設定
        /// </summary>
        /// <param name="condition">条件を評価する関数</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithCondition(Func<bool> condition)
        {
            _belief._condition = condition;
            return this;
        }

        /// <summary>
        /// 信念の観測位置を設定
        /// </summary>
        /// <param name="observedLocation">位置を取得する関数</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithLocation(Func<Vector3> observedLocation)
        {
            _belief._observedLocation = observedLocation;
            return this;
        }

        /// <summary>
        /// 設定された情報を基に信念オブジェクトを生成
        /// </summary>
        /// <returns>構築されたAgentBeliefインスタンス</returns>
        public AgentBelief Build()
        {
            return _belief;
        }
    }
}
