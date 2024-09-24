using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private bool _canHit = false;
    public bool CanHit { get => _canHit; set => _canHit = value; }

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 myPosition = transform.position;
        Vector2 otherPosition = collision.transform.position;
        Vector2 direction = myPosition - otherPosition;

        if(CalculateAngleWithOY(direction) < 90f && _canHit)
        {
            _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, 0);
            _player.Rb.AddForce(Vector2.up * _player.Force * 0.5f, ForceMode2D.Impulse);
            if(collision.TryGetComponent(out EnemyContrroller enemy))
            {
                enemy.Dead();
            }
        }
    }

    float CalculateAngleWithOY(Vector2 vector)
    {
        Vector2 oy = Vector2.up;

        float angleInRadians = Mathf.Atan2(vector.x, vector.y);

        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        if (angleInDegrees < 0)
        {
            angleInDegrees += 360f;
        }

        if (angleInDegrees > 180f)
        {
            angleInDegrees = 360f - angleInDegrees;
        }

        return angleInDegrees;
    }
}
