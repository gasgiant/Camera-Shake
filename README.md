# Camera-Shake
Camera shake for Unity. 

## Usage
### Minimal example

1. Make the camera a child of another gameobject. When you want to move the camera move the parent. 

2. Add `CameraShaker` component to the camera gameobject. 

3. Call `CameraShaker.Presets.Explosion2D` to start a shake. Don't forget to add `CameraShake` namespace.
```csharp
using UnityEngine;
using CameraShake;

public class MinimalExample : MonoBehaviour
{
    private void Explode()
    {
        // Something explodes ...

        CameraShaker.Presets.Explosion2D();
    }
}
```
### One-liners

Class `CameraShakePresets` allows to generate common shake types with one line of code. You can use it like in the example above. 

__ShortShake3D__  
Suitable for short and snappy shakes in 3D. Rotates camera in all three axes. Uses `BounceShake` algorithm.
| Parameter        | Description | 
| :------------- |:-------------|
| strength     | Strength of the shake.|
| freq     | Frequency of shaking.|
| numBounces     | Number of vibrations before stop.|

__ShortShake2D__  
Suitable for short and snappy shakes in 2D. Moves camera in it's X and Y axes and rotates it in Z axis. Uses `BounceShake` algorithm.
| Parameter        | Description | 
| :------------- |:-------------|
| positionStrength     | Strength of motion in X and Y axes.|
| rotationStrength     | Strength of rotation in Z axis.|
| freq     | Frequency of shaking.|
| numBounces     | Number of vibrations before stop.|

__Explosion3D__  
Suitable for longer and stronger shakes in 3D. Rotates camera in all three axes. Uses `PerlinShake` algorithm.
| Parameter        | Description | 
| :------------- |:-------------|
| strength     | Strength of the shake.|
| duration     | Duration of the shake.|

__Explosion2D__  
Suitable for longer and stronger shakes in 2D. Moves camera in it's X and Y axes and rotates it in Z axis. Uses `PerlinShake` algorithm.
| Parameter        | Description | 
| :------------- |:-------------|
| positionStrength     | Strength of motion in X and Y axes.|
| rotationStrength     | Strength of rotation in Z axis.|
| duration     | Duration of the shake.|

### Getting more control
If you need more options, than provided by presets, you need to create an instance of some shake class and pass it into the `CameraShaker.Shake`. The example below is for `PerlinShake`.

```csharp
using UnityEngine;
using CameraShake;

public class Gun : MonoBehaviour
{
    public PerlinShake.Params shakeParams;

    public void Shoot()
    {
        // Shooting ...

        CameraShaker.Shake(new PerlinShake(shakeParams));
    }
}
```
The constructor of `PerlinShake` teakes instance of `PerlinShake.Params` as an input. You can expose the parameter variable on some `MonoBehaviour` or `ScriptableObject` to tweak the parameters in the inspector.

![shakeparams](https://i.imgur.com/TLsOKIA.png "Shake Params Inspector")
