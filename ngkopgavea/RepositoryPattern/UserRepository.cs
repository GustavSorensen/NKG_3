using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ngkopgavea.Models;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace ngkopgavea.RepositoryPattern
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Get(string username);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(DatabaseContext context) : base(context)
        {
        }
        public async Task<User> Get(string username)
        {
            try 
            {
                var user = await Context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (user != null)
                {
                    return user; //Der skal da returneres username tilbage, ikke?
                }
            }
            catch
            {

            }
            return null;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Get(username);
            if(user == null)
            {
                return null;
            }
            bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            // return null if user not found
            if (!validPassword)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Gustav-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}