using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingAI : MonoBehaviour
{
    public Area RoamingArea;

    private NavMeshAgent _agent;

    public Transform TargetingSphere;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        PickNewDestination();

        InvokeRepeating("PickNewDestination", 0f, 3.5f);
    }

    public bool AtDestination()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        if (!_agent.enabled)
        {
            return;
        }
    }

    private void PickNewDestination()
    {
        NavMeshHit hit;
        Vector3 pos = transform.position;
        var loc = RoamingArea.PickRandomLocation();
        TargetingSphere.transform.position = loc;
        if (NavMesh.SamplePosition(loc, out hit, 100, 1))
        {
            pos = hit.position;
            _agent.destination = pos;
            return;
        }
        _agent.destination = transform.position;
    }
}
