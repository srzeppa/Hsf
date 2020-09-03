using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hsf.ApplicatonProcess.August2020.Domain.Config;
using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hsf.ApplicatonProcess.August2020.Domain.Providers
{
    public interface ICountriesProvider
    {
        Task<bool> CheckIfCountryExists(string countryName);
    }

    public class CountriesProvider : ICountriesProvider
    {
        private readonly CountriesConfig _countriesConfig;
        private readonly HttpClient _httpClient;

        public CountriesProvider(IOptions<CountriesConfig> countriesConfigAccessor, HttpClient httpClient)
        {
            _countriesConfig = countriesConfigAccessor.Value;
            _httpClient = httpClient;
        }

        public async Task<bool> CheckIfCountryExists(string countryName)
        {
            var result = await _httpClient.GetAsync(string.Format(_countriesConfig.Endpoint, countryName));
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            var stringAsync = await result.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<CountryModel>>(stringAsync);
            return countries.Any();
        }
    }
}
