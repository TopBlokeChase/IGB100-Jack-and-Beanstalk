using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuncherBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject parentStem;
    private ParentStemTracker parentStemScript;
    private StemStatTracker stemStatTracker;

    void Start()
    {
        parentStemScript = GetComponent<ParentStemTracker>();
        parentStem = parentStemScript.ParentStem;
        stemStatTracker = parentStemScript.ParentScript();
    }

    public void Stomped()
    {
        stemStatTracker.ToggleMuncher(false);
        Destroy(this.gameObject);
    }
}
