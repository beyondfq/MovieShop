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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IPurchaseRepository purchaseRepository, IUserRepository userRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            if (await _purchaseRepository.CheckIfPurchaseExists(userId, purchaseRequest.MovieId))
                return true;
            return false;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var newPurchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                UserId = userId,
                TotalPrice = purchaseRequest.Price,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                PurchaseNumber = purchaseRequest.PurchaseNumber
            };

            var savedPurchase = await _purchaseRepository.AddPurchase(newPurchase);
            if (savedPurchase.Id > 1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetById(id);
            var movieCards = new List<MovieCardModel>();
            foreach (var purchase in purchases)
            {
                movieCards.Add(new MovieCardModel { Id = purchase.MovieId, PosterUrl = purchase.Movie.PosterUrl, Title = purchase.Movie.Title });
            }
            return movieCards;
        }

        public Task<PurchaseModel> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
