using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapStem : MonoBehaviour
{
    [SerializeField]
    RawImage rawImage;
    [SerializeField]
    Texture noneTexture;
    private Color noColor;
    private Color fullColor;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate variables
        rawImage = GetComponent<RawImage>();
        noColor = Color.white;
        noColor.a = 0;
        fullColor = Color.white;
        fullColor.a = 1;


        // Apply default state
        rawImage.texture = noneTexture;
        rawImage.color = noColor;
    }

    public void SetTexture(Texture newTexture)
    {
        rawImage.texture = newTexture;
        rawImage.color = fullColor;
    }

    public void RemoveTexture()
    {
        rawImage.texture = noneTexture;
        rawImage.color = noColor;
    }

    public void DisplayMuncher(bool state)
    {

    }

    public void DisplayBean(bool state)
    {

    }
}
