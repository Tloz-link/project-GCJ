using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public Player Player { get; private set; }

    public void Init()
    {
        Managers.UI.ShowPopupUI<UI_Joystick>();
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void SetInputDirection(Vector2 direction)
    {
        Player.SetInputDirection(direction);
    }

    public void Clear()
    {

    }
}
