using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface ITokenService
    {
        Task<Token> CreateToken(Token newToken);
        bool UseTokenAsync(string tokenString);
        Task<List<Token>> GetAllElements();

    }
}
