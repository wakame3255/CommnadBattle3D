using System;
using R3;

public class InGamePresenter : IBinder ,IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private InGameModel _model;

    private InGameView _view;

    public InGamePresenter(InGameModel model, InGameView view)
    {
        _model = model;

        _view = view;

        model.Initialize();
        view.Initialize();     
    }

    public void Bind()
    {
        _model.CurrentGameState.Subscribe(_view.UpdateView).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}