using EmployeeArrivalApp.DataAccess.Contracts;
using EmployeeArrivalApp.DTOs;

namespace EmployeeArrivalApp.DataAccess.Services
{
    public class SubscribtionService : ISubscriptionService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<TokenDTO> GetSubscribtionAsync(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Add("Accept-Client", "Fourth-Monitor");
                var result = await _httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var tokenDTO = await result.Content.ReadAsAsync<TokenDTO>();
                    return tokenDTO;
                }
            }
       
            return null;
        }
    }
}
