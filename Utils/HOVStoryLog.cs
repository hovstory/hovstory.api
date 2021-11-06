using DAOLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HOVStoryUtils
{
    public class HOVStoryLog
    {
        public void Log(string type, string content, ClaimsPrincipal user)
        {
            string operatorName = getOperatorName(user);
            try
            {
                ILogRepository logRepository = new LogRepository();
                logRepository.Log(new DTOLibrary.Log
                {
                    Type = type,
                    Content = content,
                    Operator = operatorName
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error at Logger: {ex.Message}. Operator: {operatorName}");
            }
            Console.WriteLine($"[{DateTime.Now}] Error type: {type}. Content: {content}. Operator: {operatorName}");
        }

        private string getOperatorName(ClaimsPrincipal user)
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
