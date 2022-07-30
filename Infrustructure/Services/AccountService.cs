using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ApplicationCore.Entities;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUserRepository _UserRepository;
        public AccountService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<bool> CreateUser(UserRegisterModel model)
        {
            // step 1: check if the email exists in database
            var user = await _UserRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                throw new Exception("Email already exists, please try to login");
            }

            // create a unique salt and hash the password with salt
            var salt = GetRandomSalt();

            var hashedPassword = GetHashedPasswordWithSalt(model.Password, salt);

            // save the User into User Table using user Repository
            var dbUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var savedUser = await _UserRepository.AddUser(dbUser);
            if (savedUser.Id > 0)
            {
                return true;
            }
            return false;
        }
        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPasswordWithSalt(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            Convert.FromBase64String(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8));
            return hashed;
        }

        public async Task<UserInfoResponseModel> ValidateUser(UserLoginModel model)
        {
            var dbUser = await _UserRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                throw new Exception("Please register first");
            }

            var hashedPassword = GetHashedPasswordWithSalt(model.Password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                return new UserInfoResponseModel { Id = dbUser.Id, Email = dbUser.Email, FirstName = dbUser.FirstName, LastName = dbUser.LastName };
            }
            return null;
        }
    }
}
