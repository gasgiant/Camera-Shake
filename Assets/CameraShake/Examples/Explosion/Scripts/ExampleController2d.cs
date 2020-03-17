using UnityEngine;

// Don't forget to add this
using CameraShake;

public class ExampleController2d : MonoBehaviour
{
    // Parameters of the shake to tweak in the inspector
    public PerlinShake.Params shakeParams;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindObjectOfType<Explosion>().Explode();

            // Making camera shake
            CameraShaker.Shake(new PerlinShake(shakeParams));
        }
    }
}
