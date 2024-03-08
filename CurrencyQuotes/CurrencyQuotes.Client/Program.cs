using System.Net;
using System.Text.Json;
using DTO;

const string apiUrl = "https://localhost:7179";
const string separator = "-----------------------\n";

Console.WriteLine("Добро пожаловать!\n" +
                  "Выберите действие:\n" +
                  "1. Посмотреть все котировки валют\n" +
                  "2. Выбрать валюту\n" +
                  "...");

var answer = Console.ReadLine();

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

if (answer == "1")
{
    using var httpClient = new HttpClient();
    
    var response = await httpClient.GetAsync($"{apiUrl}/api/currency");
    
    var readAsString = await response.Content.ReadAsStringAsync();
    
    var result = JsonSerializer.Deserialize<ValCurs>(readAsString, options) ?? new ValCurs();
    
    Console.WriteLine(separator +
                      $"Данные актуальные на {result.Date}");
    
    foreach (var item in result.Valute)
    {
        Console.WriteLine($"ISO Цифр. код валюты: {item.NumCode}\n" +
                          $"ISO Букв. код валюты: {item.CharCode}\n" +
                          $"Номинал: {item.Nominal}\n" +
                          $"Название валюты: {item.Name}\n" +
                          $"Значение: {item.Value}\n" +
                          $"Курс за 1 единицу валюты: {item.VunitRate}\n" +
                          $"Внутренний уникальный код валюты: {item.ID}\n" +
                          separator);
    }
}
else if (answer == "2")
{
    using var httpClient = new HttpClient();
    
    var response = await httpClient.GetAsync($"{apiUrl}/api/currency/names");
    
    var readAsString = await response.Content.ReadAsStringAsync();
    
    var result = JsonSerializer.Deserialize<List<ValName>>(readAsString, options) ?? [];
    
    Console.WriteLine(separator);
    
    foreach (var item in result)
    {
        Console.WriteLine($"ISO Цифр. код валюты: {item.NumCode}\n" +
                          $"Название валюты: {item.Name}\n" +
                          separator);
    }
    
    Console.WriteLine("Выберите код валюты:\n" +
                      "...");
    
    var numCodeStr = Console.ReadLine();

    if (int.TryParse(numCodeStr, out int numCode))
    {
        var responseByNumCode = await httpClient.GetAsync($"{apiUrl}/api/currency/getByNumCode?numCode={numCode}");

        if (responseByNumCode.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine("Некорректный код!");
            return;
        }

        var readAsStringByNumCode = await responseByNumCode.Content.ReadAsStringAsync();

        var resultByNumCode = JsonSerializer.Deserialize<Valute>(readAsStringByNumCode, options) ?? new Valute();

        Console.WriteLine(separator);

        Console.WriteLine($"ISO Цифр. код валюты: {resultByNumCode.NumCode}\n" +
                          $"ISO Букв. код валюты: {resultByNumCode.CharCode}\n" +
                          $"Номинал: {resultByNumCode.Nominal}\n" +
                          $"Название валюты: {resultByNumCode.Name}\n" +
                          $"Значение: {resultByNumCode.Value}\n" +
                          $"Курс за 1 единицу валюты: {resultByNumCode.VunitRate}\n" +
                          $"Внутренний уникальный код валюты: {resultByNumCode.ID}\n" +
                          separator);
    }
    else
    {
        Console.WriteLine("Код валюты должен быть целым числом!");
    }
}