using WebApplication1.DTOs;

namespace WebApplication1.Contracts
{
    public interface IHttpService
    {
      public Task<TokenDTO> GetSubscribtionAsync(string url);
    }
}
