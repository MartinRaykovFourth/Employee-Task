using WebApplication1.DTOs;

namespace WebApplication1.Contracts
{
    public interface IArrivalService
    {
        public Task AddToDatabaseAsync(List<EmployeeArrivalDTO> dtos);
        public Task<List<EmployeeArrivalFullInfoDTO>> GetAllAsync();
    }
}
