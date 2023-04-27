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

            speed = 200f;
            jumpSpeed = 5500f;
        }
        void Update()
        {
            jumpControls();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            moveControls();
        }

        private void jumpControls()
        {
            if (Input.GetKeyDown("space"))
            {
                playerRb.AddForce(new Vector3 (0, jumpSpeed, 0));
            }
        }

        private void moveControls()
        {
            if (Input.GetKey("w"))
            {
                playerRb.AddForce(playerCamera.forward * speed);
            }
            if (Input.GetKey("a"))
            {
                playerRb.AddForce(-playerCamera.right * speed);
            }
            if (Input.GetKey("s"))
            {
                playerRb.AddForce(-playerCamera.forward * speed);
            }
            if (Input.GetKey("d"))
            {
                playerRb.AddForce(playerCamera.right * speed);
            }
        }
    }
}