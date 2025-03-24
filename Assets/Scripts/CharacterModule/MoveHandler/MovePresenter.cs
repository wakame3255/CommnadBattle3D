using System;
using UnityEngine;
using R3;

public class MovePresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private MoveModel _model;
    private MoveView _view;
    public MovePresenter(MoveModel moveModel, MoveView moveView)
    {
        _model = moveModel;
        _view = moveView;

        _model.Initialize();
        _view.Initialize();
    }
   
    public void Bind()
    {
        _model.SetPosition(Vector3Extensions.ToSystemVector3(_view.transform.position));

        //位置情報の更新
        _model.RPTransformPosition.Subscribe(pos => _view.SetPosition(Vector3Extensions.ToUnityVector3(pos))).AddTo(_disposables);

        //クリック位置の更新
        _view.RPClickPos.Subscribe(pos => _model.SetPosition(Vector3Extensions.ToSystemVector3(pos))).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}

