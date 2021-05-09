using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransactionalEmail.Core.Models
{
    public class Email
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("format")]
        public string Format { get; set; }

        [BsonElement("sent_date")]
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
