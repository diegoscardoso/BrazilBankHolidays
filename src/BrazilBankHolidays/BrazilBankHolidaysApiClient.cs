using System.Net;
using System.Text.Json;
using BrazilBankHolidays.Models;

namespace BrazilBankHolidays;

public class BrazilBankHolidaysApiException : Exception
{
    public BrazilBankHolidaysApiException(string message) : base(message) { }
}

public class BrazilBankHolidaysApiClient
{
    private readonly HttpClient _httpClient;

    public BrazilBankHolidaysApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Holiday>> GetHolidaysAsync(int year)
    {
        try
        {
            var url = $"https://brasilapi.com.br/api/feriados/v1/{year}";
            using var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new BrazilBankHolidaysApiException($"Não foi possível obter feriados para o ano {year}.");
            }

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var holidays = JsonSerializer.Deserialize<List<Holiday>>(json);
            return holidays ?? new List<Holiday>();
        }
        catch (BrazilBankHolidaysApiException)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            throw new BrazilBankHolidaysApiException("Falha de comunicação com a BrasilAPI. Verifique sua conexão e tente novamente.");
        }
        catch (TaskCanceledException)
        {
            throw new BrazilBankHolidaysApiException("A requisição para a BrasilAPI expirou (timeout).");
        }
        catch (JsonException)
        {
            throw new BrazilBankHolidaysApiException("Não foi possível interpretar a resposta da BrasilAPI.");
        }
    }
}
