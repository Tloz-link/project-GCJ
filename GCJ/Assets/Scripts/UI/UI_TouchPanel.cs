using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ���̽�ƽ ������ ������ ������ �����ϴ� ��ġ�г� Ŭ����. @ȫ����
public class UI_TouchPanel : MonoBehaviour, IPointerDownHandler
{
    public Joystick joystick; // ����) ���̽�ƽ Ŭ����

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
