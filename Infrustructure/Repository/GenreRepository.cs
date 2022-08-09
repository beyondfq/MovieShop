using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using ApplicationCore.Models;

namespace Infrastructure.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public GenreRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Genre> Add(Genre genre)
        {

            _movieShopDbContext.Genres.Add(genre);
            await _movieShopDbContext.SaveChangesAsync();
            return genre;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _movieShopDbContext.Genres.ToListAsync();
            return genres;
        }
    }
}
