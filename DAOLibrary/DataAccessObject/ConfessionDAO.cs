using DTOLibrary;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOLibrary.DataAccessObject
{
    /// <summary>
    /// Data Access Object for Confession Collection
    /// </summary>
    internal class ConfessionDAO
    {
        private static ConfessionDAO instance = null;
        private static readonly object instanceLock = new object();

        private ConfessionDAO()
        {

        }

        /// <summary>
        /// Instance to access the methods
        /// </summary>
        internal static ConfessionDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ConfessionDAO();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Get All of Confessions in the Collection
        /// </summary>
        /// <param name="orderByDate">Set this to true if you want to get confessions and order them by Created Date Descending.</param>
        /// <exception cref="Exception"></exception>
        /// <returns>List of Confessions</returns>
        internal List<Confession> Get(bool orderByDate = false, string status = "", string[] confessionIds = null)
        {
            List<Confession> confessions = new List<Confession>();

            try
            {
                var _context = new HovStoryContext();
                if (confessionIds != null)
                {
                    confessions = _context.Confessions.Find(conf => confessionIds.Contains(conf.Id))
                                    .ToList();
                }
                else
                {

                    confessions = _context.Confessions.Find(_ => true)
                                    .ToList();
                }

                if (orderByDate)
                {
                    confessions = confessions.OrderByDescending(cfs => cfs.CreatedAt)
                                    .ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    if (!ConfessionStatus.CheckStatus.IsMatch(status))
                    {
                        throw new Exception("Invalid Status value!");
                    }

                    confessions = confessions.Where(cfs => cfs.Status.Equals(status))
                                    .ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return confessions;
        }

        /// <summary>
        /// Get a specific confession based on the object id
        /// </summary>
        /// <param name="id">The object id of the confession</param>
        /// <exception cref="Exception">Throw Exception if more than one confessions were found.</exception>
        /// <returns>The Confession</returns>
        internal Confession Get(string id)
        {
            Confession confession = null;

            try
            {
                var _context = new HovStoryContext();
                confession = _context.Confessions.Find(cfs => cfs.Id.Equals(id))
                                .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return confession;
        }

        /// <summary>
        /// Insert a new Confession
        /// </summary>
        /// <param name="confession"></param>
        /// <exception cref="Exception"></exception>
        internal void Create(Confession confession)
        {
            try
            {
                var _context = new HovStoryContext();

                confession.Status = ConfessionStatus.Pending;
                confession.CreatedAt = DateTime.Now;
                confession.ModifiedOn = DateTime.Now;

                _context.Confessions.InsertOne(confession);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update an existed Confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedConfession"></param>
        /// <exception cref="Exception"></exception>
        internal void Update(string id, Confession updatedConfession)
        {
            try
            {
                var _context = new HovStoryContext();

                // Check existence of Confession Id
                Confession confession = Get(id);
                if (confession == null)
                {
                    throw new Exception("Confession does not exist!!");
                }

                _context.Confessions.ReplaceOne(cfs => cfs.Id.Equals(id), updatedConfession);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Approve a confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <returns>The Approved Confession</returns>
        /// <exception cref="Exception"></exception>
        internal Confession Approve(string id, string admin)
        {
            try
            {
                var _context = new HovStoryContext();
                Confession confession = Get(id);

                if (confession == null)
                {
                    throw new Exception($"Can't find confession with the id {id}");
                }

                if (confession.Status == ConfessionStatus.Approved)
                {
                    throw new Exception($"Confession is already approved!! " +
                        $"Confession Approved Id: {confession.ConfessionId}");
                }

                int approveId = GenerateApproveId();
                confession.Status = ConfessionStatus.Approved;
                confession.Comment = approveId.ToString();
                confession.Admin = admin;
                confession.ModifiedOn = DateTime.Now;
                confession.ConfessionId = approveId;

                Update(id, confession);
                return confession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reject a confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <param name="comment"></param>
        /// <returns>The Rejected Confession</returns>
        /// <exception cref="Exception"></exception>
        internal Confession Reject(string id, string admin, string comment)
        {
            try
            {
                var _context = new HovStoryContext();
                Confession confession = Get(id);

                if (confession == null)
                {
                    throw new Exception($"Can't find confession with the id {id}");
                }

                if (confession.Status == ConfessionStatus.Rejected)
                {
                    throw new Exception($"Confession is already rejected!! " +
                        $"Confession Id: {confession.Id}");
                }

                confession.Status = ConfessionStatus.Rejected;
                confession.Comment = comment;
                confession.Admin = admin;
                confession.ModifiedOn = DateTime.Now;

                Update(id, confession);
                return confession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int GenerateApproveId()
        {
            int approveId = 1;

            try
            {
                var _context = new HovStoryContext();
                IEnumerable<Confession> confessions = _context.Confessions.Find(
                    cfs => cfs.Status.Equals(ConfessionStatus.Approved))
                    .ToList().OrderByDescending(cfs => cfs.ConfessionId);

                // Get the current maximum id
                if (!confessions.Any())
                {
                    approveId = 1;
                }
                else
                {
                    int id = confessions.First().ConfessionId;
                    approveId = ++id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return approveId;
        }
    }
}
