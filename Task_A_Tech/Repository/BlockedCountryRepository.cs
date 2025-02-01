using System.Collections.Concurrent;
using Task_A_Tech.Models;

public class BlockedCountryRepository
{
    private readonly ConcurrentDictionary<string, Country> _blockedCountries;

    public BlockedCountryRepository(ConcurrentDictionary<string, Country> blockedCountries)
    {
        this._blockedCountries = blockedCountries;
    }

    public void Add(string countryCode)
    {
        _blockedCountries.TryAdd(countryCode, new Country { Code = countryCode, BlockedAt = DateTime.UtcNow });
    }

    public void Remove(string countryCode)
    {
        _blockedCountries.TryRemove(countryCode, out _);
    }

    public bool Contains(string countryCode)
    {
        return _blockedCountries.ContainsKey(countryCode);
    }

    public List<string> GetAll(int page, int pageSize)
    {
        return _blockedCountries.Keys
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}