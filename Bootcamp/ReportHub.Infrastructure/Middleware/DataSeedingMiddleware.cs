using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReportHub.Application.Contracts.Repository;
using ReportHub.Domain.Entities;

namespace ReportHub.Infrastructure.Middleware
{
    public class DataSeedingMiddleware
    {
        private readonly RequestDelegate _next;

        public DataSeedingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var invoiceRepository = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
                var bankAccountRepository = scope.ServiceProvider.GetRequiredService<IBankAccountRepository>();
                var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();


                #region ITEM SEED

                var existingItems = await itemRepository.GetAll(pageNumber: 1, pageSize: 1);
                if (!existingItems.Any())
                {
                    var items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemId = "6615502fdba2c01ffc3a1b01",
                            Name = "Wireless Mouse",
                            Description = "Ergonomic wireless mouse with USB receiver",
                            Price = 25.99m,
                            Currency = "USD"
                        },
                        new Item()
                        {
                            ItemId = "6615502fdba2c01ffc3a1b02",
                            Name = "Mechanical Keyboard",
                            Description = "Backlit mechanical keyboard with blue switches",
                            Price = 74.50m,
                            Currency = "USD"
                        },
                        new Item()
                        {
                            ItemId = "6615502fdba2c01ffc3a1b03",
                            Name = "27-inch Monitor",
                            Description = "Ultra HD 4K LED monitor with adjustable stand",
                            Price = 299.99m,
                            Currency = "EUR"
                        },
                        new Item()
                        {
                            ItemId = "6615502fdba2c01ffc3a1b04",
                            Name = "Noise Cancelling Headphones",
                            Description = "Over-ear wireless headphones with ANC",
                            Price = 149.00m,
                            Currency = "GBP"
                        }
                    };

                    foreach (var item in items)
                    {
                        Console.WriteLine($"Seeding item: {item.ItemId}");
                        await itemRepository.Insert(item);
                    }

                    Console.WriteLine("Item seeding completed.");
                }
                else
                {
                    Console.WriteLine("Database already contains items data. Skipping seeding.");
                }

                #endregion

                #region BANK ACCOUNT SEED
                var existingBankAccounts = await bankAccountRepository.GetAll(pageNumber: 1, pageSize: 1);
                if (!existingBankAccounts.Any())
                {
                    var bankAccounts = new List<BankAccount>()
                    {
                        new BankAccount()
                        {
                            BankAccountId = "66154f95dba2c01ffc3a1a01",
                            AccountNumber = "GE29NB0000000101904917",
                            AccountHolderName = "Alice Johnson",
                            Currency = "USD"
                        },
                        new BankAccount()
                        {
                            BankAccountId = "66154f95dba2c01ffc3a1a02",
                            AccountNumber = "DE89370400440532013000",
                            AccountHolderName = "Bob Smith",
                            Currency = "EUR"
                        },
                        new BankAccount()
                        {
                            BankAccountId = "66154f95dba2c01ffc3a1a03",
                            AccountNumber = "GB29NWBK60161331926819",
                            AccountHolderName = "Charlie Davis",
                            Currency = "GBP"
                        }
                    };

                    foreach (var account in bankAccounts)
                    {
                        Console.WriteLine($"Seeding account: {account.BankAccountId}");
                        await bankAccountRepository.Insert(account);
                    }

                    Console.WriteLine("Bank account seeding completed.");
                }
                else
                {
                    Console.WriteLine("Database already contains bank account data. Skipping seeding.");
                }

                #endregion

                #region INVOICE SEED
                var existingInvoices = await invoiceRepository.GetAll(pageNumber: 1, pageSize: 1);
                if (!existingInvoices.Any())
                {
                    var invoices = new List<Invoice>()
                    {
                        new Invoice()
                        {
                            InvoiceId = "INV2025001",
                            IssueDate = DateTime.UtcNow.AddDays(-10),
                            DueDate = DateTime.UtcNow.AddDays(20),
                            Amount = 275.48m,
                            Currency = "USD",
                            PaymentStatus = "Paid",
                            BankAccountId = "66154f95dba2c01ffc3a1a01",
                            Items = new List<InvoiceItem>()
                            {
                                new InvoiceItem()
                                {
                                    ItemId = "6615502fdba2c01ffc3a1b01",
                                    Quantity = 2,
                                    UnitPrice = 51.98m
                                },
                                new InvoiceItem()
                                {
                                    ItemId = "6615502fdba2c01ffc3a1b02",
                                    Quantity = 3,
                                    UnitPrice = 223.5m
                                }
                            }
                        },
                        new Invoice()
                        {
                            InvoiceId = "INV2025002",
                            IssueDate = DateTime.UtcNow.AddDays(-15),
                            DueDate = DateTime.UtcNow.AddDays(15),
                            Amount = 299.99m,
                            Currency = "EUR",
                            PaymentStatus = "Pending",
                            BankAccountId = "66154f95dba2c01ffc3a1a02",
                            Items = new List<InvoiceItem>()
                            {
                                new InvoiceItem()
                                {
                                    ItemId = "6615502fdba2c01ffc3a1b03",
                                    Quantity = 1,
                                    UnitPrice = 299.99m
                                }
                            }
                        },
                        new Invoice()
                        {
                            InvoiceId = "INV2025003",
                            IssueDate = DateTime.UtcNow.AddDays(-5),
                            DueDate = DateTime.UtcNow.AddDays(30),
                            Amount = 1192.00m,
                            Currency = "GBP",
                            PaymentStatus = "Overdue",
                            BankAccountId = "66154f95dba2c01ffc3a1a03",
                            Items = new List<InvoiceItem>()
                            {
                                new InvoiceItem()
                                {
                                    ItemId = "6615502fdba2c01ffc3a1b04",
                                    Quantity = 8,
                                    UnitPrice = 1192.00m
                                }
                            }
                        }
                    };

                    foreach (var invoice in invoices)
                    {
                        Console.WriteLine($"Seeding invoice: {invoice.InvoiceId}");
                        await invoiceRepository.Insert(invoice);
                    }

                    Console.WriteLine("Invoice seeding completed.");
                }
                else
                {
                    Console.WriteLine("Database already contains invoice data. Skipping seeding.");
                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data seeding failed: {ex.Message}");
            }

            await _next(context);
        }
    }
}
