using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Entity;
using MiniLibrary.Models;
using MiniLibrary.Service;
using MiniLibrary.Service.Interfaces;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace MiniLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService _authorService;
        private ILocationService _locationService;

        public AuthorController(IAuthorService authorService, ILocationService locationService)
        {
            _authorService = authorService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Index()
        {
            List<Author> authorList = await _authorService.GetAllAsync();

            return View(authorList);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Countries = await _locationService.GetAllCountriesAsync();

            ViewBag.States = new List<State>();

            ViewBag.Cities = new List<string>();

            return View("Cadaster");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Author author)
        {
            ResponseViewModel response = new();

            try
            {
                author.Id = string.Empty;
                Author addedAuthor = await _authorService.AddAsync(author);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return Json(response);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Author author = await _authorService.GetAsync(id);

            var countries = await _locationService.GetAllCountriesAsync();
            ViewBag.Countries = countries;

            Country country = author.Country?.Length == 3 ? countries.FirstOrDefault(c => c.Iso3.Equals(author?.Country)) : null;

            var states = await _locationService.GetAllStatesAsync(country != null ? country?.Name : author.Country) ?? new List<State>();
            ViewBag.States = states;

            State state = author.State?.Length <= 2 ? states.FirstOrDefault(c => c.State_code.Equals(author.State)) : null;

            ViewBag.Cities = await _locationService.GetAllStateCities(
                country != null ? country?.Name : author.Country,
                state != null ? state?.Name : author.State) ?? new List<string>();

            return View("Cadaster", author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            ResponseViewModel response = new();

            try
            {
                Author addedAuthor = await _authorService.EditAsync(author);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return Json(response);
        }

        public async Task<IActionResult> States(string countryName, string state)
        {
            var countries = await _locationService.GetAllCountriesAsync();

            Country country = countryName?.Length == 3 ? countries.FirstOrDefault(c => c.Iso3.Equals(countryName)) : null;

            List<State> states = await _locationService.GetAllStatesAsync(country != null ? country?.Name : countryName) ?? new List<State>();

            ViewBag.State = state;

            return View("_States", states);
        }

        public async Task<IActionResult> Cities(string countryName, string stateName, string city)
        {
            var countries = await _locationService.GetAllCountriesAsync();

            Country country = countryName?.Length == 3 ? countries.FirstOrDefault(c => c.Iso3.Equals(countryName)) : null;

            List<string> cities = await _locationService.GetAllStateCities(country != null ? country?.Name : countryName, stateName) ?? new List<string>();

            ViewBag.City = city;

            return View("_Cities", cities);
        }
    }
}
