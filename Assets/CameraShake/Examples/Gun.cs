using UnityEngine;
using CameraShake;

public class Gun : MonoBehaviour
{
    public PerlinShake.Params shakeParams;

    public void Shoot()
    {
        // Shooting...

        CameraShaker.Shake(new PerlinShake(shakeParams));
    }
}
