using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] private Transform limitA;

    [SerializeField] private Transform limitB;

    [SerializeField] private float speed;

    [SerializeField] private Transform lookAtTarget;
    
    [SerializeField] private Transform[] _turrets;

    private Transform _currentLimit;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    Vector3 Move(Vector3 current, Vector3 limit)
    {
        if(current.x < limit.x)
            current +=  speed * Time.deltaTime * transform.right;
        else
            current -=  speed * Time.deltaTime * transform.right;
        if(current.y < limit.y)
            current +=  speed * Time.deltaTime * transform.up;
        else
            current -=  speed * Time.deltaTime * transform.up;
        return current;
    }
    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var topLimit = limitA.position;
        var lowerLimit = limitB.position;

        if (_currentLimit == null || (Math.Abs(position.x - limitB.position.x) < 0.01 && (Math.Abs(position.y - limitB.position.y) < 0.01)))
            _currentLimit = limitA;
        else if( Math.Abs(position.x - limitA.position.x) < 0.01 && (Math.Abs(position.y - limitA.position.y) < 0.01))
            _currentLimit = limitB;

        position = Move(position, _currentLimit.position);
        position = new Vector3(Mathf.Clamp(position.x, Math.Min(lowerLimit.x,topLimit.x), Math.Max(lowerLimit.x,topLimit.x)),
                               Mathf.Clamp(position.y, Math.Min(lowerLimit.y,topLimit.y), Math.Max(lowerLimit.y,topLimit.y)),
                               position.z);
        transform.position = position;

        Vector3 aimVector = lookAtTarget.position - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

        Vector3 rot = lookAtTarget.eulerAngles;
        rot.z = angle;
        for(int i=0;i < _turrets.Length;i++)
        {
            Transform turret = _turrets[i];
            turret.eulerAngles = rot;
        }
    }
}