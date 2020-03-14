using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Representation of translation and rotation. 
    /// </summary>
    [System.Serializable]
    public struct Displacement
    {
        public Vector3 position;
        public Vector3 eulerAngles;

        public Displacement(Vector3 position, Vector3 eulerAngles)
        {
            this.position = position;
            this.eulerAngles = eulerAngles;
        }

        public Displacement(Vector3 position)
        {
            this.position = position;
            this.eulerAngles = Vector3.zero;
        }

        public static Displacement Zero
        {
            get
            {
                return new Displacement(Vector3.zero, Vector3.zero);
            }
        }

        public static Displacement operator +(Displacement a, Displacement b)
        {
            return new Displacement(a.position + b.position,
                b.eulerAngles + a.eulerAngles);
        }

        public static Displacement operator -(Displacement a, Displacement b)
        {
            return new Displacement(a.position - b.position,
                b.eulerAngles - a.eulerAngles);
        }

        public static Displacement operator -(Displacement disp)
        {
            return new Displacement(-disp.position, -disp.eulerAngles);
        }

        public static Displacement operator *(Displacement coords, float number)
        {
            return new Displacement(coords.position * number,
                coords.eulerAngles * number);
        }

        public static Displacement operator *(float number, Displacement coords)
        {
            return coords * number;
        }

        public static Displacement operator /(Displacement coords, float number)
        {
            return new Displacement(coords.position / number,
                coords.eulerAngles / number);
        }

        public static Displacement Scale(Displacement a, Displacement b)
        {
            return new Displacement(Vector3.Scale(a.position, b.position),
                Vector3.Scale(b.eulerAngles, a.eulerAngles));
        }

        public static Displacement Lerp(Displacement a, Displacement b, float t)
        {
            return new Displacement(Vector3.Lerp(a.position, b.position, t),
                Vector3.Lerp(a.eulerAngles, b.eulerAngles, t));
        }

        public Displacement ScaledBy(float posScale, float rotScale)
        {
            return new Displacement(position * posScale, eulerAngles * rotScale);
        }

        public Displacement Normalized
        {
            get
            {
                return new Displacement(position.normalized, eulerAngles.normalized);
            }
        }

        public static Displacement InsideUnitSpheres()
        {
            return new Displacement(Random.insideUnitSphere, Random.insideUnitSphere);
        }
    }
}