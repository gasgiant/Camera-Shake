using UnityEngine;
using CameraShake;

public class MinimalExample : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraShaker.Presets.ShortShake2D();
        }
    }
}
