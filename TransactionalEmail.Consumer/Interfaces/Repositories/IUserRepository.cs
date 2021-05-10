using System.Threading.Tasks;
using TransactionalEmail.Consumer.Models;

namespace TransactionalEmail.Consumer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> GetByEmail(string email);
        Task<User> Update(User user);
    }
}
