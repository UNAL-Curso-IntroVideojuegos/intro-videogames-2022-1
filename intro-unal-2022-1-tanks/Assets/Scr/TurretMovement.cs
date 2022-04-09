using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{

    // Variables para el movimiento
    private float velocidad = 1;
	private float direccion = 1;
	private float maximoSuperior = 0;
	private float minimoInferior = -3.0f;

    // Variables para mover la torreta
	[SerializeField]
	private Transform _turret;
	[SerializeField]
	private Transform Tank;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Me empezare a mover");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
		float posicionActual = Mathf.Clamp(pos.y, minimoInferior, maximoSuperior);

        // Se verifica si se llega a la posicion mas alta o mas baja
		if (posicionActual == maximoSuperior || posicionActual == minimoInferior) {
			direccion = direccion * -1;
		}

        pos.y += velocidad * direccion * Time.deltaTime;
        transform.position = pos;

		Vector3 tankPos = Tank.position;
		Vector3 aimVector = tankPos - transform.position;
        float angulo = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;

        Vector3 rot = _turret.eulerAngles;
        rot.z = angulo;
        _turret.eulerAngles = rot;
        
    }
}
