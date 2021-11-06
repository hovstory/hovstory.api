using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOVStoryUtils
{
    public class HOVStoryDatetime
    {
        public DateTime ConvertToVn(DateTime utcTime)
        {
            try
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime vnTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
                return vnTime;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
