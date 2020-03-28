using UnityEngine;

// Don't forget to add this.
using CameraShake;

public class ShakeTrigger : MonoBehaviour
{
    // Parameters of the shake to tweak in the inspector.
    public BounceShake.Params shakeParams;

    // This is called by animator.
    public void Stomp()
    {
        Vector3 sourcePosition = transform.position;

        // Creating new instance of a shake and registering it in the system.
        CameraShaker.Shake(new BounceShake(shakeParams, sourcePosition));
    }
}
