using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository
{
    public interface IConfessionRepository
    {
        /// <summary>
        /// Get All the Confessions in the Collection
        /// </summary>
        /// <returns>List of Confession</returns>
        /// <seealso cref="System.Collections.Generic.List{T}"/>
        /// <seealso cref="DTOLibrary.Confession"/>
        public List<Confession> GetConfessions();
    }
}
