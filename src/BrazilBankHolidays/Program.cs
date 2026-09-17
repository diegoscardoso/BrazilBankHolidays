using System.Globalization;
using BrazilBankHolidays;

const int MinimumYear = 1900;

string? input = args.Length > 0 ? args[0] : null;

if (input is null)
{
    Console.WriteLine("Uso: dotnet run -- <ano>");
    Console.Write("Informe o ano desejado: ");
    input = Console.ReadLine();
}

if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out int year) || input.Trim().Length != 4)
{
    Console.Error.WriteLine("Erro: o ano informado é inválido. Utilize um número inteiro com 4 dígitos (ex.: 2026).");
    return 1;
}

if (year < MinimumYear)
{
    Console.Error.WriteLine($"Erro: o ano deve ser maior ou igual a {MinimumYear}.");
    return 1;
}

using var httpClient = new HttpClient();
var client = new BrazilBankHolidaysApiClient(httpClient);

try
{
    var holidays = await client.GetHolidaysAsync(year);
    var sortedHolidays = holidays.OrderBy(h => h.Date).ToList();

    foreach (var holiday in sortedHolidays)
    {
        var date = DateTime.ParseExact(holiday.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Console.WriteLine($"{date:dd/MM/yyyy} - {holiday.Name}");
    }

    Console.WriteLine();
    Console.WriteLine($"Total de feriados: {sortedHolidays.Count}");
    return 0;
}
catch (BrazilBankHolidaysApiException ex)
{
    Console.Error.WriteLine($"Erro: {ex.Message}");
    return 1;
}
