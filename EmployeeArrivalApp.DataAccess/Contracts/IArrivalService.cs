using EmployeeArrivalApp.DTOs;

namespace EmployeeArrivalApp.DataAccess.Contracts
{
    public interface IArrivalService
    {
        public Task AddToDatabaseAsync(List<EmployeeArrivalDTO> dtos);
        public Task<List<EmployeeArrivalFullInfoDTO>> GetAllAsync();
    }
}
