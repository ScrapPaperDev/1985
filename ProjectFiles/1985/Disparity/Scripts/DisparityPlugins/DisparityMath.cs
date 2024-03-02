namespace Disparity.Plugins
{
    public class DisparityMath : IMathProvider
    {
        public float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
