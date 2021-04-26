using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _arrivalthreshold = 0.1f;
    
    private Path _path;
    private Waypoint _currentWaypoint;
    
    void Start()
    {
        /* TODO: we should have a system for creating paths so that a path is formed only on levelstart */
        SetupPath();
        _currentWaypoint = _path.GetPathStart();
    }

    private void SetupPath()
    {
        _path = new Path();
        // sort based on name, we might need to change this when the level is spawned
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>().OrderBy(wp => wp.gameObject.name).ToArray();

        _path.SetWaypoints(waypoints);
    }
    
    void Update()
    {
        Vector3 heightOffsetPosition = new Vector3(_currentWaypoint.Position.x,
            transform.position.y, _currentWaypoint.Position.z);
        float distance = Vector3.Distance(transform.position, heightOffsetPosition);
        
        if (distance <= _arrivalthreshold)
        {
            if (_currentWaypoint == _path.GetPathEnd())
            {
                print("Ik ben bij het eindpunt");
                // TODO: Implement UnityEvent, for now disable this script
                this.enabled = false;
            }
            else
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
            }
        }
        else
        {
            transform.LookAt(heightOffsetPosition);
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
