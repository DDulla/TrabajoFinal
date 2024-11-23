using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float chaseRange = 5f;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[destPoint].position;
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= chaseRange)
        {
            agent.SetDestination(player.position);
            isChasing = true;
        }
        else if (isChasing)
        {
            isChasing = false;
            GoToNextPoint();
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isChasing)
        {
            GoToNextPoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
