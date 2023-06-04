using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

        // Menus and UI
        public GameObject UI;
        public GameObject winScreen;
        public LoseScript loseScript;
        public GameObject loseScreen;


        // Spawn
        Vector3 playerSpawn;

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

        // Bean checks
        private BeanBehaviour beanScript;

        // Stomp checks
        private MuncherBehaviour muncherScript;
        private BonkerBehaviour bonkerScript;

        // Bonk checks
        private Vector3 bonkDirection;
        public float bonkForce;
        public float bonkLift;

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
        public TextMeshProUGUI beanCountText;
        private int collectedBeans;

        void Start()
        {
            UI.SetActive(true);
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            playerSpawn = transform.position;
            collectedBeans = 0;
            UpdateBeanCountText();
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
        }

        void FixedUpdate()
        {
            Physics.gravity = gravityValue;
            MovePlayer();
            if (transform.position.y < -5)
            {
                transform.position = playerSpawn;
            }
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
                // Play walking sound
                //
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
            // Play jump sound
            //
            // Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            // Play landing sound
            //
            // Allows the player to jump
            readyToJump = true;
        }

        private void StompEnemy(string enemyType)
        {
            // Play stomp sound
            //
            switch (enemyType)
            {
                case "Bonker":
                    bonkerScript.Stomped();
                    break;
                case "Muncher":
                    muncherScript.Stomped();
                    break;
                default:
                    break;
            }
            // Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, stompForce, rb.velocity.z);
        }

        private void Bonked()
        {
            Debug.Log("Bonk code");
            Vector3 bonkVelocity = bonkDirection * bonkForce;
            bonkVelocity.y = bonkLift;
            Debug.Log(bonkVelocity);
            rb.velocity = bonkVelocity;
        }

        private void CollectBean()
        {
            collectedBeans++;
            UpdateBeanCountText();
        }

        private void UpdateBeanCountText()
        {
            beanCountText.text = "Collected Beans: " + collectedBeans.ToString();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bean")
            {
                beanScript = other.GetComponent<BeanBehaviour>();
                beanScript.GetBean();
                CollectBean();
            }
            if (other.gameObject.tag == "Muncher")
            {
                muncherScript = other.gameObject.GetComponent<MuncherBehaviour>();
                StompEnemy("Muncher");
            }
            if (other.gameObject.tag == "Bonker")
            {
                bonkerScript = other.transform.parent.gameObject.GetComponent<BonkerBehaviour>();
                StompEnemy("Bonker");
            }
            if (other.gameObject.tag == "BonkerHead")
            {
                Debug.Log("BONKED!!!");
                bonkDirection = -other.transform.up;
                bonkDirection = bonkDirection.normalized;
                Bonked();
            }
        }
 
    }

}