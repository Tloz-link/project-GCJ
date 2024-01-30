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
        CreatureState = ECreatureState.Idle;
        Collider.excludeLayers = ~(1 << (int)Define.ELayer.Hero);
        Collider.includeLayers = (1 << (int)Define.ELayer.Hero);

        Speed = 3.0f;
        Hp = 3;
        Attack = 2;

        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.radius = 0.2f;
        _agent.speed = 1f;

        StartCoroutine(CoUpdateAI());

        return true;
    }

    public void Damaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Managers.Object.Despawn<Monster>(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseObject target = other.GetComponent<BaseObject>();

        // TODO
        target.OnDamaged(this);
    }

    #region Battle
    public override void OnDamaged(BaseObject attacker)
    {

    }

    public override void OnDead(BaseObject attacker)
    {
        base.OnDead(attacker);
    }
    #endregion

    #region AI
    private NavMeshAgent _agent;
    private Transform _target;

    protected override void UpdateIdle()
    {
        _target = Managers.Object.Hero?.transform;

        if (_target != null)
            _agent.SetDestination(_target.position);
    }
    #endregion
}
