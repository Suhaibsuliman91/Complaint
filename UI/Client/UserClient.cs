using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

namespace UI.Client
{
    public class UserClient:Client<DTO.User>, IUserClient
    {
        readonly HttpClient _httpClient;
        public IOptions<ServiceSetting> _service;

        public UserClient(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
           : base(httpClient, serviceSetting, httpContext) 
        {
            _service = serviceSetting;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_service.Value.ClientHost);
        }

        public async Task<DTO.User> Login(DTO.User User)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"User/Login?Email={User.Email}&Password={User.Password}");
            if (response.IsSuccessStatusCode)
            {
                var objReponse = await response.Content.ReadAsStringAsync();
                var objResult = JsonConvert.DeserializeObject<DTO.User>(objReponse);
                return objResult;
            }
            return default(DTO.User);
        }

    }
}
