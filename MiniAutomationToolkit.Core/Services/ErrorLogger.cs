namespace MiniAutomationToolkit.Core.Services;

public class ErrorLogger
{
    public string? TryReadFile(
        string sourceFilePath,
        string logFilePath)
    {
        try
        {
            return File.ReadAllText(sourceFilePath);
        }
        catch (FileNotFoundException exception)
        {
            LogError(logFilePath, exception);
            return null;
        }
        catch (UnauthorizedAccessException exception)
        {
            LogError(logFilePath, exception);
            return null;
        }
    }

    private static void LogError(
        string logFilePath,
        Exception exception)
    {
        var logEntry =
            $"{DateTime.Now} | {exception.GetType().Name} | {exception.Message}{Environment.NewLine}";

        File.AppendAllText(logFilePath, logEntry);
    }
}