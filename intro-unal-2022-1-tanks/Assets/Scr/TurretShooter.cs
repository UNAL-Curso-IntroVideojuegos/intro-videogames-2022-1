using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private Transform _tank;

    [SerializeField]
    private Transform _area;

    [SerializeField]
    private Transform _shootPoint2;
    [SerializeField]
    private float _timer;
    [SerializeField]
    private float _timeShoot;
    
    void Start(){
        _timeShoot = 1f;
        _timer = _timeShoot;
        
    }
    void Update()
    {
        

        Vector2 posTankRed = new Vector2(transform.position.x, transform.position.y);
        Vector2 posTankBlue = new Vector2(_tank.position.x, _tank.position.y);
        float radio = _area.localScale.x/2;

        if (Vector2.Distance(posTankRed,posTankBlue) <=radio){
            _timer -= Time.deltaTime;
            if(_timer <= 0){
                GameObject projectile = Instantiate(_projectilePrefab);
                projectile.transform.position = _shootPoint.position;
                projectile.transform.rotation = _shootPoint.rotation;

                GameObject projectile2 = Instantiate(_projectilePrefab);
                projectile2.transform.position = _shootPoint2.position;
                projectile2.transform.rotation = _shootPoint2.rotation;
                _timer = _timeShoot;
                }

                 
        }
        
    }
}
