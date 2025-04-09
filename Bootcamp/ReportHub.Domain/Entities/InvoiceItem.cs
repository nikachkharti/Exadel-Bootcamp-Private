using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

/*
    Imagine this:

    An Invoice can contain multiple items (e.g., 2 keyboards, 3 mice)
    An Item (like a keyboard) can appear on multiple invoices
    But each time it appears on an invoice, it has:
    A quantity
    A price at the time of sale (not necessarily the current item price)

    So you need more than just an ItemId. You need something like this:
    
    public class InvoiceItem
    {
        public string ItemId { get; set; }      // reference to the item
        public int Quantity { get; set; }       // how many were sold
        public decimal UnitPrice { get; set; }  // the price at the moment of invoicing
    }

    This is perfect for the following structure inside an Invoice:
    public class Invoice
    {
        public string InvoiceId { get; set; }
        public List<InvoiceItem> Items { get; set; } = new();
    }
    

    So now, when an invoice is saved in MongoDB, it might look like this:
    {
      "_id": "661555d1dba2c01ffc3a1c01",
      "items": [
        { "itemId": "6615502fdba2c01ffc3a1b01", "quantity": 2, "unitPrice": 25.99 },
        { "itemId": "6615502fdba2c01ffc3a1b02", "quantity": 1, "unitPrice": 74.50 }
      ]
    }

    Relationship	Needs InvoiceItem?	Why
    One-to-Many	❌	A foreign key or reference is enough
    Many-to-Many with no extra data	❌ or ✅	In Mongo, you might store lists of Ids
    Many-to-Many with extra data (like price, qty)	✅	You need a separate model like InvoiceItem

 */


namespace ReportHub.Domain.Entities
{
    public class InvoiceItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ItemId { get; set; }

        public int Quantity { get; set; } // OPTIONAL

        public decimal UnitPrice { get; set; } // OPTIONAL Can match item.Price or be overridden
    }
}
