using MiniLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service.Interfaces
{
    public interface IBookService
    {
        Task<Book> AddAsync(Book author);

        Task<Book> EditAsync(Book author);

        Task<List<Book>> GetAllAsync();

        Task<Book> GetAsync(string id);
    }
}
