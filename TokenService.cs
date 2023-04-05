using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories.Contracts;
using Assignment2.BLL.Services.Contracts;


namespace Assignment2.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IGenericRepository<Token> _repository;

        public TokenService(IGenericRepository<Token> repository)
        {
            _repository = repository;
        }

        public async Task<Token> CreateToken(Token newToken)
        {
            try
            {
                return await _repository.CreateToken(newToken);
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteToken(int tokenId)
        {
            await _repository.DeleteToken(tokenId);
        }

        public bool UseTokenAsync(string tokenString)
        {
            var token = _repository.GetTokenByString(tokenString);

            if (token == null)
            {
                return false;
            }

            DeleteToken(token.TokenId);

            return true;
        }

        public async Task<List<Token>> GetAllElements()
        {
            try
            {
                return await _repository.GetAllElements();
            }
            catch
            {
                throw;
            }
        }

    }
}
