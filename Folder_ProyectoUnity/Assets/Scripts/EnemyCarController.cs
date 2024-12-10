using UnityEngine;
using UnityEngine.AI;

public class EnemyCarController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance; 
        agent.avoidancePriority = 50; 
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
