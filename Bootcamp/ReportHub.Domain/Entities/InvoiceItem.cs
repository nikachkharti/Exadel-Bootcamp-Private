using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReportHub.Domain.Entities
{
    public class InvoiceItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } // Can match item.Price or be overridden
    }
}
