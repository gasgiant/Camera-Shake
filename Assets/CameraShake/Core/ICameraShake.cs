using UnityEngine;

namespace CameraShake
{
    public interface ICameraShake
    {
        /// <summary>
        /// Represents current position and rotation of the camera according to the shake.
        /// </summary>
        Displacement CurrentDisplacement { get; }

        /// <summary>
        /// Shake system will dispose the shake on the first frame when this is true.
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// CameraShaker calls this when the shake is added to the list of active shakes.
        /// </summary>
        void Initialize(Vector3 cameraPosition, Quaternion cameraRotation);

        /// <summary>
        /// CameraShaker calls this every frame on active shakes.
        /// </summary>
        void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation);
    }
}
