using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransactionalEmail.Core.Models
{
    public class Email
    {
        public Email(string to, string subject, string message)
        {
            To = to;
            Subject = subject;
            Message = message;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Message { get; private set; }
    }
}
