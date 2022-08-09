using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<bool> AddGenre(GenreModel genre)
        {
            var newGenre = new Genre
            {
                Name = genre.Name,
                Id = genre.Id
            };
            var addedGenre = await _genreRepository.Add(newGenre);
            if (addedGenre.Id > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllGenres();

            var genresModels = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name }).ToList();

            return genresModels;
        }
    }
}
