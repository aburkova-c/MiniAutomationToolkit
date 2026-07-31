using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using MiniAutomationToolkit.Core.Helpers; 

var testCases = new[]
{
    (ClientType: ClientType.Vip, OrderAmount: 500m),
    (ClientType: ClientType.Vip, OrderAmount: 2000m),
    (ClientType: ClientType.Premium, OrderAmount: 800m),
    (ClientType: ClientType.Premium, OrderAmount: 1000m),
    (ClientType: ClientType.Premium, OrderAmount: 1500m),
    (ClientType: ClientType.Regular, OrderAmount: 500m),
    (ClientType: ClientType.Regular, OrderAmount: 1500m),
    (ClientType: ClientType.Regular, OrderAmount: 1000m)
};

foreach (var testCase in testCases)
{
    var discount = DiscountCalculator.CalculateDiscount(
        testCase.OrderAmount,
        testCase.ClientType);

    Console.WriteLine(
        $"Client: {testCase.ClientType}, amount: {testCase.OrderAmount}, discount: {discount}");
}

try
{
    DiscountCalculator.CalculateDiscount(
        -100m,
        ClientType.Regular);
}
catch (ArgumentOutOfRangeException exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}

var fileNames = new List<string>
{
    "error_2024.log",
    "notes.txt",
    "screen_001.png",
    "debug.txt",
    "application.log",
    "report.txt",
    "SCREEN_002.PNG",
    "trace.log",
    "readme.txt",
    "screen_003.PnG",
    "server.log",
    "config.txt",
    "screen_004.png",
    "errors.log",
    "manual.txt",
    "screen_005.PNG",
    "audit.log",
    "todo.txt",
    "screen_006.png",
    "system.log"
};

var firstScreenshot = FileSearcher.FindFirstScreenshot(fileNames);
Console.WriteLine($"First screenshot: {firstScreenshot}");

var fileNamesWithoutScreenshots = new List<string>
{
    "error.log",
    "debug.txt",
    "report.docx",
    "application.log",
    "notes.txt"
};

try
{
    var screenshot = FileSearcher.FindFirstScreenshot(fileNamesWithoutScreenshots);
    Console.WriteLine($"First screenshot: {screenshot}");
}
catch (FileNotFoundException exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}
