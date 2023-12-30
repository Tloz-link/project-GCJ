using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class GameManagerEx
{
    public Player Player { get; private set; }

    public void Init()
    {
        Managers.UI.ShowPopupUI<UI_Joystick>();
        Player = GameObject.Find("Player").GetComponent<Player>();

        Camera.main.gameObject.GetOrAddComponent<FollowCamera>();
    }

    public void SetInputDirection(Vector2 direction)
    {
        Player.SetInputDirection(direction);
    }

    public void Clear()
    {
        FollowCamera camera = Camera.main.gameObject.GetComponent<FollowCamera>();
        if (camera != null )
        {
            Object.Destroy(camera);
        }
    }
}
