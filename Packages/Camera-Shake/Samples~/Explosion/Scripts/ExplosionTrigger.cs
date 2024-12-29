using UnityEngine;

// Don't forget to add this.
using CameraShake;

public class ExplosionTrigger : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindObjectOfType<Explosion>().Explode();

            // Shaking the camera.
            CameraShaker.Presets.ShortShake2D();
        }
    }
}
