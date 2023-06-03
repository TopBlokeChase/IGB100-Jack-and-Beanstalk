using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentStemTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject parentStem;

    public GameObject ParentStem { get { return parentStem; } set { parentStem = value; } }
}
