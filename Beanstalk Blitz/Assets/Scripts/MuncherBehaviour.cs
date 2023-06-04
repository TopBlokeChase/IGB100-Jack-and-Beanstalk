using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuncherBehaviour : MonoBehaviour
{
    // Parent stem
    [SerializeField]
    private GameObject parentStem;
    private ParentStemTracker parentStemScript;
    private StemStatTracker stemStatTracker;

    // Variables
    public float timeToDealDmg;
    public float munchTime;

    void Start()
    {
        parentStemScript = GetComponent<ParentStemTracker>();
        parentStem = parentStemScript.ParentStem;
        stemStatTracker = parentStemScript.ParentScript();
        munchTime = Time.time;
    }

    void FixedUpdate()
    {
        // Play munch sound

        // Deal damage if too much time has elapsed
        if (Time.time - munchTime > timeTODealDmg)
        {
            munchTime = Time.time;
            stemStatTracker.ChangeHealth(-1);
        }
    }

    public void Stomped()
    {
        stemStatTracker.ToggleMuncher(false);
        Destroy(this.gameObject);
    }
}