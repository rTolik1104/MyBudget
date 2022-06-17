using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBudget.Models;

namespace MyBudget.Data
{
    public class AppDbContext:IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Incomes> Incomes { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<IncomeCategories> IncomeCategories { get; set; }
        public DbSet<ExpenseCategories> ExpenseCategories { get; set; }
    }
}
