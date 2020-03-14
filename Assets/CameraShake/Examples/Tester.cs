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
            CameraShaker.Presets.ShortShake2D();
        }
    }
}
