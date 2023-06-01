using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code Reference
 * Author: Unity Ace (Youtube)
 * URL: https://www.youtube.com/watch?v=5Rq8A4H6Nzw
 * Title: First Person Camera in Unity
 */

namespace BeanstalkBlitz
{
    public class PlayerCameraBehaviour : MonoBehaviour
    {
        public Transform player;
        private float mouseSensitivity = 4f;
        float verticalAngle = 0f;

        //bool lockedCursor = true;

        void Start()
        {
            // Lock and hide the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (!PauseMenu.GameIsPaused)
            {
                // Collect Mouse Input
                float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                // Rotate the camera around its local X axis
                verticalAngle -= inputY;
                verticalAngle = Mathf.Clamp(verticalAngle, -90, 90);
                transform.localEulerAngles = Vector3.right * verticalAngle;

                // Rotate the player and the camera around its Y axis
                player.Rotate(Vector3.up * inputX);
            }
        }
    }
}