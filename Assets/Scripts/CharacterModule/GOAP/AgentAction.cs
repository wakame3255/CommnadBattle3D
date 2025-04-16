using System;
using System.Collections.Generic;

/// <summary>
/// GOAPシステムにおけるアクション（行動）を表現するクラス
/// 前提条件、効果、実行コスト、および実行戦略を管理する
/// </summary>
public class AgentAction
{
    /// <summary>
    /// アクションの識別名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// アクションの実行コスト（低いほど優先）
    /// </summary>
    public float Cost { get; private set; }

    /// <summary>
    /// アクション実行に必要な前提条件のセット
    /// すべての条件が満たされている必要がある
    /// </summary>
    public HashSet<AgentBelief> Preconditions { get; } = new();

    /// <summary>
    /// アクション実行後に得られる効果のセット
    /// </summary>
    public HashSet<AgentBelief> Effects { get; } = new();

    /// <summary>
    /// アクションの実行戦略
    /// 具体的な実行ロジックを提供する
    /// </summary>
    private IActionStrategy _actionStrategy;

    /// <summary>
    /// アクションが完了したかどうか
    /// </summary>
    public bool Complete => _actionStrategy.Complete;

    /// <summary>
    /// アクションの初期化
    /// </summary>
    /// <param name="name">アクションの名前</param>
    AgentAction(string name)
    {
        Name = name;
    }

    /// <summary>
    /// アクションの開始処理を実行
    /// アクション実行の初期化を行う
    /// </summary>
    public void Enter()
    {
        _actionStrategy.Enter();
    }

    /// <summary>
    /// アクションの更新処理を実行
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間</param>
    public void Update(float deltaTime)
    {
        // アクションが実行可能な場合のみ更新
        if (_actionStrategy.CanPerform)
        {
            _actionStrategy.Update(deltaTime);
        }

        // アクションが完了していない場合は継続
        if (!_actionStrategy.Complete)
        {
            return;
        }

        // アクション完了時、すべての効果を評価
        foreach (AgentBelief effect in Effects)
        {
            effect.Evaluate();
        }
    }

    /// <summary>
    /// アクションの終了処理を実行
    /// リソースの解放やクリーンアップを行う
    /// </summary>
    public void End()
    {
        _actionStrategy.End();
    }

    /// <summary>
    /// AgentActionのビルダークラス
    /// Builderパターンを使用してアクションオブジェクトを構築
    /// </summary>
    public class Builder
    {
        readonly AgentAction _action;

        /// <summary>
        /// ビルダーを初期化
        /// </summary>
        /// <param name="name">作成するアクションの名前</param>
        public Builder(string name)
        {
            _action = new AgentAction(name)
            {
                Cost = 1f, // デフォルトコスト
            };
        }

        /// <summary>
        /// アクションのコストを設定
        /// </summary>
        /// <param name="cost">実行コスト（低いほど優先）</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithCost(float cost)
        {
            _action.Cost = cost;
            return this;
        }

        /// <summary>
        /// アクションの実行戦略を設定
        /// </summary>
        /// <param name="actionStrategy">実装する具体的な実行戦略</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder WithActionStrategy(IActionStrategy actionStrategy)
        {
            _action._actionStrategy = actionStrategy;
            return this;
        }

        /// <summary>
        /// アクションの前提条件を追加
        /// </summary>
        /// <param name="precondition">実行に必要な条件（AgentBelief）</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder AddPrecondition(AgentBelief precondition)
        {
            _action.Preconditions.Add(precondition);
            return this;
        }

        /// <summary>
        /// アクションの効果を追加
        /// </summary>
        /// <param name="effect">実行後に得られる効果（AgentBelief）</param>
        /// <returns>ビルダーインスタンス</returns>
        public Builder AddEffect(AgentBelief effect)
        {
            _action.Effects.Add(effect);
            return this;
        }

        /// <summary>
        /// 設定された情報を基にアクションオブジェクトを生成
        /// </summary>
        /// <returns>構築されたAgentActionインスタンス</returns>
        public AgentAction Build()
        {
            return _action;
        }
    }
}


