using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerBehaviour : MonoBehaviour
{
    private Vector3 spawnPoint;
    public float respawnDistance;
    
    void Start()
    {
        spawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 distance = transform.position - spawnPoint;
        if (distance.magnitude > respawnDistance)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
    }

    public void Stomped()
    {

    }
}
