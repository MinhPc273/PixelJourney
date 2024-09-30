using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private AnimState _animState;
    private bool _isCollected = false;

    private void Awake()
    {
        _animState = GetComponent<AnimState>();
    }

    private void Update()
    {
        if(_isCollected) return;
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    public void Collect()
    {
        if (_animState.State == State.Collect) return;
        PlayerController.Current.TakeKey();
        _isCollected = true;
        _animState.SetState(State.Collect);
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
