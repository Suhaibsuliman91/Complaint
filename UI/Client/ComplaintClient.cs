using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Options;
using DTO;

namespace UI.Client
{
    public class ComplaintClient:Client<DTO.Complaint>,IComplaintClient
    {
        private readonly HttpClient _httpClient;
        readonly IOptions<ServiceSetting> _serviceSettings;
        public ComplaintClient(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
             : base(httpClient, serviceSetting, httpContext) 
        {
            _serviceSettings = serviceSetting;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_ServiceSetting.ClientHost);

        }
        public async Task<DTO.FileInfo> DownloadFile(string FileName)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            HttpResponseMessage response = await _httpClient.GetAsync($"{_ControllerName}/DownloadFile?FileName=" + FileName);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var fileInfo = JsonConvert.DeserializeObject<DTO.FileInfo>(result);
                return fileInfo;
            }
            return null;
        }

        public async Task<IEnumerable<Complaint>> GetAllForUser(int UserID)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Complaint/GetAllForUser?UserID={UserID}");
            if (response.IsSuccessStatusCode)
            {
                string objReponse = await response.Content.ReadAsStringAsync();
                IEnumerable<DTO.Complaint> objResult = JsonConvert.DeserializeObject<IEnumerable<DTO.Complaint>>(objReponse);
                return objResult;
            }
            return new List<DTO.Complaint>().AsEnumerable<DTO.Complaint>();
        }
    }
}
