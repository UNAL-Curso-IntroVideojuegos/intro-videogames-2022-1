using UnityEngine;
using Random=UnityEngine.Random;
using System;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class StaticCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private bool _followTarget = false;
    [SerializeField]
    private bool _allowYShake= false;
    
    private Camera _mainCamera;
    private Vector3 _initialPosition;
    private Vector3 _offset;

    private float _smoothTime = 0.3f;
    private Vector3 _smoothVelocity = Vector3.zero;
    
    private float _shakeIntensity = 1f;
    private float _shakeDuration = 0f;
    
    public void StartScreenShake(float intensity, float duration)
    {
        _shakeIntensity = intensity;
        _shakeDuration = duration;
    }

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();

        _initialPosition = _mainCamera.transform.position;
        if (_target != null)
        {
            _offset = _target.position - _initialPosition;
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _initialPosition;
        if (_followTarget && _target != null)
        {
            newPosition = _target.position - _offset;
        }

        if (_mainCamera.transform.position != newPosition || _shakeDuration > 0)
        {
            Vector3 lerpedPosition = Vector3.SmoothDamp(_mainCamera.transform.position, newPosition, ref _smoothVelocity, _smoothTime);
            if (_shakeDuration > 0)
            {
                lerpedPosition = lerpedPosition + Random.insideUnitSphere * (this._shakeIntensity * this._shakeDuration);
                _shakeDuration -= Time.deltaTime;
            }

            if (!_allowYShake)
            {
                lerpedPosition.y = _mainCamera.transform.position.y;
            }

            _mainCamera.transform.position = lerpedPosition;
        }
    }

}
