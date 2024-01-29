using MiniLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service.Interfaces
{
    public interface ILocationService
    {
        Task<List<Country>> GetAllCountriesAsync();

        Task<List<State>> GetAllStatesAsync(string contryName);

        Task<List<string>> GetAllStateCities(string countryName, string stateName);
    }
}
