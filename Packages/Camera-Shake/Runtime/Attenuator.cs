using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Contains methods for changing strength and direction of shakes depending on their position.
    /// </summary>
    public static class Attenuator
    {
        /// <summary>
        /// Returns multiplier for the strength of a shake, based on source and camera positions.
        /// </summary>
        public static float Strength(StrengthAttenuationParams pars, Vector3 sourcePosition, Vector3 cameraPosition)
        {
            Vector3 vec = cameraPosition - sourcePosition;
            float distance = Vector3.Scale(pars.axesMultiplier, vec).magnitude;
            float strength = Mathf.Clamp01(1 - (distance - pars.clippingDistance) / pars.falloffScale);

            return Power.Evaluate(strength, pars.falloffDegree);
        }

        /// <summary>
        /// Returns displacement, opposite to the direction to the source in camera's local space.
        /// </summary>
        public static Displacement Direction(Vector3 sourcePosition, Vector3 cameraPosition, Quaternion cameraRotation)
        {
            Displacement direction = Displacement.Zero;
            direction.position = (cameraPosition - sourcePosition).normalized;
            direction.position = Quaternion.Inverse(cameraRotation) * direction.position;

            direction.eulerAngles.x = direction.position.z;
            direction.eulerAngles.y = direction.position.x;
            direction.eulerAngles.z = -direction.position.x;

            return direction;
        }

        [System.Serializable]
        public class StrengthAttenuationParams
        {
            /// <summary>
            /// Radius in which shake doesn't lose strength.
            /// </summary>
            [Tooltip("Radius in which shake doesn't lose strength.")]
            public float clippingDistance = 10;

            /// <summary>
            /// Defines how fast strength falls with distance.
            /// </summary>
            [Tooltip("How fast strength falls with distance.")]
            public float falloffScale = 50;

            /// <summary>
            /// Power of the falloff function.
            /// </summary>
            [Tooltip("Power of the falloff function.")]
            public Degree falloffDegree = Degree.Quadratic;

            /// <summary>
            /// Contribution of each axis to distance. E. g. (1, 1, 0) for a 2D game in XY plane.
            /// </summary>
            [Tooltip("Contribution of each axis to distance. E. g. (1, 1, 0) for a 2D game in XY plane.")]
            public Vector3 axesMultiplier = Vector3.one;
        }
    }
}
