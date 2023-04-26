using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeanstalkBlitz
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed;
        private float jumpHeight;
        private float jumpSpeed;
        private bool invulnerable;
        public Transform playerCamera;

        // Start is called before the first frame update
        void Start()
        {
            invulnerable = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown("w"))
            {
                
            }
        }
    }
}