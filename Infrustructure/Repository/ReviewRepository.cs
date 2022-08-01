using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public ReviewRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public Task<bool> CheckIfReviewExists(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetById(int userId, int movieId)
        {
            var review = await _movieShopDbContext.Reviews
                .Where(r => r.UserId == userId && r.MovieId == movieId)
                .FirstOrDefaultAsync();
            return review;
        }

        public Task<Review> ReviewRemove(Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> ReviewUpdate(Review review)
        {
            _movieShopDbContext.Reviews.Add(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review> ReviewAdd(Review review)
        {
            _movieShopDbContext.Reviews.Add(review);
            await _movieShopDbContext.SaveChangesAsync();
            return review;
        }
    }
}
