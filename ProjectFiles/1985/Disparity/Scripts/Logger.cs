using System;

namespace Disparity
{
    public interface ILogger
    {
        void Log(object msg);
    }

    public class DefaultLogger : ILogger
    {
        public void Log(object msg)
        {
            Console.WriteLine(msg);
        }
    }

    public static class Logger
    {
        public static ILogger logger;

        public static void Log(object msg)
        {
            logger.Log(msg);
        }
    }
}