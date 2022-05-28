using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShowcaseTweenPunch : MonoBehaviour
{
    public float _punch;
    public float _duration = 0.8f;
    public ShowcaseTweenPunchItem[] _items;

    
    public void OnButtonPressed()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Run(_punch, _duration);
        }
    }
}

[Serializable]
public class ShowcaseTweenPunchItem
{
    public Transform _target;
    public int _vibrato = 10;
    public float _elas = 1;

    private Tween _tween;
    
    public void Run(float punch, float duration)
    {
        if (_tween != null && _tween.IsActive())
        {
            _tween.Kill();
        }

        _target.localScale = Vector3.one;
        _tween = _target.DOPunchScale(Vector3.one * punch, duration, _vibrato, _elas);
    }
}