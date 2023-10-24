using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace AD_Auth.Identity
{
    public class EmployeeService : LdapProvider
    {
        private IOptions<IdentityOptions> _options;
        public EmployeeService(IOptions<IdentityOptions> options) : base(options)
        {
            _options = options;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public async Task<EmployeeInformation?> GetInformation(string employeeNumber)
        {
            EmployeeInformation? userInformation = null;

            try
            {

                using (DirectoryEntry directoryEntry = GetConnection())
                {
                    using (DirectorySearcher adsSearcher = new DirectorySearcher(directoryEntry))
                    {
                        adsSearcher.Filter = string.Format(_options.Value.SearchEmployeePattern, employeeNumber);

                        SearchResult adsSearchResult = adsSearcher.FindOne();

                        if (adsSearchResult != null)
                        {
                            ResultPropertyCollection res = adsSearchResult.Properties;

                            userInformation = new EmployeeInformation();
                            userInformation.mail = res["mail"][0].ToString();
                            userInformation.title = res["title"][0].ToString();
                            userInformation.manager = res["manager"][0].ToString();
                            userInformation.displayName = res["displayName"][0].ToString();
                            userInformation.department = res["department"][0].ToString();
                            userInformation.company = res["company"][0].ToString();
                            userInformation.telephoneNumber = res["telephoneNumber"][0].ToString();
                            userInformation.l = res["l"][0].ToString();
                            userInformation.employeeType = res["employeeType"][0].ToString();
                            userInformation.c = res["c"][0].ToString();
                            userInformation.whenCreated = res["whenCreated"][0].ToString();
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




    }
}
