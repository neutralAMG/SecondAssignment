
namespace SecondAssignment.Infraestructure.Utils.ILoggerConcrete
{
    public interface IConcreteLogger
    {
        void LogWargning(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogCritical(string message);
    }
}
