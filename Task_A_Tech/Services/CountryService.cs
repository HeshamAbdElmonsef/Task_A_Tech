public class CountryService
{
    private readonly BlockedCountryRepository _repository;

    public CountryService(BlockedCountryRepository repository)
    {
        _repository = repository;
    }

    public void BlockCountry(string countryCode)
    {
        if (_repository.Contains(countryCode))
        {
            throw new InvalidOperationException("Country is already blocked.");
        }

        _repository.Add(countryCode);
    }

    public void UnblockCountry(string countryCode)
    {
        if (!_repository.Contains(countryCode))
        {
            throw new KeyNotFoundException("Country is not blocked.");
        }

        _repository.Remove(countryCode);
    }

    public List<string> GetBlockedCountries(int page, int pageSize)
    {
        return _repository.GetAll(page, pageSize);
    }
}