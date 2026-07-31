namespace MiniAutomationToolkit.Core.Configuration;

public class AppConfig
{
    private readonly Dictionary<string, string> _settings = new();

    public AppConfig(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) // Пропуск пустых строк
            {
                continue;
            }

            if (line.TrimStart().StartsWith("#")) // Пропуск комментариев
            {
                continue;
            }

            var parts = line.Split('=', 2); // Разделение только по первому =

            if (parts.Length != 2)
            {
                throw new InvalidDataException(
                    $"Invalid configuration line: {line}");
            }

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(key)) 
            {
                throw new InvalidDataException(
                    $"Configuration key cannot be empty: {line}");
            }

            if (!_settings.TryAdd(key, value)) // Проверка повторяющихся ключей
            {
                throw new InvalidDataException(
                    $"Duplicate configuration key: {key}");
            }
        }
    }

    public T GetSetting<T>(string key)
    {
        if (!_settings.TryGetValue(key, out var value))
        {
            throw new KeyNotFoundException($"Configuration key not found: {key}");
        }

        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception exception)
            when (exception is FormatException
                      or InvalidCastException
                      or OverflowException)
        {
            throw new InvalidDataException($"Cannot convert setting '{key}' to type {typeof(T).Name}.", exception);
        }
    }
}