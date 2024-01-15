using Microsoft.Extensions.Options;

namespace UI.Client
{
    public class UserClient:Client<DTO.User>, IUserClient
    {
        public UserClient(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
           : base(httpClient, serviceSetting, httpContext) { }
    }
}
