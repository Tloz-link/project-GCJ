using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 조이스틱 조작이 가능한 영역을 감지하는 터치패널 클래스. @홍지형
public class UI_TouchPanel : MonoBehaviour, IPointerDownHandler
{
    public Joystick joystick; // 참조) 조이스틱 클래스

    public void OnPointerDown(PointerEventData eventData)
    {
        joystick.OnPointerDown(eventData);
        joystick.OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.OnPointerUp(eventData); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystick.OnDrag(eventData);
    }
}
