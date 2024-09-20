using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _force;

    [SerializeField] Vector2 _boxSize;
    [SerializeField] float _castDistance;
    [SerializeField] LayerMask _groundLayer;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(horizontal * _speed, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _rb.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
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
