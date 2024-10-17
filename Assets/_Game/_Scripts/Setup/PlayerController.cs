using JunEngine;
using System;
using System.Collections;
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


    private float _horizontal;
    private float _horizontalCurrent;
    private float _horizontalTemp;

    private bool _canJump = true;
    private bool _isInvincible = false;
    private bool _isDead = false;

    [Header("Stat")]
    public int HpMAX = 3;
    private int _hp = 3;

    public int Hp => _hp;

    private int _key = 0;
    private int _maxKey = 3;

    private int _fruit = 0;

    public int Fruit => _fruit;
    public int Key => _key;
    public int MaxKey => _maxKey;

    [SerializeField] GameObject _shield;

    public Action OnTakeDame;
    public Action OnTakeKey;
    public Action OnTakeFruit;

    public GamePlayCanvas GamePlayCanvas;

    public GameObject MessKey;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<AnimState>();
    }

    private void Start()
    {
        GameManager.Current.OnInitGame += InitGame;
        InitGame();
    }

    private void InitGame()
    {
        MessKey.SetActive(false);
        _fruit = 0;
        _key = 0;
        _hp = HpMAX;
        _isDead = false;
        _canJump = true;
        _isInvincible = false;
        _shield.SetActive(false);
        GamePlayCanvas.Init();
    }

    void Update()
    {
        if (_isDead) return;
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
            //transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            MessKey.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_rb.velocity.x < 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            MessKey.transform.localRotation = Quaternion.Euler(0, 180, 0);
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

    public void CollectFruit()
    {
        _fruit++;
        OnTakeFruit?.Invoke();
    }

    public void TakeDame()
    {
        if(_isInvincible || _isDead)
        {
            return;
        }

        _hp--;
        if(_hp <= 0)
        {
            _hp = 0;
            OnTakeDame?.Invoke();
            StartCoroutine(Dead());
            return;
        }

        Knock();
        StartCoroutine(Shield());

        OnTakeDame?.Invoke();
    }

    private IEnumerator Dead()
    {
        _rb.velocity = Vector3.zero;
        _isDead = true;
        _anim.SetState(State.Dead);
        yield return new WaitForSeconds(1f);
        GameManager.Current.Lose();
    }

    public void Knock()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * _force * 0.75f, ForceMode2D.Impulse);
    }

    private IEnumerator Shield()
    {
        _isInvincible = true;
        _shield.SetActive(true);
        _shield.GetComponent<Animator>().SetTrigger("Enter");
        yield return new WaitForSeconds(1.5f);
        _shield.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(0.5f);
        _isInvincible = false;
        _shield.SetActive(false);
    }

    public void TakeKey()
    {
        _key++;
        OnTakeKey?.Invoke();
    }

    public void CheckFinish(Action done)
    {
        if(_key == _maxKey)
        {
            StartCoroutine(Finish());
            done?.Invoke();
        }
        else
        {
            MessKey.SetActive(true);
        }
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Current.Win();
        Pref.Fruit += _fruit;
    }
}
