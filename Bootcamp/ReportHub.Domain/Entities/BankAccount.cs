using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReportHub.Domain.Entities
{
    public class BankAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BankAccountId { get; set; }

        public string AccountNumber { get; set; }

        public string AccountHolderName { get; set; }

        public string Currency { get; set; }
    }
}
