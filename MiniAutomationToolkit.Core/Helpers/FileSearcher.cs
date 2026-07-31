using System.IO;

namespace MiniAutomationToolkit.Core.Helpers;

public static class FileSearcher
{
    public static string FindFirstScreenshot(List<string> fileNames)
    {
        var screenshots = fileNames.Where(fileName =>
            fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase));
        
        if (!screenshots.Any())
        {
            throw new FileNotFoundException("No screenshots found in the provided list.");
        }
        return screenshots.FirstOrDefault();
    }
}