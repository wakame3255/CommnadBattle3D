using System;
using UnityEngine;
using System.Collections.Generic;
public class GoapAgent
{
    private AllCharacterStatus _allCharacterStatus;

    private CountdownTimer _statusTimer;

    private AgentGoal _lastGoal;

    private AgentGoal _currentGoal;


    public GoapAgent(AllCharacterStatus allCharacterStatus)
    {
       _allCharacterStatus = allCharacterStatus;
    }

    
}
