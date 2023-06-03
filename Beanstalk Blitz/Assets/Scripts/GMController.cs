using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GMController : MonoBehaviour
{
    // Current Stems - Stores the current stems in the beanstalk
    List<GameObject> beanstalk = new List<GameObject>();

    // Enemy Spawning
    public GameObject muncherPrefab;
    private float muncherSpawnTime;
    public float muncherInterval;

    public GameObject bonkerPrefab;
    private float bonkerSpawnTime;
    public float bonkerInterval;

    // Webslinger not implemented in current version
    /*public GameObject webslingerPrefab;
    private float webslingerSpawnTime;
    public float webslingerinterval;*/

    // Bean Spawning
    public GameObject beanPrefab;
    private float beanSpawnTime;
    public float beanInterval;

    // Tracking which stem an enemy or bean has spawned on
    ParentStemTracker parentStemTrackerScript;

    // Stem Spawning
    Vector3 enemySpawnPoint;
    public int startingSize;
    public int maxSizeStalk;
    float spawnTime;
    public float spawnInterval = 10f;

    public List<GameObject> stemPrefabList = new List<GameObject>();
    private GameObject currentStem = null;
    private GameObject stemPrefab;
    public GameObject spawnAnchor;

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

    Vector3 spawnOffset;
    Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Load prefabs into list
        var stemPrefabArray = Resources.LoadAll("Assets/Prefabs/Stems", typeof(GameObject)).Cast<GameObject>();
        foreach (var pf in stemPrefabArray)
        {
            stemPrefabList.Add(pf);
        }

        // Spawn first stem segments
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
        SpawnController();
    }

    private GameObject RandomStem()
    {
        if (beanstalk.Count == 0)
        {
            return null;
        }
        return beanstalk[Random.Range(0, beanstalk.Count)];
    }

    private void SpawnController()
    {
        if (Time.time - muncherSpawnTime > muncherInterval)
        {
            SpawnMuncher();
        }
        if (Time.time - bonkerSpawnTime > bonkerInterval)
        {
            SpawnBonker();
        }
        if (Time.time - beanSpawnTime > beanInterval)
        {
            SpawnBean();
        }
    }

    private void SpawnMuncher()
    {
        GameObject stemSegment;
        stemSegment = RandomStem();
        stemScript = stemSegment.GetComponent<StemStatTracker>();
        if (!stemScript.HasMuncher)
        {
            stemScript.ToggleMuncher(true);
            int randomIndex = Random.Range(2, 5);
            Quaternion spawnRotation = stemSegment.transform.GetChild(randomIndex).rotation;
            enemySpawnPoint = stemSegment.transform.GetChild(randomIndex).position;
            GameObject muncher = Instantiate(muncherPrefab, enemySpawnPoint, spawnRotation);
            parentStemTrackerScript = muncher.GetComponent<ParentStemTracker>();
            parentStemTrackerScript.ParentStem = stemSegment;
        }
        muncherSpawnTime = Time.time;
    }
    private void SpawnBonker()
    {
        GameObject stemSegment;
        stemSegment = RandomStem();
        stemScript = stemSegment.GetComponent<StemStatTracker>();
        if (!stemScript.HasBonker)
        {
            stemScript.ToggleBonker(true);
            int randomIndex = Random.Range(2, 5);
            Quaternion spawnRotation = stemSegment.transform.GetChild(randomIndex).transform.GetChild(0).transform.GetChild(0).rotation;
            enemySpawnPoint = stemSegment.transform.GetChild(randomIndex).transform.GetChild(0).transform.GetChild(0).position;
            GameObject bonker = Instantiate(bonkerPrefab, enemySpawnPoint, spawnRotation);
            parentStemTrackerScript = bonker.GetComponent<ParentStemTracker>();
            parentStemTrackerScript.ParentStem = stemSegment;
        }
        bonkerSpawnTime = Time.time;
    }
    // Webslinger not implemented in current version
    /*private void SpawnWebslinger()
    {

    }*/
    private void SpawnBean()
    {
        GameObject stemSegment;
        stemSegment = RandomStem();
        stemScript = stemSegment.GetComponent<StemStatTracker>();
        if (!stemScript.HasBean)
        {
            stemScript.ToggleBean(true);
            int randomIndex = Random.Range(2, 5);
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0,360), 0);
            enemySpawnPoint = stemSegment.transform.GetChild(randomIndex).transform.GetChild(0).transform.GetChild(1).position;
            GameObject bean = Instantiate(beanPrefab, enemySpawnPoint, spawnRotation);
            parentStemTrackerScript = bean.GetComponent<ParentStemTracker>();
            parentStemTrackerScript.ParentStem = stemSegment;
        }
        beanSpawnTime = Time.time;
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
        beanstalk.Add(currentStem);
        stemScript = currentStem.GetComponent<StemStatTracker>();
        stemScript.InitialiseVariables(stemMaxHealth);
        spawnTime = Time.time;
        stemPrefab = loadNextStem();
    }

    private GameObject loadNextStem()
    {
        spawnAnchor = currentStem.transform.GetChild(1).gameObject;
        GameObject nextStem = stemPrefabList[Random.Range(0, stemPrefabList.Count)];
        spawnOffset = nextStem.transform.position - nextStem.transform.GetChild(0).transform.position;
        spawnPoint = spawnAnchor.transform.position + spawnOffset;
        return nextStem;
    }
}
