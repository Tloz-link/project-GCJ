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
        Speed = 3.0f;
        HP = 3;

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
        HP -= damage;
        if (HP <= 0)
        {
            Managers.Object.Despawn<Monster>(this);
        }
    }

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
