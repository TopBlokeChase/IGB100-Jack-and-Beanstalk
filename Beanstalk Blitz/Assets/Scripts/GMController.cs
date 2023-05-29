using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GMController : MonoBehaviour
{
    // Current Stems - Stores the current stems in the beanstalk
    public List<GameObject> beanstalk = new List<GameObject>();

    // Enemy Spawning
    public GameObject muncherPrefab;
    //public GameObject bonkerPrefab;
    //public GameObject webslingerPrefab;

    // Stem Spawning
    public int startingSize;
    public int maxSizeStalk;
    float spawnTime;
    public float spawnInterval = 10f;
    public List<GameObject> stemPrefabList;
    private GameObject currentStem = null;
    private GameObject stemPrefab;
    public GameObject spawnAnchor;
    private Vector3 spawnOffset;
    private Vector3 spawnPoint;

    // Map Control
    public GameObject Map;

    

    // Start is called before the first frame update
    void Start()
    {
        // Load prefabs into list
        var stemPrefabArray = Resources.LoadAll("Assets/Prefabs/Stems", typeof(GameObject)).Cast<GameObject>();
        foreach (var pf in stemPrefabArray)
        {
            Debug.Log(pf);
            stemPrefabList.Add(pf);
        }
        Debug.Log("List Count: " + stemPrefabList.Count);

        // Spawn first stem
        GameStart();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beanstalk.Count < maxSizeStalk)
        {
            if (Time.time - spawnTime > spawnInterval)
            {
                SpawnStem();
            }
        }
        
        MapController();
    }

    private void MapController()
    {

    }

    private void GameStart()
    {
        stemPrefab = stemPrefabList[Random.Range(0, stemPrefabList.Count)];
        spawnOffset = stemPrefab.transform.position - stemPrefab.transform.GetChild(0).transform.position;
        spawnPoint = spawnAnchor.transform.position + spawnOffset;
        SpawnStem();
        while (startingSize > 1)
        {
            SpawnStem();
            startingSize--;
        }
    }

    private void SpawnStem()
    {
        currentStem = Instantiate(stemPrefab, spawnPoint, transform.rotation);
        spawnTime = Time.time;
        stemPrefab = LoadNextStem();
        beanstalk.Add(currentStem);
    }

    private GameObject LoadNextStem()
    {
        spawnAnchor = currentStem.transform.GetChild(1).gameObject;
        GameObject nextStem = stemPrefabList[Random.Range(0, stemPrefabList.Count)];
        spawnOffset = nextStem.transform.position - nextStem.transform.GetChild(0).transform.position;
        spawnPoint = spawnAnchor.transform.position + spawnOffset;
        return nextStem;
    }
}
