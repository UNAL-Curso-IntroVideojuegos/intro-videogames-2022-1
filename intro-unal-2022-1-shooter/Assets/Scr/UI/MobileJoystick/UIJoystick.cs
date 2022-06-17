using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UIJoystickType
{
    Static,
    Dynamic,
}

public class UIJoystick : MonoBehaviour
{
    [Header("Configuration")]
    public UIJoystickType joystickType = UIJoystickType.Static;
    public float knobRange = 50f;
    [Range(0f, 1f), Tooltip("% of the range that will be the deadzone")]
    public float deadZone = 0.3f;
    public Rect contactArea = new Rect(0, 0, 1f, 1f);

    [Header("Visuals")]
    public RectTransform background;
    public RectTransform knob;

    protected RectTransform rectTransform;
    
    protected CanvasScaler mainCanvasScaler;
    protected Image backgroundImage;
    protected Image knobImage;
    protected Vector2 baseJoystickPosition;

    protected bool isBeingHeld = false;
    protected bool isInDeadZone = false;
    protected int activeFingerId = -1;
    protected Vector2 initialTouchPosition = Vector2.zero;
    protected Vector2 initialDragOrigin = Vector2.zero;
    protected Vector2 currentDelta = Vector2.zero;

    public bool IsBeingHeld => this.isBeingHeld;
    public Vector2 Delta => this.isInDeadZone || !this.isBeingHeld ? Vector2.zero : this.currentDelta;

    private void Start()
    {
        OnInit();
    }

    private void OnDisable()
    {
        if (this.isBeingHeld) {
            this.OnStopTouch(-1);
        }
    }

    void OnInit()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.mainCanvasScaler = this.GetComponentInParent<CanvasScaler>();
        this.backgroundImage = this.background.transform.GetComponent<Image>();
        this.knobImage = this.knob.transform.GetComponent<Image>();

        Vector2 rectSize = this.rectTransform.rect.size;
        Vector2 pivotDelta = this.rectTransform.pivot - new Vector2(0.5f, 0.5f);
        Vector3 positionDelta = new Vector2(pivotDelta.x * rectSize.x, pivotDelta.y * rectSize.y);

        this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        this.rectTransform.localPosition -= positionDelta;
    }

    public void Update()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (Input.touchCount <= 0) {
            this.ForceTouchStop();
        } else {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began) {
                    this.OnStartTouch(touch.fingerId, touch.position);
                } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                    this.OnStopTouch(touch.fingerId);
                } else if (touch.phase == TouchPhase.Moved) {
                    this.OnMoved(touch.fingerId, touch.position);
                }
            }
        }
#else
        if (Input.GetMouseButtonDown(0)) {
            this.OnStartTouch(-1, Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            this.OnStopTouch(-1);
        }

        if (this.isBeingHeld) {
            this.OnMoved(-1, Input.mousePosition);
        }
#endif
    }

    void OnStartTouch(int fingerId, Vector2 touchPosition)
    {
        if (this.isBeingHeld) {
            return;
        }

        Rect realContactArea = new Rect(this.contactArea.x * Screen.width, this.contactArea.y * Screen.height, this.contactArea.width * Screen.width, this.contactArea.height * Screen.height);

        if (!realContactArea.Contains(touchPosition)) {
            return;
        }

        if (this.IsPointerOverBlockingObject(touchPosition)) {
            return;
        }

        this.isBeingHeld = true;
        this.activeFingerId = fingerId;
        this.initialTouchPosition = touchPosition;
        this.baseJoystickPosition = this.rectTransform.position;

        // If the joystick is dynamic, we move it first
        if (this.joystickType == UIJoystickType.Dynamic) {
            this.rectTransform.position = touchPosition;

            this.initialDragOrigin = touchPosition;
        } else {
            this.initialDragOrigin = this.baseJoystickPosition;
        }

        this.OnMoved(fingerId, touchPosition);
    }

    void OnStopTouch(int fingerId)
    {
        if (!this.isBeingHeld || this.activeFingerId != fingerId) {
            return;
        }

        this.isBeingHeld = false;
        this.activeFingerId = -1;
        this.initialTouchPosition = Vector2.zero;
        this.initialDragOrigin = Vector2.zero;

        // If the joystick is dynamic, we move it back to the original
        if (this.joystickType == UIJoystickType.Dynamic) {
            this.rectTransform.position = this.baseJoystickPosition;
        }

        this.knob.localPosition = Vector2.zero;
    }

    void OnMoved(int fingerId, Vector2 touchPosition)
    {
        if (!this.isBeingHeld || this.activeFingerId != fingerId) {
            return;
        }

        float currentCanvasScale = Mathf.Pow(Screen.width / this.mainCanvasScaler.referenceResolution.x, 1f - this.mainCanvasScaler.matchWidthOrHeight)
            * Mathf.Pow(Screen.height / this.mainCanvasScaler.referenceResolution.y, this.mainCanvasScaler.matchWidthOrHeight);

        Vector2 currentPositionDelta = (touchPosition - this.initialDragOrigin) / currentCanvasScale;
        float currentDistance = Mathf.Sqrt(currentPositionDelta.x * currentPositionDelta.x + currentPositionDelta.y * currentPositionDelta.y);

        // Calculate dead zone
        this.isInDeadZone = (currentDistance < (this.knobRange * this.deadZone)) ? true : false;

        if (currentDistance > this.knobRange) {
            this.currentDelta = currentPositionDelta / currentDistance;
        } else {
            this.currentDelta = currentPositionDelta / this.knobRange;
        }

        this.knob.localPosition = this.currentDelta * this.knobRange;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Rect contactAreaScreen = new Rect(this.contactArea.x * Screen.width, this.contactArea.y * Screen.height, this.contactArea.width * Screen.width, this.contactArea.height * Screen.height);

        Gizmos.DrawWireCube(contactAreaScreen.center, new Vector3(contactAreaScreen.width, contactAreaScreen.height, 0));
    }

    public virtual bool IsPointerOverBlockingObject(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        eventDataCurrentPosition.position = position;

        EventSystem.current.RaycastAll(eventDataCurrentPosition, raycastResults);

        return raycastResults.Count > 0;
    }
    
    public void ForceTouchStop() => this.OnStopTouch(this.activeFingerId);
}
