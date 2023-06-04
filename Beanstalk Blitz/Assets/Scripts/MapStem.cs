using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapStem : MonoBehaviour
{
    [SerializeField]
    RawImage stemImage;
    [SerializeField]
    Texture noneTexture;
    [SerializeField]
    Texture muncherAlert;
    private RawImage muncherImage;
    [SerializeField]
    Texture beanAlert;
    private RawImage beanImage;
    private Color noColor;
    private Color fullColor;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate variables
        noColor = Color.white;
        noColor.a = 0;
        fullColor = Color.white;
        fullColor.a = 1;

        // Stem
        stemImage = GetComponent<RawImage>();

        // Alerts
        muncherImage = transform.GetChild(0).GetComponent<RawImage>();
        beanImage = transform.GetChild(1).GetComponent<RawImage>();


        // Apply default state
        stemImage.texture = noneTexture;
        stemImage.color = noColor;
        muncherImage.texture = noneTexture;
        muncherImage.color = noColor;
        beanImage.texture = noneTexture;
        beanImage.color = noColor;
    }

    public void SetTexture(Texture newTexture)
    {
        stemImage.texture = newTexture;
        stemImage.color = fullColor;
    }

    public void RemoveTexture()
    {
        stemImage.texture = noneTexture;
        stemImage.color = noColor;
    }

    public void DisplayMuncher(bool state)
    {
        if (state)
        {
            muncherImage.texture = muncherAlert;
            muncherImage.color = fullColor;
        }
        else
        {
            muncherImage.texture = noneTexture;
            muncherImage.color = noColor;
        }
    }

    public void DisplayBean(bool state)
    {
        if (state)
        {
            muncherImage.texture = beanAlert;
            muncherImage.color = fullColor;
        }
        else
        {
            beanImage.texture = noneTexture;
            beanImage.color = noColor;
        }
    }
}
