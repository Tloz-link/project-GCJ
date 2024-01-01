using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private int id;
    private int hp;

    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;

    public void SetInfo(int id, Transform target)
    {
        agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.radius = 0.2f;
        agent.speed = 1f;

        this.id = id;
        this.hp = 3;
        this.target = target;
    }

    public bool IsAlive()
    {
        return hp > 0;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Managers.Monster.DestoryMonster(id);
        }
    }
}
