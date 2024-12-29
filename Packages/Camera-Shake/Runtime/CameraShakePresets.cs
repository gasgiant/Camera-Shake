using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Contains shorthands for creating common shake types.
    /// </summary>
    public class CameraShakePresets
    {
        readonly CameraShaker shaker;

        private BounceShakePool poolBounceShake;
        private PerlinShakePool poolPerlinShake;

        public CameraShakePresets(CameraShaker shaker)
        {
            poolBounceShake = new BounceShakePool();
            poolBounceShake.InitPool();

            poolPerlinShake = new PerlinShakePool();
            poolPerlinShake.InitPool();

            this.shaker = shaker;
        }

        /// <summary>
        /// Suitable for short and snappy shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis. 
        /// </summary>
        /// <param name="positionStrength">Strength of motion in X and Y axes.</param>
        /// <param name="rotationStrength">Strength of rotation in Z axis.</param>
        /// <param name="freq">Frequency of shaking.</param>
        /// <param name="numBounces">Number of vibrations before stop.</param>
        public void ShortShake2D(
            float positionStrength = 0.08f,
            float rotationStrength = 0.1f,
            float freq = 25,
            int numBounces = 5)
        {
            BounceShake.Params pars = poolBounceShake.GetParams();
            pars.positionStrength = positionStrength;
            pars.rotationStrength = rotationStrength;
            pars.freq = freq;
            pars.numBounces = numBounces;

            BounceShake shake = poolBounceShake.GetShake();
            shake.Initialize(pars);

            shaker.RegisterShake(shake);
        }

        /// <summary>
        /// Suitable for longer and stronger shakes in 3D. Rotates camera in all three axes.
        /// </summary>
        /// <param name="strength">Strength of the shake.</param>
        /// <param name="freq">Frequency of shaking.</param>
        /// <param name="numBounces">Number of vibrations before stop.</param>
        public void ShortShake3D(
            float strength = 0.3f,
            float freq = 25,
            int numBounces = 5)
        {
            BounceShake.Params pars = poolBounceShake.GetParams();
            pars.axesMultiplier = new Displacement(Vector3.zero, new Vector3(1, 1, 0.4f));
            pars.rotationStrength = strength;
            pars.freq = freq;
            pars.numBounces = numBounces;

            BounceShake shake = poolBounceShake.GetShake();
            shake.Initialize(pars);

            shaker.RegisterShake(shake);
        }

        /// <summary>
        /// Suitable for longer and stronger shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis.
        /// </summary>
        /// <param name="positionStrength">Strength of motion in X and Y axes.</param>
        /// <param name="rotationStrength">Strength of rotation in Z axis.</param>
        /// <param name="duration">Duration of the shake.</param>
        public void Explosion2D(
            float positionStrength = 1f,
            float rotationStrength = 3,
            float duration = 0.5f)
        {
            PerlinShake.NoiseMode noiseMode1 = new PerlinShake.NoiseMode(8, 1);
            PerlinShake.NoiseMode noiseMode2 = new PerlinShake.NoiseMode(20, 0.3f);

            Envelope.EnvelopeParams envelopePars = new Envelope.EnvelopeParams();
            envelopePars.decay = duration <= 0 ? 1 : 1 / duration;

            PerlinShake.Params param = poolPerlinShake.GetParams();
            param.strength = new Displacement(new Vector3(1, 1) * positionStrength, Vector3.forward * rotationStrength);
            param.envelope = envelopePars;
            param.noiseModes[0] = noiseMode1;
            param.noiseModes[1] = noiseMode2;

            PerlinShake shake = poolPerlinShake.GetShake();
            shake.Initialize(param);

            shaker.RegisterShake(shake);
        }

        /// <summary>
        /// Suitable for longer and stronger shakes in 3D. Rotates camera in all three axes. 
        /// </summary>
        /// <param name="strength">Strength of the shake.</param>
        /// <param name="duration">Duration of the shake.</param>
        public void Explosion3D(
            float strength = 8f,
            float duration = 0.7f)
        {
            PerlinShake.NoiseMode noiseMode1 = new PerlinShake.NoiseMode(6, 1);
            PerlinShake.NoiseMode noiseMode2 = new PerlinShake.NoiseMode(20, 0.2f);

            Envelope.EnvelopeParams envelopePars = poolPerlinShake.GetEnvelopeParams();
            envelopePars.decay = duration <= 0 ? 1 : 1 / duration;

            PerlinShake.Params param = poolPerlinShake.GetParams();
            param.strength = new Displacement(Vector3.zero, new Vector3(1, 1, 0.5f) * strength);
            param.envelope = envelopePars;
            param.noiseModes[0] = noiseMode1;
            param.noiseModes[1] = noiseMode2;

            PerlinShake shake = poolPerlinShake.GetShake();
            shake.Initialize(param);
            shaker.RegisterShake(shake);
        }
    }
}
