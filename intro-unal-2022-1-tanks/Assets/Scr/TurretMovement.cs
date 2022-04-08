using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private float speed = 1;
	private float dir = 1;
	[SerializeField]
	private float maxY = 2.4f;
	[SerializeField]
	private float minY = -1.5f;
	[SerializeField]
	private Transform _turret;
	[SerializeField]
	private Transform Tank;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
		float aux = Mathf.Clamp(pos.y, minY, maxY);
		if (aux == maxY || aux == minY) {
			dir *= -1;
		}
        pos += dir * speed * Time.deltaTime * transform.up;
        transform.position = pos;
		
		Vector3 tankPos = Tank.position;
		Vector3 aimVector = tankPos - transform.position;
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
        
        Vector3 rot = _turret.eulerAngles;
        rot.z = angle;
        _turret.eulerAngles = rot;
    }
}
