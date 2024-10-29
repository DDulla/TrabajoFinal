using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    public float speed = 8f;
    public Transform target; 
    public int life = 3;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

