using System;
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

        [BsonElement("to")]
        public string To { get; private set; }

        [BsonElement("subject")]
        public string Subject { get; private set; }

        [BsonElement("message")]
        public string Message { get; private set; }

        [BsonElement("sent_date")]
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
