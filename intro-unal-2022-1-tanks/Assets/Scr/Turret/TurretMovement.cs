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
    private Transform currentTurret;
    [SerializeField]
    private Transform target;
    
    private int moveSelect = 1;
    
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
        currentPos.y += moveSelect * _speed * Time.deltaTime;

        if (currentPos.y >= _max)
            moveSelect = -1;
        else if (currentPos.y <= _min) 
            moveSelect = 1;

        currentPos.y = Mathf.Clamp(currentPos.y, _min, _max);
        transform.position = currentPos;
    }

    void Look()
    {
        /* foreach (Transform t in _turrets)
        {
            //t.LookAt(target.position);
            t.up = (target.position - t.position).normalized;
        }*/

        for (int i = 0; i < _turrets.Length; i++){
            currentTurret = _turrets[i];
            Vector3 aimVector = target.position - currentTurret.position;
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

            Vector3 turretRotation = currentTurret.eulerAngles;
            turretRotation.z = angle;
            currentTurret.eulerAngles = turretRotation;
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
