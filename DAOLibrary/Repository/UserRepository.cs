using DAOLibrary.DataAccessObject;
using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Get(string email) => UserDAO.Instance.Get(email);

        public User Login(string email, string password) => UserDAO.Instance.Login(email, password);

        public User SignUp(User user) => UserDAO.Instance.SignUp(user);
    }
}
