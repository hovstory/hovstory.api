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
        public Confession Approve(string id, string admin) => ConfessionDAO.Instance.Approve(id, admin);

        public void Create(Confession confession) => ConfessionDAO.Instance.Create(confession);

        public Confession Get(string id) => ConfessionDAO.Instance.Get(id);

        public List<Confession> Get(bool orderByDate = false, string status = "", string[] confessionIds = null) => ConfessionDAO.Instance.Get(orderByDate, status, confessionIds);

        public Confession Reject(string id, string admin, string comment) => ConfessionDAO.Instance.Reject(id, admin, comment);

        public void Update(string id, Confession updatedConfession) => ConfessionDAO.Instance.Update(id, updatedConfession);

    }
}
