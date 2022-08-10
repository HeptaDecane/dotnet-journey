namespace Pipelines.Services;

public interface ILoggingService
{
    public void Log(LogLevel level, string message);
}

public class LoggingService : ILoggingService
{
    public void Log(LogLevel level, string message)
    {
        Console.WriteLine($"{level}: {message}");
    }
}