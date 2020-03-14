using UnityEngine;
using CameraShake;

public class Greande : MonoBehaviour
{
    public void Explode(float explosionStrength, PerlinShake.Params shakeParams)
    {
        CameraShaker.Shake(new PerlinShake(shakeParams, explosionStrength, transform.position));
    }
}
