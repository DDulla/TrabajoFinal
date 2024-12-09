using UnityEngine;

public class Trophy : MonoBehaviour
{
    public string trophyID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectTrophy(this);
            Destroy(gameObject);
        }
    }
}
