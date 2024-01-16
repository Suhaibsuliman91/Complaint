namespace UI.Client
{
    public interface IUserClient:IClient<DTO.User>
    {
        public Task<DTO.User> Login(DTO.User User);
    }
}
