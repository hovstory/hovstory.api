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
    /// Data Access Object for User Collection
    /// </summary>
    internal class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        private UserDAO()
        {

        }

        /// <summary>
        /// Instance to access the methods
        /// </summary>
        internal static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Get a specific User by email
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The user</returns>
        internal User Get(string email)
        {
            User user = null;

            try
            {
                var _context = new HovStoryContext();
                user = _context.Users.Find(acc => acc.Email.Equals(email.ToLower()))
                        .SingleOrDefault();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The existed User or null</returns>
        internal User Login(string email, string password)
        {
            User user = null;

            try
            {
                var _context = new HovStoryContext();
                user = _context.Users.Find(acc => acc.Email.Equals(email.ToLower()) && acc.Password.Equals(password))
                        .SingleOrDefault();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }
    }
}
