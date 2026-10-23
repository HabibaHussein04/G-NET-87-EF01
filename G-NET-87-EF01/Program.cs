
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#region Question 4 - Entities

namespace ReadMoreBooks
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public int PublicationYear { get; set; }
        public bool IsInStock { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Author> Authors { get; set; }
            = new List<Author>();
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }
            = new List<Book>();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; }
            = new List<Book>();
    }
}

#endregion

#region Question 5 - DbContext

namespace ReadMoreBooks
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(
            DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

#endregion

#region Question 1 and Question 3 - Console Application and SQL Server Provider

namespace ReadMoreBooks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question 6 - Create and Verify Database

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Database connection string was not found.");

            var options =
                new DbContextOptionsBuilder<BookstoreContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            using (var context = new BookstoreContext(options))
            {
                context.Database.EnsureCreated();

                Console.WriteLine(
                    "Database created successfully!");

                Console.WriteLine(
                    $"Books table accessible: {context.Books.Any()}");

                Console.WriteLine(
                    $"Authors table accessible: {context.Authors.Any()}");

                Console.WriteLine(
                    $"Categories table accessible: {context.Categories.Any()}");
            }

            #endregion
        }
    }
}

#endregion
