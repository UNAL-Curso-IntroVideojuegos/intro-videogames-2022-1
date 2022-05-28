using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseWobbleLine : MonoBehaviour
{
    public WobbleLine _line;
    public Transform _point;
    public Vector2 _velocity;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _line.OnImpact(_point.position, _velocity);
        }
    }
}
