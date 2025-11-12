using System;
using UnityEngine;
using R3;

/// <summary>
/// 移動機能を管理するプレゼンター
/// モデルとビュー間のデータバインディングを処理
/// </summary>
public class MovePresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private MoveModel _model;
    private MoveView _view;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveModel">移動モデル</param>
    /// <param name="moveView">移動ビュー</param>
    public MovePresenter(MoveModel moveModel, MoveView moveView)
    {
        _model = moveModel;
        _view = moveView;

        _model.Initialize();
        _view.Initialize();
    }
   
    /// <summary>
    /// モデルとビューのバインド処理
    /// </summary>
    public void Bind()
    {
        _model.SetPosition(Vector3Extensions.ToSystemVector3(_view.transform.position));

        //位置情報の更新
        _model.RPTransformPosition.Subscribe(pos => _view.SetPosition(Vector3Extensions.ToUnityVector3(pos))).AddTo(_disposables);

        //クリック位置の更新
        _view.RPClickPos.Subscribe(pos => _model.MovePosition(Vector3Extensions.ToSystemVector3(pos))).AddTo(_disposables);
    }

    /// <summary>
    /// リソースの解放
    /// </summary>
    public void Dispose()
    {
        _disposables.Dispose();
    }
}

