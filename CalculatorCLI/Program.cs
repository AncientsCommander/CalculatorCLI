using System.Data;
using System.Text.RegularExpressions;

double result;

while (true)
{
    Console.WriteLine("Введите формулу");
    var input = Console.ReadLine()?.Replace(" ", "");
    
    var isDigits = input is not null && Regex.IsMatch(input, @"^[\d+\-*/()%]+$");

    if (!isDigits)
    {
        Console.WriteLine("Формула содержит недопустимые символы. Проверьте формулу и повторите");
        continue;
    }

    try
    {
        result = double.Parse(new DataTable().Compute(input, string.Empty).ToString()!);
        
        if (double.IsInfinity(result)) throw new ArgumentException("На 0 делить нельзя. Проверьте формулу и повторите");
    }
    catch (ArgumentException exception)
    {
        Console.WriteLine(exception.Message);
        continue;
    }
    catch (Exception)
    {
        Console.WriteLine("Формула содержит ошибку. Проверьте формулу и повторите");
        continue;
    }

    break;
}

Console.WriteLine($"Результат функции: {result}");
Console.WriteLine("Нажмите любую кнопку чтобы выйти");
Console.ReadLine();