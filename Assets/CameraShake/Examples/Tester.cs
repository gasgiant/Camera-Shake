using UnityEngine;
using CameraShake;

public class Tester : MonoBehaviour
{
    public BounceShake.Params pars;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraShaker.Shake(new BounceShake(pars));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraShaker.Presets.ShortShake3D();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CameraShaker.Presets.Explosion3D();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CameraShaker.Presets.ShortShake2D();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CameraShaker.Presets.Explosion2D();
        }
    }
}
