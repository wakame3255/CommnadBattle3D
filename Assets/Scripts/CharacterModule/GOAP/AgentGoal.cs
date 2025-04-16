using System;
using System.Collections.Generic;

/// <summary>
/// GOAPエージェントの目標（Goal）を表現するクラス
/// エージェントが達成しようとする目標状態と、その優先度を管理する
/// </summary>
public class AgentGoal
{
    /// <summary>
    /// 目標の識別名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 目標の優先度
    /// 値が高いほど優先して実行される
    /// </summary>
    public float Priority { get; private set; }

    /// <summary>
    /// 目標達成に必要な望ましい状態（信念）のコレクション
    /// これらの条件がすべて満たされると目標が達成されたとみなされる
    /// </summary>
    public HashSet<AgentBelief> DesiredEffects { get; } = new();

    /// <summary>
    /// エージェントの目標を初期化
    /// </summary>
    /// <param name="name">目標の名前</param>
    AgentGoal(string name)
    {
        Name = name;
    }

    /// <summary>
    /// AgentGoalのビルダークラス
    /// Builderパターンを使用して目標オブジェクトを構築
    /// </summary>
    public class Builder
    {
        readonly AgentGoal _goal;

        /// <summary>
        /// ビルダーを初期化
        /// </summary>
        /// <param name="name">作成する目標の名前</param>
        public Builder(string name)
        {
            _goal = new AgentGoal(name);
        }

        /// <summary>
        /// 目標の優先度を設定
        /// </summary>
        /// <param name="priority">目標の優先度（値が高いほど優先）</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithPriority(float priority)
        {
            _goal.Priority = priority;
            return this;
        }

        /// <summary>
        /// 目標達成に必要な望ましい状態（信念）を追加
        /// </summary>
        /// <param name="effect">目標達成に必要な信念状態</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithEffect(AgentBelief effect)
        {
            _goal.DesiredEffects.Add(effect);
            return this;
        }

        /// <summary>
        /// 設定された情報を基に目標オブジェクトを生成
        /// </summary>
        /// <returns>構築されたAgentGoalインスタンス</returns>
        public AgentGoal Build()
        {
            return _goal;
        }
    }
}
