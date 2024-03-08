using System.Text;
using System.Xml.Serialization;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyQuotes.Controllers;

[ApiController]
[Route("api/currency")]
public class CurrencyController : ControllerBase
{
    private readonly HttpClient _httpClient;

    private readonly string _connectToCBR;
    public CurrencyController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _connectToCBR = config.GetSection("CBRUrl").Value;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencies()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var request = await _httpClient.GetAsync(_connectToCBR+DateTime.Now.ToString("dd/MM/yyyy"));
        var response = new StringBuilder(await request.Content.ReadAsStringAsync());
        var str = response.Replace(',', '.').ToString();
        var xml = new XmlSerializer(typeof(ValCurs));
        using TextReader reader = new StringReader(str);
        var result = xml.Deserialize(reader) as ValCurs ?? new ValCurs();
        
        return Ok(result);
    }
    
    [HttpGet("names")]
    public async Task<IActionResult> GetNames()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var request = await _httpClient.GetAsync(_connectToCBR+DateTime.Now.ToString("dd/MM/yyyy"));
        var response = new StringBuilder(await request.Content.ReadAsStringAsync());
        var str = response.Replace(',', '.').ToString();
        var xml = new XmlSerializer(typeof(ValCurs));
        using TextReader reader = new StringReader(str);
        var result = xml.Deserialize(reader) as ValCurs ?? new ValCurs();
        var names = result.Valute.Select(x => (x.NumCode, x.Name));
        var list = new List<ValName>();
        foreach (var item in names)
        {
            list.Add(new ValName()
            {
                NumCode = item.NumCode,
                Name = item.Name
            });
        }
        return Ok(list);
    }
    
    [HttpGet("numcode")]
    public async Task<IActionResult> GetCurrencies([FromQuery]int numCode)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var request = await _httpClient.GetAsync(_connectToCBR+DateTime.Now.ToString("dd/MM/yyyy"));
        var response = new StringBuilder(await request.Content.ReadAsStringAsync());
        var str = response.Replace(',', '.').ToString();
        var xml = new XmlSerializer(typeof(ValCurs));
        using TextReader reader = new StringReader(str);
        var result = xml.Deserialize(reader) as ValCurs ?? new ValCurs();
        
        return Ok(result.Valute.FirstOrDefault(x => x.NumCode == numCode));
    }
}