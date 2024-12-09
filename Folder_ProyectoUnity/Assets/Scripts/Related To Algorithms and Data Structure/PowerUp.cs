using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Score,
        Speed,
        Life
    }

    public PowerUpType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ApplyPowerUp(type);
            Destroy(gameObject);
        }
    }
}
