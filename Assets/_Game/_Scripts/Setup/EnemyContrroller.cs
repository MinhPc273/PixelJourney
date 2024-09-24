using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContrroller : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] float _speed;
    [SerializeField] float _distance;

    [Header("Die")]
    [SerializeField] float _forceDie;

    private Vector3 _pointLeft;
    private Vector3 _pointRight;
    private Vector3 _currentPoint;

    private AnimState _anim;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<AnimState>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _pointLeft = transform.position + Vector3.left * _distance;
        _pointRight = transform.position + Vector3.right * _distance;
        _currentPoint = _pointLeft;
        transform.position = _currentPoint;
    }

    private void Update()
    {
        Vector2 point = _currentPoint - transform.position;

        if (point.magnitude < 0.5f)
        {
            if (_currentPoint == _pointLeft)
            {
                _currentPoint = _pointRight;
            }
            else
            {
                _currentPoint = _pointLeft;
            }
        }

        if (_currentPoint == _pointLeft)
        {
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Dead()
    {
        _collider.enabled = false;
        _rb.AddForce(Vector2.up * _forceDie, ForceMode2D.Impulse);
        _anim.SetState(State.Dead);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (_pointLeft != Vector3.zero)
        {
            Gizmos.DrawLine(_pointLeft, _pointRight);
        }
        else
        {
            Gizmos.DrawCube(transform.position, new Vector3(_distance * 2, 0.1f, 0f));
        }
    }
}
