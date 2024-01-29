using MiniLibrary.Entity;
using MiniLibrary.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service
{
    public class BookService : ApiBaseService, IBookService
    {
        private const string PATH = "Book";

        public async Task<Book> AddAsync(Book book)
        {
            return await PostAsync<Book>($"{PATH}/Add", book);
        }

        public async Task<Book> EditAsync(Book book)
        {
            return await PostAsync<Book>($"{PATH}/Edit", book);
        }

        public async Task<Book> GetAsync(string id)
        {
            return await GetAsync<Book>($"{PATH}/Get/{id}");
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await GetAsync<List<Book>>($"{PATH}/GetAll");
        }
    }
}
