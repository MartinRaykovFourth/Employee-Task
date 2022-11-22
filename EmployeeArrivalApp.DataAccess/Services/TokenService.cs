using Microsoft.EntityFrameworkCore;
using EmployeeArrivalApp.Data;
using EmployeeArrivalApp.DTOs;
using EmployeeArrivalApp.DataAccess.Contracts;

namespace EmployeeArrivalApp.DataAccess.Services
{
    public class TokenService : ITokenService
    {
        private readonly MyContext _context;
        public TokenService(MyContext context)
        {
            _context = context;
        }

        public async Task AddTokenToDatabaseAsync(TokenDTO dto)
        {
            Token token = new Token()
            {
                Id = Guid.Parse(dto.Token),
                ExpirationDate = DateTime.Parse(dto.Expires)
            };

            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateToken(string token)
        {
            Guid tokenGuid = Guid.Parse(token);

            if (_context.Tokens.Any(t => t.Id == tokenGuid))
            {
                Token realToken = await _context.Tokens.SingleOrDefaultAsync(t => t.Id == tokenGuid);
                if (realToken != null && realToken.ExpirationDate > DateTime.Now)
                    return true;
            }
            return false;
        }
    }
}
