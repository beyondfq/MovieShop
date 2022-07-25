using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository
{
    public class CastRepository: ICastRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public CastRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Cast> GetById(int id)
        {
            var cast = await _movieShopDbContext.Casts
                .Include(c => c.MoviesOfCast).ThenInclude(c => c.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);
            return cast;
        }
    }
}
