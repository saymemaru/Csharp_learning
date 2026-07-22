using NLog;

namespace NLogTest
{
    public class Person
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public Person(string name)
        {
            Logger.Trace($"{name}出现了");
        }

        public void Walk()
        {
            Logger.Debug("person walk");
        }

    }
}
