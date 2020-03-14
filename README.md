# Camera-Shake
Camera shake for Unity. 

## Usage
### Setup
1. Make the camera a child of another gameobject. When you want to move the camera move the parent. 

2. Add `CameraShaker` component to the camera gameobject. 

By default `CameraShaker` works with the transform of its gameobject. Alternatively, you can add `CameraShaker` to any gameobject you like and set it's `cameraTransform` field in inspector, or by calling `CameraShaker.Instance.SetCameraTransform`.

### Using Presets
The simplest way to shake the camera is to use presets. Class `CameraShakePresets` allows to generate some common shake types with one line of code.

 Call `CameraShaker.Presets.Explosion2D` to start a shake. Don't forget to add `CameraShake` namespace.
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

### Without presets
If you need more options, than provided by presets, you need to create an instance of some shake class and pass it into the `CameraShaker.Shake`. The example below is for `PerlinShake`.

```csharp
using UnityEngine;
using CameraShake;

public class Gun : MonoBehaviour
{
    [SerializeField]
    PerlinShake.Params shakeParams;

    public void Shoot()
    {
        // Shooting ...

        CameraShaker.Shake(new PerlinShake(shakeParams));
    }
}
```
The constructor of `PerlinShake` tekes an instance of `PerlinShake.Params` as an input. You can expose the parameter variable on some `MonoBehaviour` or `ScriptableObject` to tweak the parameters in the inspector.

![shakeparams](https://i.imgur.com/TLsOKIA.png "PerlinShake.Params Inspector")

Shakes can have more options in their constructors. For example you can pass position of the source of the explosion into the `PerlinShake` constructor, and the shake will change the strength based on the distance from the source to the camera.

```csharp
public class Grenade : MonoBehaviour
{
    public void Explode(float explosionStrength, PerlinShake.Params shakeParams)
    {
        CameraShaker.Shake(new PerlinShake(shakeParams, explosionStrength, transform.position));
    }
}
```
### Writing custom shakes
`CameraShaker` works with any calss, that implements `ICameraShake` interface. 

```csharp
public interface ICameraShake
    {
        // Represents current position and rotation of the camera according to the shake.
        Displacement CurrentDisplacement { get; }

        // Shake system will dispose the shake on the first frame when this is true.

        bool IsFinished { get; }

        // CameraShaker calls this when the shake is added to the list of active shakes.

        void Initialize(Vector3 cameraPosition, Quaternion cameraRotation);

        // CameraShaker calls this every frame on active shakes.

        void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation);
    }
```
Here is a basic example of custom shake class.
```csharp
public class VeryBadShake : ICameraShake
{
    readonly float intensity;
    readonly float duration;
    float time;

    public VeryBadShake(float intensity, float duration)
    {
        this.intensity = intensity;
        this.duration = duration;
    }

    public Displacement CurrentDisplacement { get; private set; }

    public bool IsFinished { get; private set; }

    public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
    {
    }

    public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
    {
        time += deltaTime;
        if (time > duration)
            IsFinished = true;
        CurrentDisplacement = 
            new Displacement(Random.insideUnitCircle * intensity, Vector3.zero);
    }
}
```

## Presets

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
