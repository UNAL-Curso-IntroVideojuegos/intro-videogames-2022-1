using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] _turrets;
    private Transform currentTurret;
    [SerializeField]
    private Transform _objetive;
    [SerializeField]
    private Transform _init;
    [SerializeField]
    private Transform _final;

    private Vector3 currentFinal = new Vector3(0,0,0);
    private Vector3 finalPosition;
    private Vector3 currentInitial = new Vector3(0,0,0);
    private Vector3 initialPosition;
    private Vector3 difference;
    private float distance;
    private Vector3 tankPosition;
    [SerializeField]
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        finalPosition = _final.position;
        initialPosition = _init.position;

        for (int i = 0; i < _turrets.Length; i++){
            currentTurret = _turrets[i];
            Vector3 aimVector = _objetive.position - currentTurret.position;
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
            
            Vector3 turretRotation = currentTurret.eulerAngles;
            turretRotation.z = angle;
            currentTurret.eulerAngles = turretRotation;
        }


        if (finalPosition != currentFinal || initialPosition != currentInitial){
            currentFinal = finalPosition;
            currentInitial = initialPosition;
            difference = currentFinal - currentInitial;
            distance = difference.magnitude;
        }
        
        float totalTime = distance / speed;
        float currentFractionTime = Time.fixedTime / totalTime;
        tankPosition = Vector3.Lerp(initialPosition, finalPosition, Mathf.PingPong(currentFractionTime, 1));
        tankPosition.z = 0;

        transform.position = tankPosition;

    }

    private void FixedUpdate()
    {

    }
}
