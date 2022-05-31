using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowcaseTween : MonoBehaviour
{
    public float _fromX = -5;
    public float _toX = 5;
    public float _time = 1;
    public ShowcaseTweenMoveItem[] _items;

    
    public void OnButtonPressed()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Run(_fromX, _toX, _time);
        }
    }
}

[Serializable]
public class ShowcaseTweenMoveItem
{
    public Transform _transform;
    public Ease _ease;
    private Tween _tween;

    public void Run(float from, float to, float time, float delay = 0, bool yoyo = true)
    {
        if (_tween != null && _tween.IsActive())
        {
            _tween.Kill();
        }
        _tween = _transform.DOMoveX(to, time).From(from).SetEase(_ease).SetDelay(delay);
        _transform.DORotate(_transform.eulerAngles + Vector3.up * 180, time);
        if (yoyo)
        {
            _tween.OnComplete(() => Run(to, from, time, 0.25f, false));
        }
    }
}