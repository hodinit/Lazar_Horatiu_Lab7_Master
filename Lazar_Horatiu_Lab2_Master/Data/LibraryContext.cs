using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lazar_Horatiu_Lab2_Master.Models;

namespace Lazar_Horatiu_Lab2_Master.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Lazar_Horatiu_Lab2_Master.Models.Book> Book { get; set; } = default!;
        public DbSet<Lazar_Horatiu_Lab2_Master.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Lazar_Horatiu_Lab2_Master.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Lazar_Horatiu_Lab2_Master.Models.Author> Author { get; set; } = default!;
        public DbSet<Lazar_Horatiu_Lab2_Master.Models.Order> Order { get; set; } = default!;
    }
}
