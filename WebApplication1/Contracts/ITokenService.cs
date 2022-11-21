using WebApplication1.DTOs;

namespace WebApplication1.Contracts
{
    public interface ITokenService
    {
        public Task AddTokenToDatabaseAsync(TokenDTO dto);

        public Task<bool> ValidateToken(string token);
    }
}
