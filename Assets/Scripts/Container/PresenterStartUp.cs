using System;
using VContainer;
using VContainer.Unity;

public class PresenterStartUp : IStartable
{
    private TurnControllerPresenter _turnControllerPresenter;

    private InGamePresenter _inGamePresenter;

    private PlayerCharacterContPresenter _playerCharacterContPresenter;

    public PresenterStartUp(TurnControllerPresenter turnController, InGamePresenter inGamePresenter, PlayerCharacterContPresenter playerCharacter)
    {
        _turnControllerPresenter = turnController;

        _inGamePresenter = inGamePresenter;

        _playerCharacterContPresenter = playerCharacter;
    }
    public void Start()
    {
        //各プレゼンターのバインド処理
        _turnControllerPresenter.Bind();
       _inGamePresenter.Bind();
        _playerCharacterContPresenter.Bind();
    }
}