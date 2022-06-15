using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMobileButton : MonoBehaviour, 
                            IPointerDownHandler, IPointerUpHandler, 
                            IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private bool _isSinglePress = false;
    
    private bool _isHover = false;
    private bool _isBeingPressed = false;
    private bool _wasPressedThisFrame = false;

    public bool IsBeingPressed
    {
        get
        {
            if (_isSinglePress)
            { 
                return _isHover && _isBeingPressed && _wasPressedThisFrame;
            }
            return _isHover && _isBeingPressed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isBeingPressed = true;
        if (_isSinglePress)
        {
            StartCoroutine(SinglePress());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isBeingPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHover = true;
        //TODO: Animation
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHover = false;
        //TODO: Animation
    }

    IEnumerator SinglePress()
    {
        _wasPressedThisFrame = true;
        yield return null;
        _wasPressedThisFrame = false; 
    }
}
