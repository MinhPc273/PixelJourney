using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public enum Type
    {
        Loop,
        ForwardBack
    }

    [SerializeField] Type _type;
    [SerializeField] Transform _pointParent;
    [SerializeField] Transform _saw;

    [SerializeField] Transform _chainPrefabs;
    [SerializeField] Transform _chainParent;
    [SerializeField] float _unitDistanceChain;

    [SerializeField] float _speed;

    [HideInInspector]
    public List<Vector3> _listPoints;

    private int _currentPoint;

    private bool _isMoveForward = true;

    private void Awake()
    {
        _currentPoint = 0;
        _listPoints = new List<Vector3>();
        foreach (Transform point in _pointParent)
        {
            _listPoints.Add(point.position);
        }
        SetupChain();
    }

    private void Start()
    {
        _saw.position = _listPoints[_currentPoint];
    }

    private void Update()
    {
        if (_type == Type.Loop)
        {
            MoveLoop();
        }
        else
        {
            MoveForwardAndBackward();
        }
    }

    private void SetupChain()
    {
        DestroyChain();
        for (int i = 0; i < _listPoints.Count - 1; i++)
        {
            GenerateChains(_listPoints[i], _listPoints[i+1]);
        }
        if (_type == Type.Loop)
        {
            GenerateChains(_listPoints[_listPoints.Count - 1], _listPoints[0]);
        }
    }

    private void DestroyChain()
    {
        foreach (Transform chain in _chainParent)
        {
            DestroyImmediate(chain.gameObject);
        }
    }

    void GenerateChains(Vector3 pointA, Vector3 pointB)
    {
        Vector3 direction = (pointB - pointA).normalized;

        float totalDistance = Vector3.Distance(pointA, pointB);

        Vector3 currentPoint = pointA;

        while (Vector3.Distance(currentPoint, pointA) < totalDistance)
        {

            Instantiate(_chainPrefabs, currentPoint, Quaternion.identity, _chainParent);

            currentPoint += direction * _unitDistanceChain;

            if (Vector3.Distance(currentPoint, pointB) < _unitDistanceChain / 2)
            {
                break;
            }
        }

        if (Vector3.Distance(currentPoint, pointB) >= 0)
        {
            Instantiate(_chainPrefabs, pointB, Quaternion.identity, _chainParent);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        _listPoints.Clear();
        foreach (Transform point in _pointParent)
        {
            _listPoints.Add(point.position);
        }
        for (int i = 0; i < _listPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(_listPoints[i], _listPoints[i + 1]);
        }
        if (_type == Type.Loop)
        {
            Gizmos.DrawLine(_listPoints[_listPoints.Count - 1], _listPoints[0]);
        }
    }

    private void MoveLoop()
    {
        _saw.transform.transform.position = Vector3.MoveTowards(_saw.position, _listPoints[_currentPoint + 1], _speed * Time.deltaTime);
        if (Vector3.Distance(_saw.position, _listPoints[_currentPoint + 1]) < 0.01f)
        {
            _currentPoint++;
            if (_currentPoint == _listPoints.Count - 1)
            {
                _currentPoint = -1;
            }
        }
    }

    private void MoveForwardAndBackward()
    {
        _saw.transform.transform.position = Vector3.MoveTowards(_saw.position, _listPoints[_currentPoint + 1], _speed * Time.deltaTime);
        if (Vector3.Distance(_saw.position, _listPoints[_currentPoint + 1]) < 0.01f)
        {
            if(_isMoveForward){
                _currentPoint++;
                if (_currentPoint == _listPoints.Count - 1)
                {
                    _isMoveForward = false;
                    _currentPoint--;
                }
            }
            else
            {
                _currentPoint--;
                if (_currentPoint == -1)
                {
                    _isMoveForward = true;
                }
            }
        }
    }
}
