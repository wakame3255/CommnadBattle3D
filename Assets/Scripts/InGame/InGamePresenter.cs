using System;
using R3;

public class InGamePresenter : IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public InGamePresenter(InGameModel model, InGameView view)
    {
        model.Initialize();
        view.Initialize();

        model.CurrentGameState.Subscribe(view.UpdateView).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}