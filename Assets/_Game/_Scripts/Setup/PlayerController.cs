using JunEngine;
using UnityEngine;

public class PlayerController : MMSingleton<PlayerController>
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

    private float _horizontal;
    private float _horizontalCurrent;
    private float _horizontalTemp;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<AnimState>();
    }

    void Update()
    {
        //get input
        if (InputManager.Current._InputType == InputType.Keyboard)
        {
            KeyBoardInput();
        }
        else
        {
           MobileInput();
        }
        // move 
        Move(_horizontal);
        //flip
        if (_rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //set animation
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
        //check if can jump
        if (_rb.velocity.y < 0)
        {
            _canJump = true;
            _hitBox.CanHit = true;
        }
        else
        {
            _hitBox.CanHit = false;
        }
    }

    private void MobileInput()
    {
        if (InputManager.Current.BtnLeft.IsSlected())
        {
            _horizontal = -1;
        }
        else if (InputManager.Current.BtnRight.IsSlected())
        {
            _horizontal = 1;
        }
        else
        {
            _horizontal = 0;
        }
        if (InputManager.Current.BtnJump.IsSlected() && isGrounded() && _canJump)
        {
            _rb.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            _canJump = false;
        }
    }

    private void KeyBoardInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _horizontal = 1;
        }
        else
        {
            _horizontal = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && _canJump)
        {
            _rb.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            _canJump = false;
        }
    }

    private void Move(float horizontal)
    {
        _horizontalCurrent = horizontal;
        if (_horizontalCurrent != horizontal)
        {
            _horizontalTemp = 0;
        }
        if (horizontal == 0)
        {
            _horizontalTemp = 0;
        }
        if (horizontal == 1 && _horizontalTemp <= 1)
        {
            _horizontalTemp += 0.05f;
        }
        else if (horizontal == -1 && _horizontalTemp >= -1)
        {
            _horizontalTemp -= 0.05f;
        }
        _rb.velocity = new Vector2(_horizontalTemp * _speed, _rb.velocity.y);
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
