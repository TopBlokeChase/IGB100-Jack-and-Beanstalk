using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeanstalkBlitz; // Add this line

/*
 * Code Reference
 * Author: Dave / GameDevelopment (Youtube)
 * URL: https://www.youtube.com/watch?v=f473C43s8nE
 * Title: FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial
 */

namespace BeanstalkBlitz
{
    public class PlayerMovement : MonoBehaviour
    {
        // Movement
        public float moveSpeed;

        public float groundDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump = true;

        // Ground check
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        // Enemy check
        public float stompForce;
        public LayerMask isEnemy;
        bool enemyBelow;

        // Keybinds
        public KeyCode jumpKey = KeyCode.Space;

        public Transform player;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;
        Rigidbody rb;

<<<<<<< Updated upstream
=======
        //Grapple gun 
        public bool activeGrapple;
        //grapple gun swinging
        public float swingSpeed;
        public bool swinging;

>>>>>>> Stashed changes
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            // Change gravity on player start. Default = (0, -9.81, 0);
            Physics.gravity = new Vector3(0, -18f, 0);
        }

        void Update()
        {
            // Ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            // Enemy check
            enemyBelow = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isEnemy);

            MyInput();
            SpeedControl();

            // Handle drag
            if (grounded)
            {
                rb.drag = groundDrag;
            } else
            {
                rb.drag = 0;
            }

            // Handle enemy stomp
            if (enemyBelow)
            {
                stompEnemy();
            }
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer() 
        {
<<<<<<< Updated upstream
=======
            if (activeGrapple) return;
            if (swinging) return;

>>>>>>> Stashed changes
            // calculate movement direction
            moveDirection = player.forward * verticalInput + player.right * horizontalInput;


            // On ground
            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }

            // In air
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        private void SpeedControl()
        {
<<<<<<< Updated upstream
=======
            //while grapple
            if (activeGrapple) return;
            if (swinging) return;

>>>>>>> Stashed changes
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //while swinging
            if (swinging)
            {
     
                moveSpeed = swingSpeed;
            }

            // Limit velocity
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 maxVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
            }
        }

        private void Jump()
        {
            // Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }

        private void stompEnemy()
        {
            // Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, stompForce, rb.velocity.z);
        }
    }
}