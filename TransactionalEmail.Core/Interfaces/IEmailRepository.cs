using System.Threading.Tasks;
using MongoDB.Bson;
using TransactionalEmail.Core.Models;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IEmailRepository
    {
        Task<ObjectId> CreateAsync(Email email);
    }
}
