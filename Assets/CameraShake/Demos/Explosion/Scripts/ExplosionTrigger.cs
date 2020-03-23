using UnityEngine;

// Don't forget to add this.
using CameraShake;

public class ExplosionTrigger : MonoBehaviour
{
    // Parameters of the shake to tweak in the inspector.
    public PerlinShake.Params shakeParams;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindObjectOfType<Explosion>().Explode();

            // Creating new instance of a shake and registering it in the system.
            CameraShaker.Shake(new PerlinShake(shakeParams));
        }
    }
}
