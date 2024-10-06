using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private AnimState _animState;

    private void OnEnable()
    {
        _animState = GetComponentInChildren<AnimState>();
        _animState.SetState(State.Idle);
    }

    public void Collect()
    {
        if(_animState.State == State.Collect) return;
        _animState.SetState(State.Collect);
        Destroy(this.gameObject, 1f);
    }
}
