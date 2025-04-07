using System;
using R3;

public class TargetSelectionPresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private TargetSelectionModel _model = default;
    private TargetSelectionView _view = default;
    public TargetSelectionPresenter(TargetSelectionModel model, TargetSelectionView view)
    {
        _model = model;
        _view = view;
    }

    public void Bind()
    {
        _view.RPSelectedTarget.Subscribe(_model.CheckTarget).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
