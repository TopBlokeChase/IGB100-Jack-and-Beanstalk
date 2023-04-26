using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeanstalkBlitz
{
    public class PlayerCameraBehaviour : MonoBehaviour
    {
        public GameObject player;
        private Vector3 cameraOffset;
        public float mouseSensitivity;

        // Update is called once per frame
        void FixedUpdate()
        {
            followPlayer();
            lookWithMouse();
        }

        private void followPlayer()
        {
            transform.position = player.transform.position + -1*this.transform.forward + 1.5f*this.transform.up;
        }

        private void lookWithMouse()
        {
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");
            transform.RotateAround(player.transform.position, -Vector3.up, -rotateHorizontal * mouseSensitivity); // left-right
            transform.RotateAround(Vector3.zero, transform.right, -rotateVertical * mouseSensitivity); // up-down
        }
    }
}