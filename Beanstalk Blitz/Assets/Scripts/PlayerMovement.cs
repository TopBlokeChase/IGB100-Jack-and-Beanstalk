using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeanstalkBlitz
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed;
        private float maxSpeed;
        private float jumpHeight;
        private float jumpSpeed;
        private bool invulnerable;

        private Rigidbody playerRb;
        public Transform playerCamera;

        // Start is called before the first frame update
        void Start()
        {
            invulnerable = false;
            playerRb = GetComponent<Rigidbody>();

            speed = 20f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKey("w")) {
                playerRb.AddForce(playerCamera.forward * speed);
            }
        }

    }
}