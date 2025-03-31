using System;
using System.Collections.Generic;
using R3;

public class ActionContModel : IInitialize
{
    IActionNotice _actionNotice;

    private List<CharacterActionBase> _actionList = new List<CharacterActionBase>();

    public ActionContModel(IActionNotice actionNotice, List<CharacterActionBase> characterActions)
    {
        _actionNotice = actionNotice;
        _actionList = characterActions;
    }

    public void Initialize()
    {
        
    }
}