using System;
using SFML.System;

public static class VectorMath {
    public static bool IsZero(Vector2f vector) {
        return MathF.Abs(vector.X) <= float.Epsilon
            && MathF.Abs(vector.Y) <= float.Epsilon;
    }
    
    public static Vector2f Normalize(Vector2f vector) {
        if (IsZero(vector)) return new Vector2f();
        return vector / LengthOf(vector);
    }

    public static Vector2f Reflect(Vector2f vector, Vector2f normal) {
        return (2.0f * Dot(normal, vector)) * normal - vector;
    }
    
    public static float LengthOf(Vector2f vector) {
        return MathF.Sqrt(Dot(vector, vector));
    }

    public static float Dot(Vector2f a, Vector2f b) {
        return a.X * b.X + a.Y * b.Y;
    }

    public static Vector2f Random() {
        var rand = new Random();
        return Normalize(new Vector2f(
            (float) rand.NextDouble() * 2.0f - 1.0f,
            (float) rand.NextDouble() * 2.0f - 1.0f
        ));
    }

    public static Vector2f FromAngle(float radians) {
        return new Vector2f(MathF.Cos(radians), MathF.Sin(radians));
    }
}