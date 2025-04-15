using System;
using UnityEngine;
using System.Collections.Generic;
public class GoapAgent 
{
    //現在の状態
    private Dictionary<WorldStateKey, object> _worldState = new Dictionary<WorldStateKey, object>();

    //目標状態
    private Dictionary<WorldStateKey, object> _goal = new Dictionary<WorldStateKey, object>();

    //アクションのリスト
    private List<GoapActionBase> _actions = new List<GoapActionBase>();

    //現在のアクション
    private GoapActionBase _currentAction;
}
