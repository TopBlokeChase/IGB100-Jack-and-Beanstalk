using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerBehaviour : MonoBehaviour
{
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
        stemStatTracker.ToggleBonker(false);
        Destroy(this);
    }
}
