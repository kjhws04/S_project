using UnityEngine;

namespace TacticsToolkit
{
    public class CameraController : MonoBehaviour
    {
        // The object that the camera should follow
        public Transform target;

        // The size of the camera's orthographic view
        public float size = 10.0f;

        // The smoothing factor for the camera's movement
        public float smoothing = 5.0f;

        // The minimum size of the camera's orthographic view
        public float minSize = 2.0f;

        // The maximum size of the camera's orthographic view
        public float maxSize = 20.0f;

        // The rotation speed of the camera
        public float rotationSpeed = 90.0f;


        // The desired position and rotation of the camera
        private Vector3 desiredPosition;
        private Quaternion desiredRotation;

        void Start()
        {
            // Set the desired position and rotation to the current values
            desiredPosition = transform.position;
            desiredRotation = transform.rotation;
        }


        void Update()
        {
                UpdateRotation();
                UpdateZoom();

                if (target)
                    UpdatePosition();
        }

        private void UpdateRotation()
        {
            // Rotate the camera based on keyboard input
            float rotateX = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; ;
            float rotateY = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime; ;
            desiredRotation *= Quaternion.Euler(-rotateY, rotateX, 0);

            // Limit the camera's rotation to 45 degrees around the target
            float x = desiredRotation.eulerAngles.x;
            if (x > 180) x -= 360;
            x = Mathf.Clamp(x, 0, 30);
            desiredRotation = Quaternion.Euler(x, desiredRotation.eulerAngles.y, 0);
        }

        private void UpdateZoom()
        {
            // Zoom the camera in and out based on scroll wheel input
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            size = Mathf.Clamp(size - zoom, minSize, maxSize);
        }

        private void UpdatePosition()
        {
            // Calculate the desired position based on the target's position and the camera's distance and height
            desiredPosition = target.position - (desiredRotation * Vector3.forward * size);

            // Smoothly move the camera to the desired position and rotation
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * smoothing);

            // Set the camera's orthographic size
            GetComponent<Camera>().orthographicSize = size;
        }

        // Function to focus the camera on a new target
        public void FocusOnTarget(GameObject newTarget)
        {
            target = newTarget.transform;
        }
    }
}

