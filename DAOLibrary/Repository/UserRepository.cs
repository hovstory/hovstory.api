using DAOLibrary.DataAccessObject;
using DTOLibrary;

namespace DAOLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Get(string email) => UserDAO.Instance.Get(email);

        public User Login(string email, string password) => UserDAO.Instance.Login(email, password);

        public User SignUp(User user) => UserDAO.Instance.SignUp(user);
    }
}
