using System;
using UnityEngine;

/// <summary>
/// アクション実行戦略のインターフェース
/// GOAPアクションの実行方法を定義
/// </summary>
public interface IActionStrategy
{
    /// <summary>実行可能かどうか</summary>
    public bool CanPerform { get; }
    
    /// <summary>完了したかどうか</summary>
    public bool Complete { get; }

    /// <summary>
    /// 戦略開始時の処理
    /// </summary>
   public void Enter() { }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間</param>
    public void Update(float deltaTime) { }

    /// <summary>
    /// 戦略終了時の処理
    /// </summary>
    public void End() { }
}

/// <summary>
/// 攻撃戦略の実装
/// 攻撃アクションの実行方法を定義
/// </summary>
public class AttackStrategy : IActionStrategy
{
    /// <summary>実行可能かどうか</summary>
    public bool CanPerform => true;
    
    /// <summary>完了したかどうか</summary>
    public bool Complete { get; private set; }

    readonly CountdownTimer _timer;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AttackStrategy()
    {
        //アニメーションの情報をもらう
    }

    /// <summary>
    /// 攻撃開始時の処理
    /// </summary>
    public void Enter()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 攻撃の更新処理
    /// </summary>
    /// <param name="deltaTime">前フレームからの経過時間</param>
    public void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 攻撃終了時の処理
    /// </summary>
    public void End()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 移動戦略の実装
/// 移動アクションの実行方法を定義
/// </summary>
public class MoveStrategy : IActionStrategy
{
    readonly IMoveRequest _moveModel;
    readonly Func<Vector3> _destination;

    /// <summary>実行可能かどうか</summary>
    public bool CanPerform => !Complete;
    
    /// <summary>完了したかどうか（目的地までの距離による）</summary>
    public bool Complete => true; //目的地までの距離による
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveModel">移動モデル</param>
    /// <param name="destination">目的地を返す関数</param>
    public MoveStrategy(MoveModel moveModel, Func<Vector3> destination)
    {
        this._moveModel = moveModel;
        this._destination = destination;
    }

    /// <summary>
    /// 移動開始時の処理
    /// </summary>
    public void Enter() => _moveModel.MovePosition(Vector3Extensions.ToSystemVector3(_destination()));

    /// <summary>
    /// 移動終了時の処理
    /// </summary>
    public void End() => _moveModel.MoveStop();
   
}
