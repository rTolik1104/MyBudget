using Microsoft.EntityFrameworkCore;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Expense;

namespace MyBudget.Data.Services
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public ExpenseServices(AppDbContext dbContext, ILogger<ExpenseServices> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddExpense(Expenses expense)
        {
            try
            {
                await _dbContext.AddAsync(expense);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error add expense: " + ex.Message);
            }
        }

        public async Task DeleteExpense(Guid expenseId)
        {
            try
            {
                var expense = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.expenseId == expenseId);
                if (expense != null)
                {
                    _dbContext.Expenses.Remove(expense);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete expense: " + ex.Message);
            }
        }

        public string? GetCategoryNameById(Guid? categoryId)
        {
            var name = _dbContext.ExpenseCategories.Where(x => x.categoryId == categoryId).Select(x => x.category_name).First();
            return name;
        }

        public async Task<Expenses> GetExpenseById(Guid id)
        {
            var expense = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.expenseId == id);
            return expense;
        }

        public async Task<IEnumerable<Expenses>> GetExpensesByCategory(Guid categoryId)
        {
            try
            {
                var expenses = await _dbContext.Expenses.Where(x => x.categoty_id == categoryId).ToListAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error get expenses by category: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Expenses>> GetExpensesByUser(string userId)
        {
            try
            {
                var expenses = await _dbContext.Expenses.Where(x => x.user.Id == userId).OrderByDescending(x=>x.expense_date).ToListAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error get expenses by user: " + ex.Message);
                throw;
            }
        }

        public async Task<ExpenseCategoryDropdownVM> GetExpensesCategories()
        {
            var response = new ExpenseCategoryDropdownVM()
            {
                Categories = await _dbContext.ExpenseCategories.ToListAsync()
            };
            return response;
        }

        public async Task<decimal?> GetTotalIncomeByCategory(string userId, Guid? categoryId)
        {
            var total = await _dbContext.Expenses
                .Where(x => x.user.Id == userId && x.categoty_id == categoryId)
                .Select(x => x.expense_amount).SumAsync();
            return total;
        }

        public decimal? GetTotalUserExpense(string userId)
        {
            var total=GetExpensesByUser(userId).Result.Select(x=>x.expense_amount).Sum();
            return total;
        }

        public async Task UpdateExpense(Expenses expense)
        {
            try
            {
                _dbContext.Expenses.Update(expense);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update expense: " + ex.Message);
                throw;
            }
        }
    }
}
