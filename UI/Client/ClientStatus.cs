using Microsoft.Extensions.Options;

namespace UI.Client
{
    public class ClientStatus:Client<DTO.Status>, IClientStatus
    {
        public ClientStatus(HttpClient httpClient, IOptions<ServiceSetting> options, IHttpContextAccessor httpContext):base(httpClient, options, httpContext)
        {
            
        }
    }
}
