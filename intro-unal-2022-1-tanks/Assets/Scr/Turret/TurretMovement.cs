using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private bool _usePoints = true;
    
    [Space(20)]
    [SerializeField]
    private float _min = -1;
    [SerializeField]
    private float _max = 1;
    
    [Space(20)]
    [SerializeField] 
    private Transform _initPoint;
    [SerializeField] 
    private Transform _endPoint;

    [Space(20)]
    [SerializeField]
    private Transform[] _turrets;
    [SerializeField]
    private Transform target;
    
    private int moveDirection = 1;
    
    void Update()
    {
        if(_usePoints)
            MovePoint();
        else
            MoveY();
        
        Look();
    }
    
    void MovePoint()
    {
        float d = Vector3.Distance(_initPoint.position, _endPoint.position);
        float delta = Mathf.PingPong(Time.time * _speed, d);
        transform.position = Vector3.Lerp(_initPoint.position, _endPoint.position, delta / d);
    }

    void MoveY()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += moveDirection * _speed * Time.deltaTime;

        if (currentPos.y >= _max)
            moveDirection = -1; //Cambio direccion
        else if (currentPos.y <= _min) 
            moveDirection = 1; //Cambio direccion

        currentPos.y = Mathf.Clamp(currentPos.y, _min, _max);
        transform.position = currentPos;
    }

    void Look()
    {
        foreach (Transform t in _turrets)
        {
            //t.LookAt(target.position);
            t.up = (target.position - t.position).normalized;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_initPoint == null || _endPoint == null)
        {
            return;
        }
        
        Gizmos.color =Color.red;
        Gizmos.DrawLine(_initPoint.position, _endPoint.position);
    }
}
