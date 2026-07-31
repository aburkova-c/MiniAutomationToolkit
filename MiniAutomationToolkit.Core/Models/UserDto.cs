namespace MiniAutomationToolkit.Core.Models;

public record UserDto 
    // record: автоматически реализует сравнение объектов по значениям; поддерживает неизменяемость; удобно использовать для DTO.
{
    public string Name { get; } // после создания объекта нельзя изменить

    public string Email { get; }

    public UserDto(string name, string email) // имя не должно быть пустым: string.IsNullOrWhiteSpace
    {
        if (string.IsNullOrWhiteSpace(name)) // IsNullOrWhiteSpace метод возвращает true, если строка: null, "", "   ", "\t"
        {
            throw new ArgumentException(
                "Name cannot be empty.",
                nameof(name));
        }
        
        if (string.IsNullOrWhiteSpace(email)) // проверка email
        {
            throw new ArgumentException(
                $"Invalid email: {email}",
                nameof(email));
        }
        
        if (!email.Contains('@')) // email должен содержать @
        {
            throw new ArgumentException(
                $"Invalid email: {email}",
                nameof(email));
        }
        
        if (email.Contains(' ')) // в email не должно быть пробелов
        {
            throw new ArgumentException(
                $"Invalid email: {email}",
                nameof(email));
        }
        // Если все проверки прошли успешно, присвой значения свойствам
        Name = name;
        Email = email;

    }
}