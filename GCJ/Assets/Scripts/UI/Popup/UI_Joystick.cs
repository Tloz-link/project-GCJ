using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Joystick : UI_Popup
{
    enum GameObjects
    {
        TouchPanel,
        Circle,
        Donut
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.TouchPanel).BindEvent(OnPressed, Define.UIEvent.Pressed);
        GetObject((int)GameObjects.TouchPanel).BindEvent(OnPointerDown, Define.UIEvent.PointerDown);
        GetObject((int)GameObjects.TouchPanel).BindEvent(OnPointerUp, Define.UIEvent.PointerUp);

        HideJoystick();
        return true;
    }

    public virtual void OnPressed()
    {
        Vector2 position = Input.mousePosition;
        // Debug.Log("direction" + direction);

        RectTransform donut = GetObject((int)GameObjects.Donut).GetComponent<RectTransform>();
        RectTransform circle = GetObject((int)GameObjects.Circle).GetComponent<RectTransform>();

        float radius = donut.sizeDelta.x / 2f; // ���̽�ƽ ��輱�� ������
        Vector2 direction = position - (Vector2)donut.position;

        // ��ġ ��ġ�� ���̽�ƽ ��輱�� �ݰ� ���� �ִ��� Ȯ���Ѵ�.
        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius; // ���̽�ƽ �������� �߽��� ���̽�ƽ �������� �̵��ϵ��� �����Ѵ�.
        }
        circle.position = (Vector2)donut.position + direction; // ��ġ ��ġ�� ���̽�ƽ �����̸� �̵���Ų��.

        // �÷��̾� ��Ʈ�ѷ��� ������ �����Ѵ�.
        Managers.Game.SetInputDirection(direction / radius);
    }

    public virtual void OnPointerDown()
    {
        Vector2 position = Input.mousePosition;
        GetObject((int)GameObjects.Donut).transform.position = position;
        GetObject((int)GameObjects.Circle).transform.position = position;

        ShowJoystick();
    }

    public virtual void OnPointerUp()
    {
        HideJoystick();
        Managers.Game.SetInputDirection(Vector2.zero);
    }

    private void HideJoystick()
    {
        GetObject((int)GameObjects.Circle).SetActive(false);
        GetObject((int)GameObjects.Circle).transform.position = Vector3.zero;

        GetObject((int)GameObjects.Donut).SetActive(false);
        GetObject((int)GameObjects.Donut).transform.position = Vector3.zero;
    }

    private void ShowJoystick()
    {
        GetObject((int)GameObjects.Circle).SetActive(true);
        GetObject((int)GameObjects.Donut).SetActive(true);
    }
}
