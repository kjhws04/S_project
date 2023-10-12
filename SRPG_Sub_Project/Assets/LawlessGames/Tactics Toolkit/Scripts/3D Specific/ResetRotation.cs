using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticsToolkit
{
    public class ResetRotation : MonoBehaviour
    {
        void LateUpdate()
        {
            // Get the camera's rotation
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // If the object has a parent, remove the parent's rotation from the camera's rotation
            if (transform.parent != null)
            {
                cameraRotation = Quaternion.Inverse(transform.parent.rotation) * cameraRotation;
            }

            // Set the object's rotation to match the corrected camera rotation
            transform.rotation = cameraRotation;
        }
    }
}
