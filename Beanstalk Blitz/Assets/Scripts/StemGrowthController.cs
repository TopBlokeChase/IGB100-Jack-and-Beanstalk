using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemGrowthController : MonoBehaviour
{
    // Current Stems - Stores the current stems in the beanstalk
    GameObject[] beanstalk;

    float spawnTime;

    public GameObject stemPrefab;
    public GameObject anchor;
    Vector3 spawnPoint;
    Vector3 anchorOffset;

    GameObject stem = null;

    // Start is called before the first frame update
    void Start()
    {
        anchorOffset = stemPrefab.transform.position - stemPrefab.transform.GetChild(0).transform.position;
        spawnPoint = anchor.transform.position + anchorOffset;
        SpawnStem();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > 10f)
        {
            LoadNextStem();
            SpawnStem();
            spawnTime = Time.time;
        }
    }

    void SpawnStem()
    {
        stem = Instantiate(stemPrefab, spawnPoint, transform.rotation);
    }

    void LoadNextStem()
    {
        stemPrefab = stemPrefab; // Change this to select a random stem from "stems" prefab folder
        anchor = stem.transform.GetChild(1).gameObject;
        anchorOffset = stemPrefab.transform.position - stemPrefab.transform.GetChild(0).transform.position;
        spawnPoint = anchor.transform.position + anchorOffset;
    }
}
