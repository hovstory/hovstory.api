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
        public static readonly string Approved = "A";
        public static readonly string Rejected = "R";
        public static readonly string Pending = "P";

        public static readonly Regex CheckStatus = new Regex(@"^[" + Approved + Rejected + Pending + @"]$");
    }
}
