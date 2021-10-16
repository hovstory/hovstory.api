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
                user = _context.Users.Find(acc => acc.Email.Equals(email.ToLower()))
                        .SingleOrDefault();

                if (user != null)
                {
                    bool verfied = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    if (!verfied)
                    {
                        user = null;
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The created User</returns>
        internal User SignUp(User user)
        {
            try
            {
                var _context = new HovStoryContext();
                User _user = _context.Users.Find(acc => acc.Email.Equals(user.Email.ToLower()))
                                .SingleOrDefault();

                if (_user == null)
                {
                    user.CreatedAt = DateTime.Now;
                    _context.Users.InsertOne(user);
                    return user;
                } else
                {
                    throw new Exception("Email is existed! Please try again or login with the email!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
