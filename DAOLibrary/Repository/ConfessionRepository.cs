using DAOLibrary.DataAccessObject;
using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository
{
    public class ConfessionRepository : IConfessionRepository
    {
        public List<Confession> GetConfessions() => ConfessionDAO.Instance.Get();
    }
}
