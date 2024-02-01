using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Define;

public class Hero : Creature
{
    private List<SkillBase> _skills = new List<SkillBase>();
    private Vector2 _moveDir = Vector2.zero;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Hero;

        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;
        Managers.Game.OnJoystickStateChanged += HandleOnJoystickStateChanged;

        {
            KunaiSkill skill = gameObject.GetOrAddComponent<KunaiSkill>();
            skill.SetInfo(this, Define.SKILL_KUNAI_ID);
            _skills.Add(skill);
        }

        {
            RocketSkill skill = gameObject.GetOrAddComponent<RocketSkill>();
            skill.SetInfo(this, Define.SKILL_ROCKET_ID);
            _skills.Add(skill);
        }

        {
            SatelliteSkill skill = gameObject.GetOrAddComponent<SatelliteSkill>();
            skill.SetInfo(this, Define.SKILL_SATELLITE_ID);
            _skills.Add(skill);
        }

        {
            KatanaSkill skill = gameObject.GetOrAddComponent<KatanaSkill>();
            skill.SetInfo(this, Define.SKILL_KATANA_ID);
            _skills.Add(skill);
        }

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureState = ECreatureState.Idle;
        Renderer.sortingOrder = SortingLayers.HERO;
    }

    void Update()
    {
        if (IsValid(this) == false)
            return;

        transform.TranslateEx(_moveDir * Time.deltaTime * MoveSpeed);
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
    public override void OnDamaged(BaseObject attacker, SkillBase skill)
    {
        base.OnDamaged(attacker, skill);
    }

    public override void OnDead(BaseObject attacker, SkillBase skill)
    {
        base.OnDead(attacker, skill);

        _moveDir = Vector2.zero;
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;
    }
    #endregion
}
