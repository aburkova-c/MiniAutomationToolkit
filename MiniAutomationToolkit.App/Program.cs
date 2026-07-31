using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;

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