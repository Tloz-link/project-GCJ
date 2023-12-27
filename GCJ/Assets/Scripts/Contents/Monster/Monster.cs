using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.radius = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
