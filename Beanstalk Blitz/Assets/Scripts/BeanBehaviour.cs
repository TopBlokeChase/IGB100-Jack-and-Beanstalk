using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBehaviour : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject bean;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
