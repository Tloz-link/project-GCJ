using Data;
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

    #region Stat
    public int Level { get; set; }
    public int MaxExp { get; set; }
    public float ItemAcquireRange { get; set; }
    public float ResistDisorder { get; set; }

    private int _exp = 0;
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;
            if (_exp >= MaxExp)
                LevelUp();
            Managers.Game.RefreshUI();
        }
    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Hero;

        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;
        Managers.Game.OnJoystickStateChanged += HandleOnJoystickStateChanged;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureState = ECreatureState.Idle;
        Renderer.sortingOrder = SortingLayers.HERO;

        Data.HeroData hereData = CreatureData as Data.HeroData;

        Level = hereData.Level;
        MaxExp = hereData.MaxExp;
        Exp = 0;
        ItemAcquireRange = hereData.ItemAcquireRange;
        ResistDisorder = hereData.ResistDisorder;

        foreach (int skillID in hereData.SkillIdList)
            AddSkill(skillID);

        Managers.Game.RefreshUI();
    }

    void Update()
    {
        if (IsValid(this) == false)
            return;

        SetRigidbodyVelocity(_moveDir * MoveSpeed);
    }

    public void AddSkill(int skillTemplateID = 0)
    {
        string className = Managers.Data.SkillDic[skillTemplateID].ClassName;

        SkillBase skill = gameObject.AddComponent(Type.GetType(className)) as SkillBase;
        if (skill == null)
            return;

        skill.SetInfo(this, skillTemplateID);

        _skills.Add(skill);
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

        Managers.Game.RefreshUI();
    }

    public override void OnDead(BaseObject attacker, SkillBase skill)
    {
        base.OnDead(attacker, skill);

        SetRigidbodyVelocity(Vector2.zero);
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= HandleOnJoystickStateChanged;

        foreach (SkillBase s in _skills)
        {
            Destroy(s);
        }
    }

    private void LevelUp()
    {
        if (Level >= Define.MAX_LEVEL)
            return;

        Level += 1;
        Data.HeroLevelData heroLevelData = Managers.Data.HeroLevelDic[DataID + Level];

        Exp = 0;
        MaxHp = heroLevelData.MaxHp;
        Hp = heroLevelData.MaxHp;
        MaxExp = heroLevelData.Exp;
        MoveSpeed = (heroLevelData.MoveSpeed / 100.0f) * Define.DEFAULT_SPEED;
        ItemAcquireRange = heroLevelData.ItemAcquireRange;
        ResistDisorder = heroLevelData.ResistDisorder;
    }
    #endregion
}
