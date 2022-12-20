using EmployeeArrivalApp.DTOs;

namespace EmployeeArrivalApp.DataAccess.Contracts
{
    public interface ISubscriptionService
    {
      public Task<TokenDTO> GetSubscribtionAsync(string url);
    }
}
