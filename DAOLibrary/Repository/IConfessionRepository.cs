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
        public List<Confession> Get(bool orderByDate = false, string status = "", string[] confessionIds = null);

        /// <summary>
        /// Get a specific Confession based on its Object Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The Confession</returns>
        public Confession Get(string id);

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

        /// <summary>
        /// Approve a confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <returns>The Approved Confession</returns>
        /// <exception cref="Exception"></exception>
        public Confession Approve(string id, string admin);

        /// <summary>
        /// Reject a confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <param name="comment"></param>
        /// <returns>The Rejected Confession</returns>
        /// <exception cref="Exception"></exception>
        public Confession Reject(string id, string admin, string comment);
    }
}
