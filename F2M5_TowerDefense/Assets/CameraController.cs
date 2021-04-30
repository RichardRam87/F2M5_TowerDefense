using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _panSpeed;
    [SerializeField] private float _boarderThickness;
    [SerializeField] private float _dampening;
    [SerializeField] private Collider _boundsCollider;

    private Vector3 _cameraOffset;

    private void Awake()
    {
        _cameraOffset = new Vector3(1, 10, -5);
    }

    void Update()
    {
        if (Input.mousePosition.x >= Screen.width - _boarderThickness)
        {
            PanCamera(Vector3.right);
        }
        
        else if (Input.mousePosition.x <= _boarderThickness)
        {
            PanCamera(Vector3.left);
        }
        
        if (Input.mousePosition.y >= Screen.height - _boarderThickness)
        {
            PanCamera(Vector3.forward);
        }
        else if (Input.mousePosition.y <= - _boarderThickness)
        {
            PanCamera(Vector3.back);
        }
    }

    private void PanCamera(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * _panSpeed;
        targetPosition.x = Mathf.Clamp(targetPosition.x, _boundsCollider.bounds.min.x, _boundsCollider.bounds.max.x);
        //targetPosition.y = 10;
        targetPosition.z = Mathf.Clamp(targetPosition.z, _boundsCollider.bounds.min.z + -5, _boundsCollider.bounds.max.z -5);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _dampening);

    }
}
