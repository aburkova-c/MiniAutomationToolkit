using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Repositories;

public static class ProductRepository
{
    public static List<Product> LoadFromCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var products = new List<Product>();

        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var lineNumber = i + 1;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(';');

            if (parts.Length != 3)
            {
                throw new InvalidDataException(
                    $"Invalid product data at line {lineNumber}.");
            }

            var name = parts[0].Trim();
            var priceText = parts[1].Trim();
            var categoryText = parts[2].Trim();

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(priceText)
                || string.IsNullOrWhiteSpace(categoryText))
            {
                throw new InvalidDataException(
                    $"Invalid product data at line {lineNumber}.");
            }

            if (!decimal.TryParse(priceText, out var price))
            {
                throw new InvalidDataException(
                    $"Invalid product data at line {lineNumber}.");
            }

            if (price < 0)
            {
                throw new InvalidDataException(
                    $"Invalid product data at line {lineNumber}.");
            }

            if (!Enum.TryParse<ProductCategory>(
                    categoryText,
                    true,
                    out var category))
            {
                throw new InvalidDataException(
                    $"Invalid product data at line {lineNumber}.");
            }

            products.Add(
                new Product(
                    name,
                    price,
                    category));
        }

        return products;
    }

    public static List<string> GetAffordableProducts(
        IEnumerable<Product> products,
        ProductCategory category,
        decimal maxPrice)
    {
        return products
            .Where(product => product.Category == category)
            .Where(product => product.Price < maxPrice)
            .OrderBy(product => product.Price)
            .ThenBy(product => product.Name)
            .Select(product => product.Name)
            .ToList();
    }
}