using System;
using UnityEngine;

public class InGameView : MonoBehaviour, IInitialize
{
    private GameState cullentState;

    public void UpdateView(GameState state)
    {
        cullentState = state;
    }

    public void Initialize()
    {
       
    }
}