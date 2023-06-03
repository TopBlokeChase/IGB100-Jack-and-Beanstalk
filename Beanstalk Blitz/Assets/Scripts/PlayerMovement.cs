using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Change gravity on player start. Default = (0, -9.81, 0);
        public Vector3 gravityValue = new Vector3(0, -18f, 0);

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

        //Grapple gun 
        public bool activeGrapple;
        public float swingSpeed;
        public bool swinging;
        public GameObject winScreen;


        void Start()
        {
            winScreen = GameObject.Find("Win");
            winScreen = GameObject.FindGameObjectWithTag("WinScreen");
            winScreen.SetActive(false);
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
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
            }
            else
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
            Physics.gravity = gravityValue;
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
            if (activeGrapple) return;
            if (swinging) return;

            // calculate movement direction
            moveDirection = player.forward * verticalInput + player.right * horizontalInput;


            // On ground
            if (grounded && !activeGrapple )
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
            //while grapple
            if (activeGrapple) return;
            if (swinging) return;

            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

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


        //grapple hook
        private bool enableMovementOnNextTouch;

        public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
        {
            activeGrapple = true;
            velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
            Invoke(nameof(Setvelocity), 0.1f);
        }

        private Vector3 velocityToSet;
        private void Setvelocity()
        {
            enableMovementOnNextTouch = true;
            rb.velocity = velocityToSet;
        }

        public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
        {
            float gravity = Physics.gravity.y;
            float displacementY = endPoint.y - startPoint.y;
            Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
                + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

            return velocityXZ + velocityY;
        }

        public void ResetRestrictions()
        {
            activeGrapple = false;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (enableMovementOnNextTouch)
            {
                enableMovementOnNextTouch = false;
                ResetRestrictions();

                GetComponent<Grappling>().StopGrapple();
            }
        }
    }

}