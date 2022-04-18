using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float shootTime = 2;
    [SerializeField]
    private float shootDistance = 4;
    private float _timer = 0;
    [SerializeField]
    private GameObject _projectilePrefab;
    [Space(20)]
    [SerializeField]
    private Transform[] _shootPoints;
    [SerializeField]
    private Transform rangeArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = (target.position - transform.position).magnitude;
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, shootTime);
        if(_timer == 0 && targetDistance <= shootDistance){
            Debug.Log("Shot!");
            Shoot();
            _timer = shootTime;
        }
        rangeArea.localScale = new Vector3(shootDistance*2, shootDistance*2, 0);
    }

    private void Shoot()
    {
        
        foreach (Transform shootPoint in _shootPoints){
            GameObject projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = shootPoint.position;
            projectile.transform.rotation = shootPoint.rotation;
        }

    }
}
