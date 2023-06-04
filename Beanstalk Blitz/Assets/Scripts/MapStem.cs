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

    [SerializeField]
    Texture playerIcon;
    private RawImage playerImage;
    public GameObject player;
    private bool playerNear;

    // Visability
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

        // Player
        playerNear = false;
        playerImage = transform.GetChild(2).GetComponent<RawImage>();

        // Apply default state
        stemImage.texture = noneTexture;
        stemImage.color = noColor;
        muncherImage.texture = noneTexture;
        muncherImage.color = noColor;
        beanImage.texture = noneTexture;
        beanImage.color = noColor;
        Debug.Log(transform.position.y);
    }

    void FixedUpdate()
    {
        float relativeY = transform.position.y + 450;
        relativeY = relativeY * 2 / 5;
        if (Mathf.Abs(player.transform.position.y - relativeY) <= 10 && playerNear == false)
        {
            playerNear = true;
            DisplayPlayer();
        }
        else if (Mathf.Abs(player.transform.position.y - transform.position.y) > 10)
        {
            playerNear = false;
            RemovePlayer();
        }
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
            beanImage.texture = beanAlert;
            beanImage.color = fullColor;
        }
        else
        {
            beanImage.texture = noneTexture;
            beanImage.color = noColor;
        }
    }

    public void DisplayPlayer()
    {
        playerImage.texture = playerIcon;
        playerImage.color = fullColor;
        playerNear = true;
    }

    public void RemovePlayer()
    {
        playerImage.texture = noneTexture;
        playerImage.color = noColor;
        playerNear = false;
    }
}