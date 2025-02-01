using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/countries")]
public class CountriesController : ControllerBase
{
    private readonly CountryService _countryService;

    public CountriesController(CountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpPost("block")]
    public IActionResult BlockCountry([FromBody] string countryCode)
    {
        _countryService.BlockCountry(countryCode);
        return Ok($"Country {countryCode} blocked successfully.");
    }

    [HttpDelete("block/{countryCode}")]
    public IActionResult UnblockCountry(string countryCode)
    {
        _countryService.UnblockCountry(countryCode);
        return Ok($"Country {countryCode} unblocked successfully.");
    }

    [HttpGet("blocked")]
    public IActionResult GetBlockedCountries([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var countries = _countryService.GetBlockedCountries(page, pageSize);
        return Ok(countries);
    }
}