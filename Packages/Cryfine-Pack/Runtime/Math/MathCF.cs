using System;

namespace Cryfine
{
    public static class MathCF
    {
        public static float DisMan3D (float x1, float y1, float z1, float x2, float y2, float z2) 
        { 
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2) + Math.Abs(z1 - z2); 
        }
        public static float DisMan2D (float x1, float y1, float x2, float y2) 
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2); 
        }
        public static float DisSqr2D (float x1, float y1, float x2, float y2)
        {
            float dx = x1 - x2, dy = y1 - y2; return dx * dx + dy * dy; 
        }
        public static float DisSqr3D (float x1, float y1, float z1, float x2, float y2, float z2)
        {
            float dx = x1 - x2, dy = y1 - y2, dz = z1 - z2; return dx * dx + dy * dy + dz * dz;
        }
        public static float DisEuc2D (float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(DisSqr2D(x1, y1, x2, y2));
        }
        public static float DisEuc3D (float x1, float y1, float z1, float x2, float y2, float z2)
        {
            return (float)Math.Sqrt(DisSqr3D(x1, y1, z1, x2, y2, z2)); 
        }
        public static float DisCheb2D(float x1, float y1, float x2, float y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }
        public static float DisCheb3D(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Max(Math.Abs(y1 - y2), Math.Abs(z1 - z2))); 
        }
        public static float Clamp(int value, int min, int max)
        {
            return value < min ? min : (value > max ? max : value); 

        }
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : (value > max ? max : value); 

        }
        public static float Clamp01(float v) {
            return Clamp(v, 0f, 1f); 
        }
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp01(t); 
        }
        public static float SmootherStep(float t)
        {
            t = Clamp01(t); return t * t * t * (t * (t * 6f - 15f) + 10f); 
        }
        public static float ExpDecay(float a, float b, float decay, float dt)
        {
            return b + (a - b) * (float)Math.Exp(-decay * dt); 
        }
        public static int Wrap(int v, int min, int max) 
        {
            int r = max - min; return min + ((v - min) % r + r) % r; 
        }
    }
}
