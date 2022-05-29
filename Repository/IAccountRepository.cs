using HospitalProject.Models;

namespace HospitalProject.Repository
{
    public interface IAccountRepository
    {
        public User Add(User user);
        public User Login(string email, string password);
        public bool Check(string email);
        public User CheckLogin(string email, string password);
    }
}
