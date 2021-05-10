using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionalEmail.Consumer.Context;
using TransactionalEmail.Consumer.Interfaces.Repositories;
using TransactionalEmail.Consumer.Models;

namespace TransactionalEmail.Consumer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<User> Add(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.Where(x => x.Email == email).FirstAsync();
        }

        public async Task<User> Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return user;
        }
    }
}
