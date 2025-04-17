using System;
using UnityEngine;

public class GoapFactory : MonoBehaviour
{
    public IGoapPlanner CreateGoapPlanner()
    {
        return new GoapPlanner();
    }
}
