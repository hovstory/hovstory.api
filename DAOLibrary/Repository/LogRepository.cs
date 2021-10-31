using DAOLibrary.DataAccessObject;
using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository
{
    public class LogRepository : ILogRepository
    {
        public void Log(Log log) => LogDAO.Instance.Log(log);
    }
}
