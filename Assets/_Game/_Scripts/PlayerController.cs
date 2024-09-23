using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _speed;
    [SerializeField] float _force;

    public float Force => _force;

    [SerializeField] Vector2 _boxSize;
    [SerializeField] float _castDistance;
    [SerializeField] LayerMask _groundLayer;

    [Header("HitBox")]
    [SerializeField] HitBox _hitBox;

    private AnimState _anim;

    private Rigidbody2D _rb;

    public Rigidbody2D Rb => _rb;

    private bool _canJump = false;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<AnimState>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(horizontal * _speed, _rb.velocity.y);
         
        if(_rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isGrounded())
        {
            if (_rb.velocity.x != 0)
            {
                _anim.SetState(State.Run);
            }
            else
            {
                _anim.SetState(State.Idle);
            }
        }
        else
        {
            if (_rb.velocity.y > 0)
            {
                _anim.SetState(State.Jump);
            }
            else
            {
                _anim.SetState(State.Fall);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && _canJump)
        {
            _rb.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            _canJump = false;
        }

        if(_rb.velocity.y < 0)
        {
            _canJump = true;
            _hitBox.CanHit = true;
        }
        else
        {
            _hitBox.CanHit = false;
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _boxSize, 0f, -transform.up, _castDistance, _groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.down * _castDistance), new Vector3(_boxSize.x, _boxSize.y, 0));
    }
}
