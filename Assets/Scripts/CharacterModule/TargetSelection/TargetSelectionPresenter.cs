using System;
using R3;

public class TargetSelectionPresenter : IBinder, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private TargetSelectionModel _model = default;
    private TargetSelectionView _view = default;

    private SelectTargetStatusView _statusView = default;
    public TargetSelectionPresenter(TargetSelectionModel model, TargetSelectionView view, SelectTargetStatusView statusView)
    {
        _model = model;
        _view = view;
        _statusView = statusView;
    }

    public void Bind()
    {
        _view.RPSelectedTarget.Subscribe(_model.CheckTarget).AddTo(_disposables);

        _model.RPCharacterStatus.Subscribe(_statusView.SetCharacterInfomation).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
