using MiniLibrary.Entity;
using MiniLibrary.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service
{
    public class LocationService : ApiLocationBaseService, ILocationService
    {
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var countryList = await GetAsync<CountryList>("iso");

            return countryList?.Data;
        }

        public async Task<List<string>> GetAllStateCities(string countryName, string stateName)
        {
            var cityData = await PostAsync<CityList>("state/cities", new { country = countryName, state = stateName });

            return cityData?.Data;
        }

        public async Task<List<State>> GetAllStatesAsync(string countryName)
        {
            var countryData = await PostAsync<CountryData>("states", new { country = countryName });

            return countryData?.Data?.States;
        }
    }
}
