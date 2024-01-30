using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class Creature : BaseObject
{
    public Data.CreatureData CreatureData { get; private set; }
    public ECreatureType CreatureType { get; protected set; } = ECreatureType.None;

    #region Stats
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public float AtkRange { get; set; }
    public float MoveSpeed { get; set; }
    #endregion

    protected bool _freezeStateOneFrame = false;
    protected ECreatureState _creatureState = ECreatureState.None;
    public virtual ECreatureState CreatureState
    {
        get { return _creatureState; }
        set
        {
            if (_freezeStateOneFrame)
                return;

            if (_creatureState != value)
            {
                _creatureState = value;
                PlayAnimation(value);
            }
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ObjectType = EObjectType.Creature;
        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        DataTemplateID = templateID;

        if (CreatureType == ECreatureType.Hero)
            CreatureData = Managers.Data.HeroDic[templateID];
        else
            CreatureData = Managers.Data.MonsterDic[templateID];

        gameObject.name = $"{CreatureData.DataId}_{CreatureData.DescriptionTextID}";

        AnimatorController animatorController = Managers.Resource.Load<AnimatorController>(CreatureData.AnimatorDataID);
        Animator.runtimeAnimatorController = animatorController;

        MaxHp = CreatureData.MaxHp;
        Hp = CreatureData.MaxHp;
        Atk = CreatureData.Atk;
        AtkRange = CreatureData.AtkRange;
        MoveSpeed = CreatureData.MoveSpeed;

        CreatureState = ECreatureState.Idle;
    }

    private void LateUpdate()
    {
        _freezeStateOneFrame = false;
    }

    protected override void PlayAnimation(Define.ECreatureState state)
    {
        Animator.SetInteger("state", (int)state);
    }

    #region AI
    public float UpdateAITick { get; protected set; } = 0.0f;

    protected IEnumerator CoUpdateAI()
    {
        while (true)
        {
            switch (CreatureState)
            {
                case ECreatureState.Idle:
                    UpdateIdle();
                    break;
                case ECreatureState.Move:
                    UpdateMove();
                    break;
                case ECreatureState.Attack:
                    UpdateAttack();
                    break;
                case ECreatureState.Hit:
                    UpdateHit();
                    break;
                case ECreatureState.Dead:
                    UpdateDead();
                    break;
            }

            if (UpdateAITick > 0)
                yield return new WaitForSeconds(UpdateAITick);
            else
                yield return null;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateHit() { }
    protected virtual void UpdateDead() { }
    #endregion

    #region Battle
    public override void OnDamaged(BaseObject attacker)
    {
        base.OnDamaged(attacker);

        if (attacker.IsValid() == false)
            return;

        Creature creature = attacker as Creature;
        if (creature == null)
            return;

        int finalDamage = creature.Atk;
        Hp = Mathf.Clamp(Hp - finalDamage, 0, MaxHp);
        Debug.Log(gameObject.name + " Current Hp : " + Hp);

        if (Hp <= 0)
        {
            OnDead(attacker);
            CreatureState = ECreatureState.Dead;
        }
        else
        {
            CreatureState = ECreatureState.Hit;
            _freezeStateOneFrame = true;
        }
    }

    public override void OnDead(BaseObject attacker)
    {
        base.OnDead(attacker);
    }
    #endregion

    #region Misc
    protected bool IsValid(BaseObject bo)
    {
        return bo.IsValid();
    }
    #endregion
}
