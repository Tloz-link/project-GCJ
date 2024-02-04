using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class Monster : Creature
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Monster;

        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.radius = 0.35f;

        StartCoroutine(CoUpdateAI());

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureState = ECreatureState.Move;

        Renderer.sortingOrder = SortingLayers.MONSTER;
        _agent.speed = MoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseObject target = other.GetComponent<BaseObject>();
        if (target.IsValid() == false)
            return;

        Creature creature = target as Creature;
        if (creature == null || creature.CreatureType != Define.ECreatureType.Hero)
            return;

        // TODO
        target.OnDamaged(this, null);
    }

    #region Battle
    public override void OnDamaged(BaseObject attacker, SkillBase skill)
    {
        base.OnDamaged(attacker, skill);
    }

    public override void OnDead(BaseObject attacker, SkillBase skill)
    {
        base.OnDead(attacker, skill);

        // Drop Item

        Managers.Object.Despawn(this);
    }
    #endregion

    #region AI
    private NavMeshAgent _agent;
    private Hero _hero;

    protected override void UpdateMove()
    {
        _hero = Managers.Object.Hero;

        if (_hero.IsValid())
        {
            _agent.SetDestination(_hero.transform.position);

            Vector2 dir = _agent.desiredVelocity;
            if (dir.x < 0)
                LookLeft = true;
            else if (dir.x > 0)
                LookLeft = false;
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    protected override void UpdateHit()
    {
        CreatureState = ECreatureState.Move;
    }
    #endregion
}
