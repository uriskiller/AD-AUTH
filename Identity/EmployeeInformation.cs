using System.DirectoryServices;

namespace AD_Auth.Identity
{
    public class EmployeeInformation
    {
        private string? _manager;
        public string? employeeType { get; set; }
        public string? displayName { get; set; }
        public string? title { get; set; }
        public string? department { get; set; }
        public string? manager
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int start = value.IndexOf("=") + 1;
                    int end = value.IndexOf("(") - (start + 1);
                    _manager = value.Substring(start, end);
                }
                else
                {
                    _manager = null;
                }
            }

            get => _manager;
        }
        public string? company { get; set; }
        public string? mail { get; set; }
        public string? telephoneNumber { get; set; }
        public string? l { get; set; }
        public string? c { get; set; }
        public string? whenCreated { get; set; }
    }
}
