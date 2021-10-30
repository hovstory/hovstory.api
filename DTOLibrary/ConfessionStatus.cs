using HOVStoryConfiguration;
using System.Text.RegularExpressions;

namespace DTOLibrary
{
    public static class ConfessionStatus
    {
        public static readonly string Approved = Configuration.ConfessionApproved;
        public static readonly string Rejected = Configuration.ConfessionRejected;
        public static readonly string Pending = Configuration.ConfessionPending;

        public static readonly Regex CheckStatus = new Regex(@"^[" + Approved + Rejected + Pending + @"]$");
    }
}
