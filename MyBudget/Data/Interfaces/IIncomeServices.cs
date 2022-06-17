using MyBudget.Models;
using MyBudget.ViewModels.Income;

namespace MyBudget.Data.Interfaces
{
    public interface IIncomeServices
    {
        public Task<IEnumerable<Incomes>> GetIncomesByUser(string userId);
        public Task<Incomes> GetIncomeById(Guid incomeId);
        public Task<IEnumerable<Incomes>> GetIncomesByCategory(Guid categoryId);
        public Task<IncomeCategoryDropdownVM> GetIncomeCategories();

        public Task AddIncome(Incomes income);
        public Task UpdateIncome(Incomes income);
        public Task DeleteIncome(Guid incomeId);
        public decimal? GetTotalUserIncome(string userId);
        public string? GetCategoryNameById(Guid? categoryId);
        public Task<decimal?> GetTotalIncomeByCategory(string userId, Guid? categoryId);
    }
}
