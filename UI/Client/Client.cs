using Azure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace UI.Client
{
    public class Client<T> : IClient<T> where T : class
    {
        private readonly HttpClient _httpClient;
        protected readonly string _ControllerName;
        protected readonly ServiceSetting _ServiceSetting;
        public Client(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
        {
            httpClient.BaseAddress = new Uri(serviceSetting.Value.ClientHost);

            this._httpClient = httpClient;
            _ServiceSetting = serviceSetting.Value;
            _ControllerName = typeof(T).Name;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_ControllerName}/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string objReponse = await response.Content.ReadAsStringAsync();
                IEnumerable<T> objResult = JsonConvert.DeserializeObject<IEnumerable<T>>(objReponse);
                return objResult;
            }
            return null;
        }

        public async Task<T> GetByID(int Id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_ControllerName}/GetByID?Id=" + Id);
            if (response.IsSuccessStatusCode)
            {
                var objReponse = await response.Content.ReadAsStringAsync();
                var objResult = JsonConvert.DeserializeObject<T>(objReponse);
                return objResult;
            }
            return default(T);
        }
        public async Task<DTO.Response> Insert(T objDto)
        {
            string result = String.Empty;
            JObject resultt = new JObject();

            StringContent content = new StringContent(JsonConvert.SerializeObject(objDto), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage Response = await _httpClient.PostAsync($"{_ControllerName}/Create", content);
            if (Response.IsSuccessStatusCode)
            {
                result = await Response.Content.ReadAsStringAsync();
                return new DTO.Response
                {
                    IsSuccess = true,
                    StatusCode = Response.StatusCode,
                    Result = JObject.Parse(result)
                };
            }
            else
            {
                result = await Response.Content.ReadAsStringAsync();

                DTO.Failed Failed = JsonConvert.DeserializeObject<DTO.Failed>(result);

                return new DTO.Response
                {
                    IsSuccess = false,
                    StatusCode = Response.StatusCode,
                    Result = Failed.Errors
                };
            }
        }
        public async Task<DTO.Response> Update(T objDto)
        {
            string result = String.Empty;
            JObject resultt = new JObject();

            StringContent content = new StringContent(JsonConvert.SerializeObject(objDto), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage Response = await _httpClient.PostAsync($"{_ControllerName}/Update", content);
            if (Response.IsSuccessStatusCode)
            {
                result = await Response.Content.ReadAsStringAsync();
                //return JsonConvert.DeserializeObject<DTO.Area>(result);
                return new DTO.Response
                {
                    IsSuccess = true,
                    StatusCode = Response.StatusCode,
                    Result = resultt
                };
            }
            else
            {
                result = await Response.Content.ReadAsStringAsync();

                DTO.Failed Failed = JsonConvert.DeserializeObject<DTO.Failed>(result);

                return new DTO.Response
                {
                    IsSuccess = false,
                    StatusCode = Response.StatusCode,
                    Result = Failed.Errors
                };
            }
        }
        public async Task<T> Delete(T objDto)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_ServiceSetting.ClientHost + $"{_ControllerName}/Delete"),
                Content = new StringContent(JsonConvert.SerializeObject(objDto), System.Text.Encoding.UTF8, "application/json")
            };
            
            HttpResponseMessage Response = await _httpClient.SendAsync(request);
            if (Response.IsSuccessStatusCode)
            {
                string result = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                return default(T);
            }
        }
    }
}
