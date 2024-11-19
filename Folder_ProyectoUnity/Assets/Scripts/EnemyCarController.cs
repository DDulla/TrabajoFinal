using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCarController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 500f;
    public float detectionRadius = 1.5f;
    public Transform target;
    private bool isAvoidingCollision = false;

    private void Update()
    {
        if (target != null && !isAvoidingCollision)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            Vector3 targetDirection = target.position - transform.position;
            float singleStep = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            if (Vector3.Angle(transform.forward, targetDirection) > 10f)
            {
                transform.Translate(Vector3.right * 0.5f * Time.deltaTime, Space.World);
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider hitCollider = hitColliders[i];
                if (hitCollider.gameObject.CompareTag("Enemy") && hitCollider.gameObject != gameObject)
                {
                    StartCoroutine(AvoidCollision(hitCollider));
                    break;
                }
            }

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private IEnumerator AvoidCollision(Collider collider)
    {
        isAvoidingCollision = true;

        Vector3 avoidDirection = (transform.position - collider.transform.position).normalized;

        float duration = 0.5f;
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
            transform.Translate(avoidDirection * speed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAvoidingCollision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
