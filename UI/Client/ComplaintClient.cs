using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace UI.Client
{
    public class ComplaintClient:Client<DTO.Complaint>,IComplaintClient
    {
        private readonly HttpClient _httpClient;
        public ComplaintClient(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
             : base(httpClient, serviceSetting, httpContext) { }
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
    }
}
