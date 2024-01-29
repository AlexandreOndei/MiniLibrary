using MiniLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> AddAsync(Author author);

        Task<Author> EditAsync(Author author);

        Task<List<Author>> GetAllAsync();

        Task<Author> GetAsync(string id);
    }
}
