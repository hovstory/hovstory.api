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
        /// <returns>List of Confessions</returns>
        internal List<Confession> Get(bool orderByDate = false)
        {
            List<Confession> confessions = new List<Confession>();

            try
            {
                var _context = new HovStoryContext();
                confessions = _context.Confessions.Find(_ => true)
                                .ToList();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return confessions;
        }

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

        internal Confession Get()

        internal void Create(Confession confession)
        {
            try
            {
                var _context = new HovStoryContext();
                _context.Confessions.InsertOne(confession);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void Update(string id, Confession updatedConfession)
        {
            try
            {
                var _context = new HovStoryContext();
                _context.Confessions.ReplaceOne(cfs => cfs.Id.Equals(id), updatedConfession);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
