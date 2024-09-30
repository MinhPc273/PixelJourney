using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] Transform _hinge;
    [SerializeField] Transform _chainSpiked;
    [SerializeField] Transform _spikedBall;

    [Range(0f, 200f)]
    [SerializeField] float _rotationSpeed = 1.0f;
    [Range(0f, 360f)]
    [SerializeField] float _angle = 50f;

    [SerializeField] float radius = 0.5f;

    private bool isFoward = true;

    [Header("Loop")]
    [SerializeField] bool _isLoop;
    [SerializeField] bool _isClockwise;
    [Range(0, 360f)]
    [SerializeField] float _startAngleLoop;
    private Vector3 _start;
    private float _timer;

    private float _startAngle;

    void Start()
    {
        SetUpHinge();
        if (_isLoop)
        {
            _timer = _startAngleLoop + 90;
        }
        else
        {
            _timer = -_angle / 2;
        }
    }


    private void SetUpHinge()
    {
        DestroyHinge();
        Vector3 maxHinge = _hinge.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * -90f), Mathf.Sin(Mathf.Deg2Rad * -90f)) * radius;
        Vector3 direction = (maxHinge - _hinge.position).normalized;
        float _unitDistanceChain = 0.6f;

        float totalDistance = Vector3.Distance(_hinge.position, maxHinge);

        Vector3 currentPoint = _hinge.position;

        while (Vector3.Distance(currentPoint, _hinge.position) < totalDistance)
        {

            Instantiate(_chainSpiked, currentPoint, Quaternion.identity, _hinge);

            currentPoint += direction * _unitDistanceChain;

            if (Vector3.Distance(currentPoint, maxHinge) < _unitDistanceChain / 2)
            {
                break;
            }
        }

        Instantiate(_spikedBall, maxHinge, Quaternion.identity, _hinge);
    }

    private void DestroyHinge()
    {
        foreach (Transform child in _hinge)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        CaculateTimer();
        _hinge.rotation = Quaternion.Euler(0, 0, _timer);
    }

    private void CaculateTimer()
    {
        if(_isLoop)
        {
            if (!_isClockwise)
            {
                _timer += Time.deltaTime * _rotationSpeed;
                if (_timer >= 360 * 2)
                {
                    _timer = 0;
                }
            }
            else
            {
                _timer -= Time.deltaTime * _rotationSpeed;
                if (_timer <= -360 * 2)
                {
                    _timer = 0;
                }
            }
            return;
        }
        if (isFoward)
        {
            _timer += Time.deltaTime * _rotationSpeed;
            if (_timer >= _angle / 2)
            {
                isFoward = false;
            }
        }
        else
        {
            _timer -= Time.deltaTime * _rotationSpeed;
            if (_timer <= -_angle / 2)
            {
                isFoward = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_isLoop)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);

            Gizmos.color = Color.yellow;
            _start = _hinge.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * _startAngleLoop), Mathf.Sin(Mathf.Deg2Rad * _startAngleLoop)) * radius;
            Gizmos.DrawLine(transform.position, _start);

        }
        else
        {
            Gizmos.color = Color.red;
            DrawArc(transform.position, radius, -90 - _angle / 2, _angle);
        }
    }

    private void DrawArc(Vector3 center, float radius, float startAngle, float angleRange, int segmentCount = 20)
    {
        float angleStep = angleRange / segmentCount;
        Vector3 previousPoint = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * startAngle), Mathf.Sin(Mathf.Deg2Rad * startAngle)) * radius;
        Gizmos.DrawLine(transform.position, previousPoint);
        for (int i = 1; i <= segmentCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 currentPoint = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * radius; // Tính vị trí điểm hiện tại

            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
        Gizmos.DrawLine(transform.position, previousPoint);
    }
}
