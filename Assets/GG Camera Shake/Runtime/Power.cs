namespace CameraShake
{
    public static class Power
    {
        public static float Evaluate(float value, Degree degree)
        {
            switch (degree)
            {
                case Degree.Linear:
                    return value;
                case Degree.Quadratic:
                    return value * value;
                case Degree.Cubic:
                    return value * value * value;
                case Degree.Quadric:
                    return value * value * value * value;
                default:
                    return value;
            }
        }
    }

    public enum Degree { Linear, Quadratic, Cubic, Quadric }
}
