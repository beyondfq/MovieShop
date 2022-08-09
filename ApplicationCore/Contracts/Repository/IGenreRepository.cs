using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> Add(Genre genre);
    }
}
