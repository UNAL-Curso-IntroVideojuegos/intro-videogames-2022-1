using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRaidCast : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;
    private Rigidbody2D _rb;
    private void Start() {
        Init();
    }
    private void Init(){
        _rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate() {
        Vector2 dir = transform.up;
        Vector2 movement = dir * _speed * Time.fixedDeltaTime;
        Vector2 pos = _rb.position + movement;

        _rb.MovePosition(pos);
    }

    private void CheckCollision(Vector2 movement){
         // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, transform.up, movement.magnitude);
        
        Debug.Log("Out here");
        // If it hits something...
        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.transform.GetComponent<Enemy>();
            if(enemy){
                enemy.TakeDamage();
            }
            Debug.Log("Here");
            DestroyProjectile();
        }
    }
    private void DestroyProjectile(){
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}

