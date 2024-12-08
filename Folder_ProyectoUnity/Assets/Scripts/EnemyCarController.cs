using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyCarController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private bool isAvoidingCollision = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null && !isAvoidingCollision)
        {
            agent.SetDestination(target.position);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider hitCollider = hitColliders[i];
                if (hitCollider.gameObject.CompareTag("Enemy") && hitCollider.gameObject != gameObject)
                {
                    StartCoroutine(AvoidCollision(hitCollider));
                    break;
                }
            }
        }
    }

    private IEnumerator AvoidCollision(Collider collider)
    {
        isAvoidingCollision = true;

        Vector3 avoidDirection = (transform.position - collider.transform.position).normalized;
        agent.isStopped = true;

        float duration = 1f;
        float angle = 15f;

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 rightRotation = currentRotation + new Vector3(0, angle, 0);
        Vector3 leftRotation = currentRotation - new Vector3(0, angle * 2, 0);

        transform.DORotate(rightRotation, duration / 2).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DORotate(leftRotation, duration / 2).SetEase(Ease.OutQuad);
        });

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Translate(avoidDirection * agent.speed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false;
        isAvoidingCollision = false;
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
