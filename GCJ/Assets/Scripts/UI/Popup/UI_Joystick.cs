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

        float radius = donut.sizeDelta.x / 2f; // 조이스틱 경계선의 반지름
        Vector2 direction = position - (Vector2)donut.position;

        // 터치 위치가 조이스틱 경계선의 반경 내에 있는지 확인한다.
        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius; // 조이스틱 손잡이의 중심이 조이스틱 경계까지만 이동하도록 제한한다.
        }
        circle.position = (Vector2)donut.position + direction; // 터치 위치로 조이스틱 손잡이를 이동시킨다.

        // 플레이어 컨트롤러에 방향을 전달한다.
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
