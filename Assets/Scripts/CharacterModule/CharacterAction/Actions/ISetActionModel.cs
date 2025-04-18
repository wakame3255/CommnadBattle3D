using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public interface ISetActionModel
{
    public Vector3 CharacterPos { get;}

    public void SetActionModel(ActionModelBase characterAction);

    public void SetScopeTarget(List<Collider> targets);
}
