﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReportHub.Domain.Entities
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string InvoiceId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string PaymentStatus { get; set; } // e.g., "Pending", "Paid", "Overdue"

        [BsonRepresentation(BsonType.ObjectId)]
        public string BankAccountId { get; set; } // Reference to BankAccount

        public List<InvoiceItem> Items { get; set; }
    }
}
