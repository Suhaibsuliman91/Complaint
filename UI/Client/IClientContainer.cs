namespace UI.Client
{
    public interface IClientContainer
    {
        IUserClient User { get; }
        IComplaintClient Complaint { get; }
        IUserTypeClient UserType { get; }
    }
}
