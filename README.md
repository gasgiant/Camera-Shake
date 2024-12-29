# GG Camera Shake
An asset for camera shake in Unity. 

__Features__:
* One line of code shake with presets
* Several shake algorithms suitable for a wide range of use cases
* Change strength and direction of the shake depending on  
position of the shake source
* Easy to write custom shakes

[__Watch video tutorial__](https://youtu.be/fn3hIPLbSn8)
![Thumbnail](https://i.imgur.com/n0ndQ9x.png "Video thumbnail")

## Table of Contents
1. [Installation](#installation)
2. [Usage](#usage)
    * [Setup](#setup)
    * [Using Presets](#using-presets)
    * [Without Presets](#without-presets)
2. [Presets](#presets)
2. [PerlinShake](#perlinshake)
2. [BounceShake](#bounceshake)
2. [KickShake](#kickshake)
2. [Time Envelope](#time-envelope)
2. [Spatial Attenuation](#spatial-attenuation)
2. [Writing Custom Shakes](#writing-custom-shakes)

## Installation
1. You can install the package via Package Manager using this URL: `https://github.com/StinkySteak/Camera-Shake.git?path=Packages/Camera-Shake`.

2. Or you can [download](https://github.com/gasgiant/Camera-Shake/releases/download/v1.0.0/ggcamerashake.unitypackage) .unitypackage and import it into your project.



## Usage
### Setup
1. Make the camera a child of another gameobject. When you want to move the camera move the parent. 

2. Add `CameraShaker` component to the camera gameobject. 

By default `CameraShaker` works with its own transform. Alternatively, you can add `CameraShaker` to any gameobject you like and set it's `cameraTransform` field in inspector, or by calling `CameraShaker.Instance.SetCameraTransform`.

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

### Without Presets
If you need more options, than provided by presets, you need to create an instance of some shake class and pass it into the `CameraShaker.Shake`. There are three default shake classes: [PerlinShake](#perlinshake), [BounceShake](#bounceshake) and [KickShake](#kickshake). You can also write [your own](#writing-custom-shakes) shakes. The example below is for `PerlinShake`.

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
The constructor of `PerlinShake` takes an instance of `PerlinShake.Params` as an input. You can expose the parameters variable on some `MonoBehaviour` or `ScriptableObject` to tweak the parameters in the inspector.

![shakeparams](https://i.imgur.com/TLsOKIA.png "PerlinShake.Params Inspector")

Shakes can have more options in their constructors. For example you can pass position of the source of the explosion into the `PerlinShake` constructor, and the shake will change the strength depending on the distance from the source to the camera.

```csharp
public class Grenade : MonoBehaviour
{
    public void Explode(float explosionStrength, PerlinShake.Params shakeParams)
    {
        CameraShaker.Shake(new PerlinShake(shakeParams, explosionStrength, transform.position));
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
Suitable for short and snappy shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis. Uses `BounceShake` algorithm.
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
Suitable for longer and stronger shakes in 2D. Moves camera in X and Y axes and rotates it in Z axis. Uses `PerlinShake` algorithm.
| Parameter        | Description | 
| :------------- |:-------------|
| positionStrength     | Strength of motion in X and Y axes.|
| rotationStrength     | Strength of rotation in Z axis.|
| duration     | Duration of the shake.|

## PerlinShake
`PerlinShake` combines layers of Perlin noise with different frequencies to create smooth and nuanced shake. Works better for longer shakes. For very short shakes consider using `BounceShake`.
### Constructor

| Parameter        |   |  Description | 
| :------------- |:-------------|:-------------|
| parameters     | | Parameters of the shake. |
| maxAmplitude     | | Maximum amplitude of the shake.|
| sourcePosition | | World position of the source of the shake. |
| manualStrengthControl| false | Play shake once automatically. |
|  | true| Manually control strength over time. |

For more details on `maxAmplitude` and `manualStrengthControl` see [Time Envelope](#time-envelope).

### Params

| Parameter        | Description | 
| :------------- |:-------------|
| strength     | Strength of the shake for each axis. |
| noiseModes     | Layers of Perlin noise with different frequencies. |
| envelope     | Strength of the shake over time. |
| attenuation     | How strength falls with distance from the shake source. |

For more details on `envelope` see [Time Envelope](#time-envelope). For more details on `attenuation` see [Spatial Attenuation](#spatial-attenuation).

## BounceShake
`BounceShake` is useful for short and precise shakes. Unlike `PerlinShake`, it will provide reliable shake strength. Consider using `PerlinShake` for longer and stronger shakes.

### Constructors

#### BounceShake (first overload)
| Parameter        | Description | 
| :------------- |:-------------|
| parameters     | Parameters of the shake. |
| initialDirection     | Initial direction of the shake motion. |
| sourcePosition     | World position of the source of the shake. |

#### BounceShake (second overload)
Creates `BounceShake` with random initial direction.
| Parameter        | Description | 
| :------------- |:-------------|
| parameters     | Parameters of the shake. |
| sourcePosition     | World position of the source of the shake. |

### Params
| Parameter        | Description | 
| :------------- |:-------------|
| positionStrength     | Parameters of the shake. |
| rotationStrength     | Strength of the shake for rotational axes. |
| axesMultiplier     | Preferred direction of shaking. |
| freq     | Frequency of shaking. |
| numBounces     | Number of vibrations before stop. |
| randomness     | Randomness of motion. |
| attenuation     | How strength falls with distance from the shake source. |

 For more details on `attenuation` see [Spatial Attenuation](#spatial-attenuation).


## KickShake
Makes one kick in specified direction. Useful for recoil.

### Constructors

#### KickShake (first overload)
| Parameter        | Description | 
| :------------- |:-------------|
| parameters     | Parameters of the shake. |
| direction     | Direction of the kick. |

#### KickShake (second overload)
Creates an instance of KickShake in the direction from the source to the camera.
| Parameter        | Description | 
| :------------- |:-------------|
| parameters     | Parameters of the shake. |
| sourcePosition     | World position of the source of the shake. |
| attenuateStrength     | Change strength depending on distance from the camera? |

### Params
| Parameter        | Description | 
| :------------- |:-------------|
| strength     | Strength of the shake for each axis. |
| attackTime     | How long it takes to move forward. |
| attackCurve     | Forward motion curve. |
| releaseTime     | How long it takes to move back. |
| releaseCurve     | Back motion curve. |
| attenuation     | How strength falls with distance from the shake source. |

 For more details on `attenuation` see [Spatial Attenuation](#spatial-attenuation).

## Time Envelope
Class `Envelope` controls amplitude of the shake over time. It can work in two modes. In automatic mode it plays the shake ones with selected `maxAmplitude`. 

In manual mode you can keep the reference to the `PerlinShake` and change amplitude whenever you like.
```csharp
public class Vibrator : MonoBehaviour
{
    [SerializeField]
    PerlinShake.Params params;
    PerlinShake shake;

    private void Start()
    {
        shake = new PerlinShake(params);
        CameraShaker.Shake(shake);
    }

    public void Vibrate(float amplitude)
    {
        shake.AmplitudeController.SetTargetAmplitude(amplitude);
    }
}
```

### EnvelopeParams
[See interactive demonstration.](https://www.desmos.com/calculator/e9wxr78uu2)
| Parameter        | Description | 
| :------------- |:-------------|
| attack     | How fast the amplitude increases. |
| sustain     | How long in seconds the amplitude holds maximum value. |
| decay     | How fast the amplitude decreases. |
| degree     | Power in which the amplitude is raised to get intensity. |

## Spatial Attenuation

Class `Attenuator` provides methods for changing strength and direction of the shake depending on position of the shake source relative to the camera.

### StrengthAttenuationParams
[See interactive demonstration.](https://www.desmos.com/calculator/iivcfrotk8)
| Parameter        | Description | 
| :------------- |:-------------|
| clippingDistance     | Radius in which shake doesn't lose strength. |
| falloffScale     | How fast strength falls with distance. |
| falloffDegree     | Power of the falloff function. |
| axesMultiplier     | Contribution of each axis to distance. E. g. (1, 1, 0) for a 2D game in XY plane. |

## Writing Custom Shakes
`CameraShaker` works with any class that implements `ICameraShake` interface. 

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
Here is a basic example of a custom shake class.
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


