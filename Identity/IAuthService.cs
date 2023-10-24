namespace AD_Auth.Identity
{
    public interface IAuthService
    {
        Task<UserInformation> Login(string username, string password);
        Task<bool> Logout();
    }
}
