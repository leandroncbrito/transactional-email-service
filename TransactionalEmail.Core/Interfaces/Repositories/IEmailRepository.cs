using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using TransactionalEmail.Core.Models;

namespace TransactionalEmail.Core.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<ObjectId> CreateAsync(Email email);

        Task CreateManyAsync(IEnumerable<Email> emails);
    }
}
