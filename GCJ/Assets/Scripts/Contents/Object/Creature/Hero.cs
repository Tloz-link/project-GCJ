using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Define;

public class Hero : Creature
{
    public Vector2 Direction { get; private set; } = Vector2.up; 
    enum GameObjects
    {
        Sprite,
        Satellite
    }

    private GameObject[] _childs;
    private List<Skill> _skills = new List<Skill>();
    private Vector2 _moveDir = Vector2.zero;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));

        CreatureType = ECreatureType.Hero;

        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;
        Managers.Game.OnJoystickStateChanged += HandleOnJoystickStateChanged;

        {
            KunaiSkill kunai = new KunaiSkill();
            _skills.Add(kunai);
        }

        //{
        //    RocketSkill rocket = new RocketSkill();
        //    _skills.Add(rocket);
        //}

        //{
        //    SatelliteSkill satellite = new SatelliteSkill(_childs[(int)GameObjects.Satellite].transform, 3, 3.0f);
        //    _skills.Add(satellite);
        //}

        //{
        //    KatanaSkill katana = new KatanaSkill(transform);
        //    _skills.Add(katana);
        //}

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureState = ECreatureState.Idle;

        Renderer = _childs[(int)GameObjects.Sprite].GetComponent<SpriteRenderer>();
        Renderer.sortingOrder = SortingLayers.HERO;
    }

    public void BindObject(Type type)
    {
        string[] names = Enum.GetNames(type);
        _childs = new GameObject[names.Length];

        for (int i = 0; i < names.Length; i++)
        {
            _childs[i] = Util.FindChild(gameObject, names[i], true);
            if (_childs[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    protected override void Flip(bool flag)
    {
        Transform sprite = _childs[(int)GameObjects.Sprite].transform;

        Vector3 scale = sprite.localScale;
        scale.x = flag ? -1 : 1;
        sprite.localScale = scale;
    }

    void Update()
    {
        if (IsValid(this) == false)
            return;

        transform.TranslateEx(_moveDir * Time.deltaTime * MoveSpeed);

        foreach (Skill skill in _skills)
        {
            skill.Update();
        }
    }

    private void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;

        if (dir != Vector2.zero)
            Direction = dir;
    }

    private void HandleOnJoystickStateChanged(EJoystickState joystickState)
    {
        switch (joystickState)
        {
            case Define.EJoystickState.PointerDown:
                break;
            case Define.EJoystickState.Drag:
                CreatureState = Define.ECreatureState.Move;
                break;
            case Define.EJoystickState.PointerUp:
                CreatureState = Define.ECreatureState.Idle;
                break;
            default:
                break;
        }
    }

    #region Battle
    public override void OnDamaged(BaseObject attacker)
    {
        base.OnDamaged(attacker);
    }

    public override void OnDead(BaseObject attacker)
    {
        base.OnDead(attacker);

        _moveDir = Vector2.zero;
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;
    }
    #endregion
}
