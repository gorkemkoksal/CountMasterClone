using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnStart;

    private bool _isFirstTouch = true;
    private Vector2 _touchPosition;
    public float TouchDirectionOnX { get; private set; }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isFirstTouch)
        {
            OnStart?.Invoke();
            _isFirstTouch = false;
        }

        _touchPosition = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var deltaTouch = eventData.position.x - _touchPosition.x;
        TouchDirectionOnX = deltaTouch;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        TouchDirectionOnX = 0;
    }
}
