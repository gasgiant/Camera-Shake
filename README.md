# Camera-Shake
Camera shake for Unity. 

## Minimal example

1. Make the camera a child of another gameobject. When you want to move the camera move the parent. 

2. Add `CameraShaker` component to the camera gameobject. 

3. Call `CameraShaker.Presets.ShortShake2D` to start a short shake suitable for 2D games. Don't forget to add `CameraShake` namespace.

```csharp
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
```

