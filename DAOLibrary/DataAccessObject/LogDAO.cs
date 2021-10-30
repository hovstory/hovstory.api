using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.DataAccessObject
{
    internal class LogDAO
    {
        private static LogDAO instance = null;
        private static readonly object instanceLock = new object();

        private LogDAO()
        {

        }

        internal static LogDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LogDAO();
                    }
                    return instance;
                }
            }
        }

        internal void Log(string type, string content, string operatorName)
        {

        }
    }
}
