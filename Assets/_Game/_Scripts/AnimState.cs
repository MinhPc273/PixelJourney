using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimState : MonoBehaviour
{
    private State _state;
    [SerializeField] Animator _animator;

    public void SetState(State newState)
    {
        if(_state == newState) return;
        _state = newState;
        _animator.SetTrigger(_state.ToString());
    }
}

public enum State
{
    Idle,
    Run,
    Jump,
    Fall,
    Dead
}

