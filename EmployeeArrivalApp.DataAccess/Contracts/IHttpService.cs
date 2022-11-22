using EmployeeArrivalApp.DTOs;

namespace EmployeeArrivalApp.DataAccess.Contracts
{
    public interface IHttpService
    {
      public Task<TokenDTO> GetSubscribtionAsync(string url);
    }
}
