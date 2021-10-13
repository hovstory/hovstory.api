using HOVStoryConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
