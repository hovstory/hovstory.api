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
        /// <param name="orderByDate"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>List of Confession</returns>
        public List<Confession> Get(bool orderByDate = false);

        /// <summary>
        /// Get a specific Confession based on its Object Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The Confession</returns>
        public Confession Get(string id);

        /// <summary>
        /// Get List of Confessions by Status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="orderByDate"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public List<Confession> Get(string status, bool orderByDate = false);

        /// <summary>
        /// Insert a new Confession
        /// </summary>
        /// <param name="confession"></param>
        /// <exception cref="Exception"></exception>
        public void Create(Confession confession);

        /// <summary>
        /// Update an existed Confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedConfession"></param>
        /// <exception cref="Exception"></exception>
        public void Update(string id, Confession updatedConfession);


    }
}
