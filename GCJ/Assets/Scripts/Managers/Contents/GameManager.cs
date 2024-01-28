using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private int _monsterKillCount; // ���� óġ��. @ȫ����
    public int MonsterKillCount
    {
        get { return _monsterKillCount; }
        set
        {
            _monsterKillCount = value;
        }
    }

    private int _gold; // ��� ȹ�淮. @ȫ����
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
        }
    }

    private float _currentTime; // ���� �ΰ��� �ð�. @ȫ����
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
