using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using MiniAutomationToolkit.Core.Helpers;
using MiniAutomationToolkit.Core.Pages;
using MiniAutomationToolkit.Core.Configuration;
using MiniAutomationToolkit.Core.Extensions;
using System.Diagnostics;
using MiniAutomationToolkit.Core.Simulations;
using MiniAutomationToolkit.Core.Services;
using MiniAutomationToolkit.Core.Validation;
using MiniAutomationToolkit.Core.Repositories;

Console.WriteLine("=== Task 2 ===");

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

Console.WriteLine();
Console.WriteLine("=== Task 3 ===");

var fileNames = new List<string> // тестовый набор: имена с разным регистром расширения 
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
Console.WriteLine($"First screenshot: {firstScreenshot}"); // вывод результата первый скриншот

var fileNamesWithoutScreenshots = new List<string>
{
    "error.log",
    "debug.txt",
    "report.docx",
    "application.log",
    "notes.txt"
};

try // демонстрирует обработку FileNotFoundException
{
    var screenshot = FileSearcher.FindFirstScreenshot(fileNamesWithoutScreenshots);
    Console.WriteLine($"First screenshot: {screenshot}");
}
catch (FileNotFoundException exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Task 4 ===");

var user = new UserDto(
    "Alex Smith",
    "alex@example.com");

var sameUser = new UserDto(
    "Alex Smith",
    "alex@example.com");

// Эти строки не компилируются, потому что свойства неизменяемые:
// user.Name = "John Smith";
// user.Email = "john@example.com";

Console.WriteLine($"User created: {user.Name}, {user.Email}"); // Проверка создания пользователя
Console.WriteLine($"Users are equal: {user == sameUser}");    // Проверить равенство двух RECORD

var invalidUsers = new List<(string Name, string Email)> // ошибочные сценарии
{
    ("", "alex@example.com"),
    ("Alex Smith", ""),
    ("Alex Smith", "alexexample.com"),
    ("Alex Smith", "alex @example.com")
};

foreach (var invalidUser in invalidUsers) // обработка каждого сценария
{
    try
    {
        var invalidUserDto = new UserDto(
            invalidUser.Name,
            invalidUser.Email);

        Console.WriteLine(
            $"User created: {invalidUserDto.Name}, {invalidUserDto.Email}");
    }
    catch (ArgumentException exception)
    {
        Console.WriteLine($"Error: {exception.Message}");
    }
}

Console.WriteLine();
Console.WriteLine("=== Task 5 ===");

var pages = new List<BasePage> // создан список страниц
{
    new LoginPage(),
    new HomePage()
};
foreach (var page in pages)
{
    page.Load();
}

// Проверить уникальность URL
var hasDuplicateUrls = pages
    .GroupBy(page => page.Url)
    .Any(group => group.Count() > 1);

if (hasDuplicateUrls)
{
    throw new InvalidOperationException(
        "Duplicate page URLs found.");
}

Console.WriteLine("All page URLs are unique");

Console.WriteLine();
Console.WriteLine("=== Task 6 ===");

// Получить путь к файлу
var configPath = Path.Combine(
    AppContext.BaseDirectory,
    "data",
    "appsettings.txt");
    
var config = new AppConfig(configPath);

// Получить параметры в нужных типах
var baseUrl = config.GetSetting<string>("baseUrl");
var timeout = config.GetSetting<int>("timeout");
var headless = config.GetSetting<bool>("headless");
var retryCount = config.GetSetting<int>("retryCount");

Console.WriteLine($"Base URL: {baseUrl}");
Console.WriteLine($"Timeout: {timeout}");
Console.WriteLine($"Headless: {headless}");
Console.WriteLine($"Retry count: {retryCount}");

// Обработать отсутствующий ключ
try
{
    var browser = config.GetSetting<string>("browser");
    Console.WriteLine($"Browser: {browser}");
}
catch (KeyNotFoundException exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Task 7 ===");

string?[] urls =
{
    "https://google.com",
    "http://example.org",
    "ftp://files.example.com",
    null,
    "HTTPS://SITE.EXAMPLE.COM"
};

foreach (var url in urls)
{
    var result = url.HasHttpScheme();

    Console.WriteLine(
        $"Input: {url ?? "<null>"}, Has HTTP scheme: {result}");
}

Console.WriteLine();
Console.WriteLine("=== Task 8 ===");

var simulator = new LongOperationSimulator();
var stopwatch = Stopwatch.StartNew();
var operationResult = await simulator.LongOperationAsync();

stopwatch.Stop();

Console.WriteLine($"Result: {operationResult}");
Console.WriteLine($"Duration: {stopwatch.Elapsed.TotalSeconds:F2} seconds");

Console.WriteLine();
Console.WriteLine("=== Task 9 ===");

var errorLogger = new ErrorLogger();

var dataDirectory = Path.Combine(
    AppContext.BaseDirectory,
    "data");

var inputFilePath = Path.Combine(
    dataDirectory,
    "input.txt");

var missingFilePath = Path.Combine(
    dataDirectory,
    "missing.txt");

var logFilePath = Path.Combine(
    dataDirectory,
    "errors.log");

// Сценарий с существующим файлом
var fileContent = errorLogger.TryReadFile(
    inputFilePath,
    logFilePath);

if (fileContent is not null)
{
    Console.WriteLine("Existing file content:");
    Console.WriteLine(fileContent);
}

// Сценарий с отсутствующим файлом
var missingFileContent = errorLogger.TryReadFile(
    missingFilePath,
    logFilePath);

if (missingFileContent is null)
{
    Console.WriteLine("Missing file could not be read.");
}
// Вывести лог
Console.WriteLine("Error log:");
var logContent = File.ReadAllText(logFilePath);
Console.WriteLine(logContent);

Console.WriteLine();
Console.WriteLine("=== Task 10 ===");

var numbersToValidate = new[] { 5, -5, 0 };

foreach (var number in numbersToValidate)
{
    try
    {
        Guard.EnsurePositive(number);
        Console.WriteLine($"Value {number}: validation passed.");
    }
    catch (ValidationException exception)
    {
        Console.WriteLine(exception.Message);
    }
}

Console.WriteLine();
Console.WriteLine("=== Task 11 ===");

var productsFilePath = Path.Combine(
    AppContext.BaseDirectory,
    "data",
    "products.csv");

var products = ProductRepository.LoadFromCsv(
    productsFilePath);

Console.WriteLine($"Loaded products: {products.Count}");
// Бюджет 10
var foodUnderTen =
    ProductRepository.GetAffordableProducts(
        products,
        ProductCategory.Food,
        10m);

Console.WriteLine("Food under 10:");

if (foodUnderTen.Any())
{
    foreach (var productName in foodUnderTen)
    {
        Console.WriteLine(productName);
    }
}
else
{
    Console.WriteLine("No products found");
}

// бюджет 1

var foodUnderOne =
    ProductRepository.GetAffordableProducts(
        products,
        ProductCategory.Food,
        1m);

Console.WriteLine("Food under 1:");

if (foodUnderOne.Any())
{
    foreach (var productName in foodUnderOne)
    {
        Console.WriteLine(productName);
    }
}
else
{
    Console.WriteLine("No products found");
}

