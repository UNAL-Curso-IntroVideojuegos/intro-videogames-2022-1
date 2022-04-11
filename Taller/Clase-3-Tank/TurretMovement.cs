using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] 
	private float AxisMin = -1.0f, AxisMax = 1.0f;

	[SerializeField]
	private Transform mTarget;

    [SerializeField]
    private Transform[] _turrets;

	[SerializeField]
    private Transform Point_A;

	[SerializeField]
    private Transform Point_B;
	
    private float timeValue = 0.0f;
	[SerializeField]
	private float speed = 0.3f;

	[SerializeField]
    private Transform Pivot;
    
    void Update()
    {
        // Movement with one axis - Movimiento sobre un eje en este caso eje vertical Y:
		// Increase animation time.
        //timeValue = timeValue + Time.deltaTime;
		// Compute the sin position.
        //float AxisValue = Mathf.Abs(AxisMin)*Mathf.Sin(timeValue*2f);
        // Now compute the Clamp value.
        //float AxisPos = Mathf.Clamp(AxisValue, AxisMin, AxisMax);
        // Update the position of the tank.
        //transform.position = new Vector3(-3.0f, AxisPos, 0.0f);
        // Reset the animation time if it is greater than the planned time.
        //if (AxisValue > Mathf.PI * 2.0f)
        //{
        //    timeValue = 0.0f;
		//}
		
		// Movement between 2 points - Movimiento entre dos puntos en este caso el punto A y el punto B:
		// Definiendo los vectores de positión inicial y final.
        Vector3 startPosition = Point_A.transform.position;
	    Vector3 endPosition = Point_B.transform.position;
        // Incrementando el tiempo de animación.
		timeValue += Time.deltaTime;
		// Actualizando el vector de posición del tanque según la posición inicial y final de los vectores A y B.
		transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(speed*timeValue, 1.0f));

		// Turret(s) Rotation - Rotación de torreta(s):	
		foreach (Transform child in _turrets)
		{
			Vector3 difference = mTarget.transform.position - child.transform.position;
        	float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
			child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
		}
	}
}
