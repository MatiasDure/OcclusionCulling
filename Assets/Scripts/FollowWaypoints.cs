using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    private float DISTANCE_LIMIT = 1f;
    
    [SerializeField] private GameObject[] _waypoints;
    [SerializeField] private float _speed;

    private bool _isFollowing = true;
    private uint _waypointIndex = 0;
    private GameObject _currentWaypoint;

    public static event Action OnLastWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        _currentWaypoint = _waypoints[_waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFollowing) return;

        UpdateWaypoint();
    }

    private void Next()
    {
        if (_isFollowing && _waypointIndex + 1 == _waypoints.Length)
        {
            _isFollowing = false;
            OnLastWaypoint?.Invoke();
            return;
        }

        _currentWaypoint = _waypoints[++_waypointIndex];
    }

    private bool Near(Vector3 pDirection)
    {
        float distance = pDirection.magnitude;

        return distance <= DISTANCE_LIMIT;
    }

    private void UpdateWaypoint()
    {

        Vector3 direction =  _currentWaypoint.transform.position - transform.position;

        Move(direction);

        if (Near(direction)) Next();
    }

    private void Move(Vector3 pDirection)
    {
        Vector3 moveVector = pDirection.normalized * _speed * Time.deltaTime;
        transform.position += moveVector;
        transform.LookAt(_currentWaypoint.transform);
    }

}
