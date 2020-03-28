using UnityEngine;

namespace CameraShake
{
    public class PerlinShake : ICameraShake
    {
        readonly Params pars;
        readonly Envelope envelope;

        public IAmplitudeController AmplitudeController;

        Vector2[] seeds;
        float time;
        Vector3? sourcePosition;
        float norm;

        /// <summary>
        /// Creates an instance of PerlinShake.
        /// </summary>
        /// <param name="parameters">Parameters of the shake.</param>
        /// <param name="maxAmplitude">Maximum amplitude of the shake.</param>
        /// <param name="sourcePosition">World position of the source of the shake.</param>
        /// <param name="manualStrengthControl">Pass true if you want to control amplitude manually.</param>
        public PerlinShake(
            Params parameters,
            float maxAmplitude = 1,
            Vector3? sourcePosition = null,
            bool manualStrengthControl = false)
        {
            pars = parameters;
            envelope = new Envelope(pars.envelope, maxAmplitude,
                manualStrengthControl ?
                    Envelope.EnvelopeControlMode.Manual : Envelope.EnvelopeControlMode.Auto);
            AmplitudeController = envelope;
            this.sourcePosition = sourcePosition;
        }

        public Displacement CurrentDisplacement { get; private set; }
        public bool IsFinished { get; private set; }

        public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
        {
            seeds = new Vector2[pars.noiseModes.Length];
            norm = 0;
            for (int i = 0; i < seeds.Length; i++)
            {
                seeds[i] = Random.insideUnitCircle * 20;
                norm += pars.noiseModes[i].amplitude;
            }
        }

        public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
        {
            if (envelope.IsFinished)
            {
                IsFinished = true;
                return;
            }
            time += deltaTime;
            envelope.Update(deltaTime);

            Displacement disp = Displacement.Zero;
            for (int i = 0; i < pars.noiseModes.Length; i++)
            {
                disp += pars.noiseModes[i].amplitude / norm *
                    SampleNoise(seeds[i], pars.noiseModes[i].freq);
            }

            CurrentDisplacement = envelope.Intensity * Displacement.Scale(disp, pars.strength);
            if (sourcePosition != null)
                CurrentDisplacement *= Attenuator.Strength(pars.attenuation, sourcePosition.Value, cameraPosition);
        }

        private Displacement SampleNoise(Vector2 seed, float freq)
        {
            Vector3 position = new Vector3(
                Mathf.PerlinNoise(seed.x + time * freq, seed.y),
                Mathf.PerlinNoise(seed.x, seed.y + time * freq),
                Mathf.PerlinNoise(seed.x + time * freq, seed.y + time * freq));
            position -= Vector3.one * 0.5f;

            Vector3 rotation = new Vector3(
                Mathf.PerlinNoise(-seed.x - time * freq, -seed.y),
                Mathf.PerlinNoise(-seed.x, -seed.y - time * freq),
                Mathf.PerlinNoise(-seed.x - time * freq, -seed.y - time * freq));
            rotation -= Vector3.one * 0.5f;

            return new Displacement(position, rotation);
        }


        [System.Serializable]
        public class Params
        {
            /// <summary>
            /// Strength of the shake for each axis.
            /// </summary>
            [Tooltip("Strength of the shake for each axis.")]
            public Displacement strength = new Displacement(Vector3.zero, new Vector3(2, 2, 0.8f));

            /// <summary>
            /// Layers of perlin noise with different frequencies.
            /// </summary>
            [Tooltip("Layers of perlin noise with different frequencies.")]
            public NoiseMode[] noiseModes = { new NoiseMode(12, 1) };

            /// <summary>
            /// Strength over time.
            /// </summary>
            [Tooltip("Strength of the shake over time.")]
            public Envelope.EnvelopeParams envelope;

            /// <summary>
            /// How strength falls with distance from the shake source.
            /// </summary>
            [Tooltip("How strength falls with distance from the shake source.")]
            public Attenuator.StrengthAttenuationParams attenuation;
        }


        [System.Serializable]
        public struct NoiseMode
        {
            public NoiseMode(float freq, float amplitude)
            {
                this.freq = freq;
                this.amplitude = amplitude;
            }

            /// <summary>
            /// Frequency multiplier for the noise.
            /// </summary>
            [Tooltip("Frequency multiplier for the noise.")]
            public float freq;

            /// <summary>
            /// Amplitude of the mode.
            /// </summary>
            [Tooltip("Amplitude of the mode.")]
            [Range(0, 1)]
            public float amplitude;
        }
    }
}
