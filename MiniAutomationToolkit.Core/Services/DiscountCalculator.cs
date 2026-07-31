using MiniAutomationToolkit.Core.Models;
namespace MiniAutomationToolkit.Core.Services;

public class DiscountCalculator
{
    public static decimal CalculateDiscount(decimal orderAmount, ClientType clientType)
    {
        if (orderAmount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(orderAmount),
                orderAmount,
                "Order amount cannot be negative. ");
        }

        return clientType switch
        {
            ClientType.Vip => orderAmount * 0.15m,

            ClientType.Premium when orderAmount > 1000m => orderAmount * 0.10m,
            ClientType.Premium => orderAmount * 0.05m,

            ClientType.Regular when orderAmount > 1000m => orderAmount * 0.05m,
            ClientType.Regular => 0m,

            _ => throw new ArgumentOutOfRangeException(
                nameof(clientType),
                clientType,
                "Unsupported client type.")
        };
    }
}