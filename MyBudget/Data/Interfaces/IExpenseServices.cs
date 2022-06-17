using MyBudget.Models;
using MyBudget.ViewModels.Expense;

namespace MyBudget.Data.Interfaces
{
    public interface IExpenseServices
    {
        public Task<IEnumerable<Expenses>> GetExpensesByUser(string userId);
        public Task<Expenses> GetExpenseById(Guid id);
        public Task<IEnumerable<Expenses>> GetExpensesByCategory(Guid categoryId);
        public Task<ExpenseCategoryDropdownVM> GetExpensesCategories();

        public Task AddExpense(Expenses expense);
        public Task UpdateExpense(Expenses expense);
        public Task DeleteExpense(Guid expenseId);
        decimal? GetTotalUserExpense(string userId);
        public string? GetCategoryNameById(Guid? categoryId);
        public Task<decimal?> GetTotalIncomeByCategory(string userId, Guid? categoryId);
    }
}
