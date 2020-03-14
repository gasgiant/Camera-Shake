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
            float posStrength = 0.05f, 
            float rotStrength = 0.1f,
            float freq = 25,
            int numBounces = 3)
        {
            BounceShake.Params pars = new BounceShake.Params
            {
                positionStrength = posStrength,
                rotationStrength = rotStrength,
                freq = freq,
                numBounces = numBounces
            };
            shaker.RegisterShake(new BounceShake(pars));
        }

        public void ShortShake3D(
            float strength = 0.2f,
            float freq = 25,
            int numBounces = 3)
        {
            BounceShake.Params pars = new BounceShake.Params
            {
                axesMultiplier = new Displacement(Vector3.zero, Vector3.one),
                rotationStrength = strength,
                freq = freq,
                numBounces = numBounces
            };
            shaker.RegisterShake(new BounceShake(pars));
        }
    }
}
