using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPointerUpHandler = null;

    private bool isPressed = false;

    private void Update()
    {
        if (isPressed)
        {
            OnPressedHandler?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
	{
		OnClickHandler?.Invoke();
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        OnPointerDownHandler?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        OnPointerUpHandler?.Invoke();
    }
}
