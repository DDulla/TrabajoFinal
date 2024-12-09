using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrolAndChase : MonoBehaviour
{
    private Transform player;

    void Update()
    {
        if (player != null && player.gameObject != null)
        {
            // L�gica de patrullaje y persecuci�n aqu�
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime);
        }
        else
        {
            // Buscar al jugador si no est� asignado
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }
}

