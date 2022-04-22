using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
private GameObject _ProyectilPrefab;
private Transform _shootPoint; 
void Update()  { if (Input.GetButtonDown("Shoot")) 
{
GameObject projectile = Instantiate(_ProyectilePrefab);
projectile.transform.position = _shootPoint.position;
projectile.transform.rotation = _shootPoint.rotation;
}   
}
}
