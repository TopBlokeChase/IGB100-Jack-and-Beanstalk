using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerBehaviour : MonoBehaviour
{
    // Rotate to player
    public GameObject player;
    private Transform playerTransform;
    public float rotationSpeed;

    // Variables
    private Vector3 spawnPoint;
    public float respawnDistance;
    [SerializeField]
    private GameObject parentStem;
    private ParentStemTracker parentStemScript;
    private StemStatTracker stemStatTracker;

    
    
    void Start()
    {
        spawnPoint = transform.position;
        parentStemScript = GetComponent<ParentStemTracker>();
        parentStem = parentStemScript.ParentStem;
        stemStatTracker = parentStemScript.ParentScript();
    }

    void Update()
    {
        FacePlayer();
    }

    void FixedUpdate()
    {
        Vector3 distance = transform.position - spawnPoint;
        if (distance.magnitude > respawnDistance)
        {
            Respawn();
        }
    }

    private void FacePlayer()
    {
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        float stepSize = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
    }

    public void Stomped()
    {
        stemStatTracker.ToggleBonker(false);
        Destroy(this.gameObject);
    }
}
