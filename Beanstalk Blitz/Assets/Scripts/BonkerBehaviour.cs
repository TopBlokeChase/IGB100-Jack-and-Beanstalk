using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerBehaviour : MonoBehaviour
{
    // Spawning
    private Vector3 spawnPoint;
    public float respawnDistance;
    [SerializeField]

    // Parent stem
    private GameObject parentStem;
    private ParentStemTracker parentStemScript;
    private StemStatTracker stemStatTracker;

    // Rotate to player
    private GameObject player;
    public float rotationSpeed;

    // Charge player
    public float chargeSpeed;
    public float chargeDuration;
    public float angleToCharge;
    public float distanceToCharge;
    private bool charging;
    private float chargeStartTime;


    void Start()
    {
        charging = false;
        player = GameObject.FindWithTag("Player");
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
        if (charging)
        {
            ChargePlayer();
        }
    }

    private void ChargePlayer()
    {
        transform.position += transform.forward * chargeSpeed/10;
        if (Time.time - chargeStartTime > chargeDuration)
        {
            Respawn();
        }
    }

    private void FacePlayer()
    {
        Vector3 dirToPlayer = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        float stepSize = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (!charging && Vector3.Distance(transform.position, player.transform.position) <= distanceToCharge && Vector3.Angle(newDirection, dirToPlayer) <= angleToCharge)
        {
            chargeStartTime = Time.time;
            charging = true;
        }
    }

    private void Respawn()
    {
        charging = false;
        transform.position = spawnPoint;
    }

    public void Stomped()
    {
        stemStatTracker.ToggleBonker(false);
        Destroy(this.gameObject);
    }
}
