using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private GameObject _joystickUI; // 조이스틱 오브젝트 @홍지형
    public GameObject JoystickUI
    {
        get { return _joystickUI; }
        set
        {
            _joystickUI = value;
        }
    }

    private float _currentTime = 0; // 현재 인게임 시간. @홍지형
    public float CurrentTime
    {
        get { return _currentTime; }
        set
        {
            _currentTime = value;
        }
    }

    private bool _isGamePaused = false; // 게임의 일시정지 상태를 추적하는 변수  @홍지형
    public bool IsGamePaused
    {
        get { return _isGamePaused; }
        set
        {
            _isGamePaused = value;
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

    private int _monsterKillCount = 0; // 몬스터 처치수. @홍지형
    public int MonsterKillCount
    {
        get { return _monsterKillCount; }
        set
        {
            _monsterKillCount = value;
        }
    }

    private int _gold = 0; // 골드 획득량. @홍지형
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
        }
    }


    private float _hp = 50; // 현재체력. @홍지형
    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
        }
    }


    private float _exp = 50; // 경험치. @홍지형
    public float Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
        }
    }
    
    #endregion

    #region Action
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<Define.EJoystickState> OnJoystickStateChanged;
    #endregion
}
