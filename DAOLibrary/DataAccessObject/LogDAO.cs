using DTOLibrary;
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

        internal void Log(Log log)
        {
            try
            {
                var _context = new HovStoryContext();
                log.CreatedAt = DateTime.Now;

                _context.Logs.InsertOne(log);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
