using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private int _monsterKillCount; // ¸ó½ºÅÍ Ã³Ä¡¼ö. @È«ÁöÇü
    public int MonsterKillCount
    {
        get { return _monsterKillCount; }
        set
        {
            _monsterKillCount = value;
        }
    }

    private int _gold; // °ñµå È¹µæ·®. @È«ÁöÇü
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
        }
    }

    private float _currentTime; // ÇöÀç ÀÎ°ÔÀÓ ½Ã°£. @È«ÁöÇü
    public float CurrentTime
    {
        get { return _currentTime; }
        set
        {
            _currentTime = value;
        }
    }

    #region Hero
    private Vector2 _moveDir;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }

    private Define.EJoystickState _joystickState;
    public Define.EJoystickState JoystickState
    {
        get { return _joystickState; }
        set
        {
            _joystickState = value;
            OnJoystickStateChanged?.Invoke(_joystickState);
        }
    }
    #endregion

    #region Action
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<Define.EJoystickState> OnJoystickStateChanged;
    #endregion
}
