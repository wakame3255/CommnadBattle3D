using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterMovementModel
{
   private ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
   public ReadOnlyReactiveProperty<Vector3> Position => _position;

    public CharacterMovementModel()
    {

    }
}
