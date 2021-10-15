using DTOLibrary;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        internal List<Confession> Get(bool orderByDate = false)
        {
            List<Confession> confessions = new List<Confession>();

            try
            {
                var _context = new HovStoryContext();
                confessions = _context.Confessions.Find(_ => true)
                                .ToList();

                if (orderByDate)
                {
                    confessions = confessions.OrderByDescending(cfs => cfs.CreatedAt)
                                    .ToList();
                }

            } catch (Exception ex)
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
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return confession;
        }

        /// <summary>
        /// Get List of Confessions by Status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="orderByDate">Set this to true if you want to get confessions and order them by Created Date Descending.</param>
        /// <see cref="ConfessionStatus"/>
        /// <exception cref="Exception"></exception>
        /// <returns>List of Confessions</returns>
        internal List<Confession> Get(string status, bool orderByDate = false)
        {
            List<Confession> confessions = new List<Confession>();
            try
            {
                if (!ConfessionStatus.CheckStatus.IsMatch(status))
                {
                    throw new Exception("Invalid Status value!");
                }
                var _context = new HovStoryContext();
                confessions = _context.Confessions.Find(cfs => cfs.Status.Equals(status))
                                .ToList();

                if (orderByDate)
                {
                    confessions = confessions.OrderByDescending(cfs => cfs.CreatedAt)
                                    .ToList();
                }
                
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return confessions;
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
                DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now,
                    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                confession.CreatedAt = now;
                confession.ModifiedOn = now;

                _context.Confessions.InsertOne(confession);
            } catch (Exception ex)
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
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Approve a confession
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <exception cref="Exception"></exception>
        internal void Approve(string id, string admin)
        {
            try
            {
                var _context = new HovStoryContext();
                Confession confession = Get(id);

                if (confession == null)
                {
                    throw new Exception($"Can't find confession with the id {id}");
                }

                int approveId = GenerateApproveId();
                confession.Status = ConfessionStatus.Approved;
                confession.Comment = approveId.ToString();
                confession.Admin = admin;
                confession.ModifiedOn = TimeZoneInfo.ConvertTime(DateTime.Now, 
                    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

                Update(id, confession);
            } catch (Exception ex)
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
        /// <exception cref="Exception"></exception>
        internal void Reject(string id, string admin, string comment)
        {
            try
            {
                var _context = new HovStoryContext();
                Confession confession = Get(id);

                if (confession == null)
                {
                    throw new Exception($"Can't find confession with the id {id}");
                }

                confession.Status = ConfessionStatus.Rejected;
                confession.Comment = comment;
                confession.Admin = admin;
                confession.ModifiedOn = TimeZoneInfo.ConvertTime(DateTime.Now,
                    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

                Update(id, confession);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int GenerateApproveId()
        {
            int approveId = 0;

            try
            {
                var _context = new HovStoryContext();
                IEnumerable<Confession> confessions = _context.Confessions.Find(cfs => cfs.Status.Equals(ConfessionStatus.Approved))
                                                .ToList().OrderByDescending(cfs => cfs.Comment);

                // Get the current maximum id
                if (!confessions.Any())
                {
                    approveId = 1;
                } else
                {
                    string idStr = confessions.First().Comment;
                    approveId = int.Parse(idStr);
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return approveId;
        }
    }
}
