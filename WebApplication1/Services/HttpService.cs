using WebApplication1.Contracts;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
         static HttpService()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept-Client", "Fourth-Monitor");

        }
        public async Task<TokenDTO> GetSubscribtionAsync(string url)
        {
            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var tokenDTO = await result.Content.ReadAsAsync<TokenDTO>();
                return tokenDTO;
            }
            return null;
        }
    }
}
