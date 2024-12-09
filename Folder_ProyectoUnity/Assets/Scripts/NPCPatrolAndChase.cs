using UnityEngine;

public class NPCPatrolAndChase : MonoBehaviour
{
    private Transform player;

    void Update()
    {
        if (player != null && player.gameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime);
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }
}

