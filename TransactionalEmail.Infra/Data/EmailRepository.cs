using MongoDB.Driver;
using TransactionalEmail.Core.Models;
using TransactionalEmail.Core.Interfaces;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TransactionalEmail.Infra.Data
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IMongoCollection<Email> collection;

        public EmailRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("TransactionalEmail");
            this.collection = database.GetCollection<Email>("Emails");
        }

        public async Task<ObjectId> CreateAsync(Email email)
        {
            await collection.InsertOneAsync(email);

            return email.Id;
        }
    }
}
