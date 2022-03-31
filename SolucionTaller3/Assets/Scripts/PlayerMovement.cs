using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float rotationSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform turret;

    void Update()
    {
        // Movimiento hacia adelanta y atrás
        float vertical = Input.GetAxis("Vertical"); // Para que haya aceleración (Sin aceleracíón: GetAxisRaw)
        transform.position += vertical * speed * Time.deltaTime * transform.up;

        //  Rotación del tanque
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime * -horizontal);
        }
        else if (horizontal > 0)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime * -horizontal);
        }

        //  Mover cañon con el mouse
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos) - transform.position;
        worldPos.Normalize();
        float rotacion = Mathf.Atan2(worldPos.y, worldPos.x) * Mathf.Rad2Deg;
        turret.rotation = Quaternion.Euler(0f, 0f, rotacion - 90);
    }
}
