using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Repository
{
    public class MovieRepository: IMovieRepository
    {
        public Movie GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetTop30HighestRevenueMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 2, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 3, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 4, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 5, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 6, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 7, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 8, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 9, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 10, Title = "Avengers: Infinity War", Budget = 1200000}

            };

            return movies;
        }

        public List<Movie> GetTop30RatedMovies()
        {
            throw new NotImplementedException();
        }
    }
}
