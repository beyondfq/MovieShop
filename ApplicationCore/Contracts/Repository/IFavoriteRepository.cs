using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetById(int userId);
        Task<Favorite> FavoriteAdd(Favorite favorite);
        Task<bool> CheckIfFavoriteExists(int userId, int movieId);
    }
}
