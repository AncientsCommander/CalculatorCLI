using System.Data;
using System.Text.RegularExpressions;

double result;
const string sqrtOperator = "sqrt";

while (true)
{
    Console.WriteLine("Введите формулу");
    var input = Console.ReadLine()?.Replace(" ", "");

    var isDigits = input is not null && Regex.IsMatch(input, @"^(sqrt|\d+|\+|-|\*|/|%|\(|\)|%|\.|,)+$");

    if (!isDigits || input is null)
    {
        Console.WriteLine("Формула содержит недопустимые символы. Проверьте формулу и повторите");
        continue;
    }

    try
    {
        var isSqrt = input.Contains(sqrtOperator);

        if (isSqrt) input = input.Replace(sqrtOperator, "");
        if (input.Contains('%')) input = input.Replace("%", "*0.01");

        result = double.Parse(new DataTable().Compute(input.Replace(',', '.'), string.Empty).ToString()!);

        if (double.IsInfinity(result)) throw new ArgumentException("На 0 делить нельзя. Проверьте формулу и повторите");

        if (isSqrt) result = Math.Sqrt(result);
        if (result is double.NaN) throw new Exception();
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