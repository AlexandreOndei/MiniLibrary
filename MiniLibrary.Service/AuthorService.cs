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
    public class AuthorService : ApiBaseService, IAuthorService
    {
        private const string PATH = "Author";

        public async Task<Author> AddAsync(Author author)
        {
            return await PostAsync<Author>($"{PATH}/Add", author);
        }

        public async Task<Author> EditAsync(Author author)
        {
            return await PostAsync<Author>($"{PATH}/Edit", author);
        }

        public async Task<Author> GetAsync(string id)
        {
            return await GetAsync<Author>($"{PATH}/Get/{id}");
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await GetAsync<List<Author>>($"{PATH}/GetAll");
        }
    }
}
