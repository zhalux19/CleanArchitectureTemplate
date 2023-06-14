using KafkaTemplate.Data.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KafkaTemplate.Data.DataModels
{
    [Collection("Account")]
    public class AccountData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
