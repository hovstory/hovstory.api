using DTOLibrary;
using System;

namespace DAOLibrary.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get a specific User by email
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The user</returns>
        public User Get(string email);

        /// <summary>
        /// Login with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The existed User or null</returns>
        public User Login(string email, string password);

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>The created User</returns>
        public User SignUp(User user);
    }
}
