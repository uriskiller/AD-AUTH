using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace AD_Auth.Identity
{
    public class AuthService : LdapProvider, IAuthService
    {
        private IOptions<IdentityOptions> _options;
        public AuthService(IOptions<IdentityOptions> options) : base(options)
        {
            _options = options;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public async Task<UserInformation?> Login(string username, string password)
        {
            UserInformation? userInformation = null;

            try
            {

                using (DirectoryEntry directoryEntry = GetConnection(username, password))
                {
                    using (DirectorySearcher adsSearcher = new DirectorySearcher(directoryEntry))
                    {
                        adsSearcher.Filter = string.Format(_options.Value.SearchPattern, username);

                        SearchResult adsSearchResult = adsSearcher.FindOne();

                        if (adsSearchResult != null)
                        {
                            ResultPropertyCollection res = adsSearchResult.Properties;

                            userInformation = new UserInformation();
                            userInformation.cn = res["cn"][0].ToString();
                            userInformation.givenName = res["givenName"][0].ToString();
                            userInformation.distinguishedName = res["distinguishedName"][0].ToString();
                            userInformation.sAMAccountName = res["sAMAccountName"][0].ToString();
                            userInformation.mail = res["mail"][0].ToString();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                userInformation = null;
            }

            return userInformation;
        }


        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }


    }
}
