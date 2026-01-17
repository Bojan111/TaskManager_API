using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);
        string GenerateToken(string username);
        User? GetByUsername(string username);
        User RegisterUser(string username, string password);
    }
}
