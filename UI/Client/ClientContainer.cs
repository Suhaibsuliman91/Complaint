using Microsoft.Extensions.Options;

namespace UI.Client
{
    public class ClientContainer : IClientContainer
    {
        public IUserClient User { get; private set; }
        public IComplaintClient Complaint { get; private set; }
        public IUserTypeClient UserType { get; private set; }

        public ClientContainer(HttpClient httpClient,IOptions<ServiceSetting> ServiceSetting, IHttpContextAccessor httpContext)
        {
            User = new UserClient(httpClient, ServiceSetting, httpContext);
            Complaint = new ComplaintClient(httpClient, ServiceSetting, httpContext);
            UserType = new UserTypeClient(httpClient, ServiceSetting, httpContext);
        }
    }
}
