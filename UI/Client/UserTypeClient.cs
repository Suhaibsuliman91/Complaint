using Microsoft.Extensions.Options;

namespace UI.Client
{
    public class UserTypeClient:Client<DTO.UserType>, IUserTypeClient
    {
        public UserTypeClient(HttpClient httpClient, IOptions<ServiceSetting> serviceSetting, IHttpContextAccessor httpContext)
      : base(httpClient, serviceSetting, httpContext) { }
    }
}
