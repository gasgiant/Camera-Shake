using UnityEngine;

namespace CameraShake
{
    public class BounceShake : ICameraShake
    {
        readonly Params pars;
        readonly AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        readonly Vector3? sourcePosition = null;

        float attenuation = 1;
        Displacement direction;
        Displacement previousWaypoint;
        Displacement currentWaypoint;
        int bounceIndex;
        float t;

        /// <summary>
        /// Creates an instance of BounceShake.
        /// </summary>
        /// <param name="parameters">Parameters of the shake.</param>
        /// <param name="sourcePosition">World position of the source of the shake.</param>
        public BounceShake(Params parameters, Vector3? sourcePosition = null)
        {
            this.sourcePosition = sourcePosition;
            pars = parameters;
            Displacement rnd = Displacement.InsideUnitSpheres();
            direction = Displacement.Scale(rnd, pars.axesMultiplier).Normalized;
        }

        /// <summary>
        /// Creates an instance of BounceShake.
        /// </summary>
        /// <param name="parameters">Parameters of the shake.</param>
        /// <param name="initialDirection">Initial direction of the shake motion.</param>
        /// <param name="sourcePosition">World position of the source of the shake.</param>
        public BounceShake(Params parameters, Displacement initialDirection, Vector3? sourcePosition = null)
        {
            this.sourcePosition = sourcePosition;
            pars = parameters;
            direction = Displacement.Scale(initialDirection, pars.axesMultiplier).Normalized;
        }

        public Displacement CurrentDisplacement { get; private set; }
        public bool IsFinished { get; private set; }
        public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
        {
            attenuation = sourcePosition == null ?
                1 : Attenuator.Strength(pars.attenuation, sourcePosition.Value, cameraPosition);
            currentWaypoint = attenuation * direction.ScaledBy(pars.positionStrength, pars.rotationStrength);
        }

        public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
        {
            if (t < 1)
            {

                t += deltaTime * pars.freq;
                if (pars.freq == 0) t = 1;

                CurrentDisplacement = Displacement.Lerp(previousWaypoint, currentWaypoint,
                    moveCurve.Evaluate(t));
            }
            else
            {
                t = 0;
                CurrentDisplacement = currentWaypoint;
                previousWaypoint = currentWaypoint;
                bounceIndex++;
                if (bounceIndex > pars.numBounces)
                {
                    IsFinished = true;
                    return;
                }

                Displacement rnd = Displacement.InsideUnitSpheres();
                direction = -direction
                    + pars.randomness * Displacement.Scale(rnd, pars.axesMultiplier).Normalized;
                direction = direction.Normalized;
                float decayValue = 1 - (float)bounceIndex / pars.numBounces;
                currentWaypoint = decayValue * decayValue * attenuation
                    * direction.ScaledBy(pars.positionStrength, pars.rotationStrength);
            }
        }

        [System.Serializable]
        public class Params
        {
            /// <summary>
            /// Strength of the shake for positional axes.
            /// </summary>
            [Tooltip("Strength of the shake for positional axes.")]
            public float positionStrength = 0.05f;

            /// <summary>
            /// Strength of the shake for rotational axes.
            /// </summary>
            [Tooltip("Strength of the shake for rotational axes.")]
            public float rotationStrength = 0.1f;

            /// <summary>
            /// Preferred direction of shaking.
            /// </summary>
            [Tooltip("Preferred direction of shaking.")]
            public Displacement axesMultiplier = new Displacement(Vector2.one, Vector3.forward);

            /// <summary>
            /// Frequency of shaking.
            /// </summary>
            [Tooltip("Frequency of shaking.")]
            public float freq = 25;

            /// <summary>
            /// Number of vibrations before stop.
            /// </summary>
            [Tooltip("Number of vibrations before stop.")]
            public int numBounces = 5;

            /// <summary>
            /// Randomness of motion.
            /// </summary>
            [Range(0, 1)]
            [Tooltip("Randomness of motion.")]
            public float randomness = 0.5f;

            /// <summary>
            /// How strength falls with distance from the shake source.
            /// </summary>
            [Tooltip("How strength falls with distance from the shake source.")]
            public Attenuator.StrengthAttenuationParams attenuation;
        }
    }
}
