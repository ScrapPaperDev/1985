namespace Disparity
{
    public class MathAdapter
    {
        private static IMathProvider math;

        public MathAdapter(IMathProvider m)
        {
            math = m;
        }

        public static float Lerp(float a, float b, float t)
        {
            return math.Lerp(a, b, t);
        }
    }

    public interface IMathProvider
    {
        float Lerp(float a, float b, float t);
    }


}