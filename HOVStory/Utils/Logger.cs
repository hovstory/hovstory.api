using DAOLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HOVStory.Utils
{
    public static class Logger
    {
        public static void Log(string type, string content, System.Security.Claims.ClaimsPrincipal user)
        {
            try
            {
                ILogRepository logRepository = new LogRepository();
                logRepository.Log(new DTOLibrary.Log
                {
                    Type = type,
                    Content = content,
                    Operator = getOperatorName(user)
                });
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[{DateTime.Now}] Error at Logger: {ex.Message}. Operator: {operatorName}");
            }
            System.Console.WriteLine($"[{DateTime.Now}] Error type: {type}. Content: {content}. Operator: {operatorName}");
        }

        private static string getOperatorName(System.Security.Claims.ClaimsPrincipal user)
        {
            string operatorName = string.Empty;
            if (user.Identity.IsAuthenticated)
            {
                var claims = user.Claims;
                operatorName = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
            }

            return operatorName;
        }
    }
}
