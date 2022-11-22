using EmployeeArrivalApp.DTOs;

namespace EmployeeArrivalApp.DataAccess.Contracts
{
    public interface ITokenService
    {
        public Task AddTokenToDatabaseAsync(TokenDTO dto);
        public Task<bool> ValidateToken(string token);
    }
}
