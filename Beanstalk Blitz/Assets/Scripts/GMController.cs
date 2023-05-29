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
    public float muncherSpawnTime;
    public GameObject bonkerPrefab;
    public float bonkerSpawnTime;
    public GameObject webslingerPrefab;
    public float webslingerSpawnTime;

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

    // Stem stats
    [SerializeField]
    int stemMaxHealth;

    // Map Control
    public GameObject Map;
    public Texture stemGreen;
    public Texture stemYellow;
    public Texture stemOrange;
    public Texture stemRed;
    [SerializeField]
    StemStatTracker stemScript;
    MapStem mapScript;

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
        EnemySpawner();
    }
    private void EnemySpawner()
    {
        
    }

    private void MapController()
    {
        for (int i = 0; i < beanstalk.Count; i++)
        {
            stemScript = beanstalk[i].GetComponent<StemStatTracker>();
            switch (stemScript.Health)
            {
                case 0:
                    mapScript = Map.transform.GetChild(i + 1).gameObject.GetComponent<MapStem>();
                    mapScript.removeTexture();
                    break;
                case 1:
                    mapScript = Map.transform.GetChild(i + 1).gameObject.GetComponent<MapStem>();
                    mapScript.setTexture(stemRed);
                    break;
                case 2:
                    mapScript = Map.transform.GetChild(i + 1).gameObject.GetComponent<MapStem>();
                    mapScript.setTexture(stemOrange);
                    break;
                case 3:
                    mapScript = Map.transform.GetChild(i + 1).gameObject.GetComponent<MapStem>();
                    mapScript.setTexture(stemYellow);
                    break;
                case 4:
                    mapScript = Map.transform.GetChild(i + 1).gameObject.GetComponent<MapStem>();
                    mapScript.setTexture(stemGreen);
                    break;
            }
        }
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
        stemScript = currentStem.GetComponent<StemStatTracker>();
        stemScript.InitialiseVariables(stemMaxHealth);
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
