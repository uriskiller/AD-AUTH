using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace AD_Auth.Identity
{
    public class LdapProvider
    {
        private readonly IOptions<IdentityOptions> _options;
        private readonly string _basePath;
        public LdapProvider(IOptions<IdentityOptions> options)
        {
            _options = options;
            _basePath = string.Concat("LDAP://", _options.Value.BaseSearch);
        }

        protected DirectoryEntry GetConnection(string _Username, string _Password) =>
            new(_basePath, _Username, _Password, AuthenticationTypes.Secure);

        protected DirectoryEntry GetConnection() =>
            new(_basePath);

        protected string? getEntryString(SearchResult context, string proprietaryName)
        {
            var result = context.Properties;
            return result[proprietaryName][0].ToString() != null ? result[proprietaryName][0].ToString() : null;
        }
    }
}
