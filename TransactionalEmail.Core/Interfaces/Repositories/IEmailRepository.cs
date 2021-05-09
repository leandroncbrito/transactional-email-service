using System.Threading.Tasks;
using MongoDB.Bson;
using TransactionalEmail.Core.Models;

namespace TransactionalEmail.Core.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<ObjectId> CreateAsync(Email email);
    }
}
