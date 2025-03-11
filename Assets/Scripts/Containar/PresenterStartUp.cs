using System;
using VContainer;
using VContainer.Unity;

public class PresenterStartUp : IStartable
{
    private TurnControllerPresenter _turnControllerPresenter;

    private InGamePresenter _inGamePresenter;

    public PresenterStartUp(TurnControllerPresenter turnController, InGamePresenter inGamePresenter)
    {
        _turnControllerPresenter = turnController;

        _inGamePresenter = inGamePresenter;
    }
    public void Start()
    {
       _turnControllerPresenter.Bind();
       _inGamePresenter.Bind();
    }
}