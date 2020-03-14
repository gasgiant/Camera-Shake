using UnityEngine;

namespace CameraShake
{
    public class CameraShakePresets
    {
        readonly CameraShaker shaker;

        public CameraShakePresets(CameraShaker shaker)
        {
            this.shaker = shaker;
        }

        public void ShortShake2D(
            float positionStrength = 0.04f,
            float rotationStrength = 0.08f,
            float freq = 25,
            int numBounces = 5)
        {
            BounceShake.Params pars = new BounceShake.Params
            {
                positionStrength = positionStrength,
                rotationStrength = rotationStrength,
                freq = freq,
                numBounces = numBounces
            };
            shaker.RegisterShake(new BounceShake(pars));
        }

        public void ShortShake3D(
            float strength = 0.2f,
            float freq = 25,
            int numBounces = 5)
        {
            BounceShake.Params pars = new BounceShake.Params
            {
                axesMultiplier = new Displacement(Vector3.zero, new Vector3(1, 1, 0.4f)),
                rotationStrength = strength,
                freq = freq,
                numBounces = numBounces
            };
            shaker.RegisterShake(new BounceShake(pars));
        }

        public void Explosion2D(
            float positionStrength = 1f,
            float rotationStrength = 3,
            float duration = 0.5f)
        {
            PerlinShake.NoiseMode[] modes =
            {
                new PerlinShake.NoiseMode(8, 1),
                new PerlinShake.NoiseMode(20, 0.4f)
            };
            Envelope.EnvelopeParams envelopePars = new Envelope.EnvelopeParams();
            envelopePars.decay = duration <= 0 ? 1 : 1 / duration;
            PerlinShake.Params pars = new PerlinShake.Params
            {
                strength = new Displacement(new Vector3(1, 1) * positionStrength, Vector3.forward * rotationStrength),
                noiseModes = modes,
                envelope = envelopePars,
            };
            shaker.RegisterShake(new PerlinShake(pars));
        }

        public void Explosion3D(
            float strength = 8f,
            float duration = 0.7f)
        {
            PerlinShake.NoiseMode[] modes =
            {
                new PerlinShake.NoiseMode(6, 1),
                new PerlinShake.NoiseMode(20, 0.2f)
            };
            Envelope.EnvelopeParams envelopePars = new Envelope.EnvelopeParams();
            envelopePars.decay = duration <= 0 ? 1 : 1 / duration;
            PerlinShake.Params pars = new PerlinShake.Params
            {
                strength = new Displacement(Vector3.zero, new Vector3(1, 1, 0.5f) * strength),
                noiseModes = modes,
                envelope = envelopePars,
            };
            shaker.RegisterShake(new PerlinShake(pars));
        }
    }
}
