using System.Net.Http;
using System.Threading.Tasks;
using Task_A_Tech.Models;

public class IpService
{
    private readonly HttpClient _httpClient;
    private readonly BlockedCountryRepository _blockedCountryRepository;
    private readonly BlockedAttemptLogRepository _logRepository;

    public IpService(HttpClient httpClient, BlockedCountryRepository blockedCountryRepository, BlockedAttemptLogRepository logRepository)
    {
        _httpClient = httpClient;
        _blockedCountryRepository = blockedCountryRepository;
        _logRepository = logRepository;
    }

    public async Task<string> LookupIp(string ipAddress)
    {
        string accessKey = "YOUR_ACCESS_KEY"; // Replace with your actual key
        var response = await _httpClient.GetAsync($"https://api.ipapi.com/api/{ipAddress}?access_key={accessKey}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> CheckIfIpIsBlocked(string ipAddress, string userAgent)
    {
        var countryCode = await GetCountryCode(ipAddress);
        var isBlocked = _blockedCountryRepository.Contains(countryCode);

        _logRepository.Add(new BlockedAttemptLog
        {
            IpAddress = ipAddress,
            Timestamp = DateTime.UtcNow,
            CountryCode = countryCode,
            IsBlocked = isBlocked,
            UserAgent = userAgent
        });

        return isBlocked;
    }

    private async Task<string> GetCountryCode(string ipAddress)
    {
        var response = await _httpClient.GetAsync($"https://ipapi.co/{ipAddress}/country_code/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}