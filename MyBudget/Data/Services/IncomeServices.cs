using Microsoft.EntityFrameworkCore;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Income;

namespace MyBudget.Data.Services
{
    public class IncomeServices : IIncomeServices
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public IncomeServices(AppDbContext appDbContext, ILogger<IncomeServices> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
        }


        public async Task AddIncome(Incomes income)
        {
            try
            {
                await _dbContext.AddAsync(income);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error add income: " + ex.Message);
            }
        }

        public async Task DeleteIncome(Guid incomeId)
        {
            try
            {
                var income = await _dbContext.Incomes.FirstOrDefaultAsync(x => x.incomeId == incomeId);
                if (income != null)
                {
                    _dbContext.Incomes.Remove(income);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete income: " + ex.Message);
            }
        }

        public string? GetCategoryNameById(Guid? categoryId)
        {
            var name =  _dbContext.IncomeCategories.Where(x => x.categoryId == categoryId).Select(x => x.category_name).First();
            return name;
        }

        public async Task<Incomes> GetIncomeById(Guid incomeId)
        {
            var income = await _dbContext.Incomes.FirstOrDefaultAsync(x => x.incomeId == incomeId);
            return income;
        }

        public async Task<IncomeCategoryDropdownVM> GetIncomeCategories()
        {
            var response = new IncomeCategoryDropdownVM()
            {
                Categories = await _dbContext.IncomeCategories.ToListAsync()
            };
            return response;
        }

        public async Task<IEnumerable<Incomes>> GetIncomesByCategory(Guid categoryId)
        {
            try
            {
                var incomes = await _dbContext.Incomes.Where(x => x.categoty_id == categoryId).OrderByDescending(x=>x.income_date).ToListAsync();
                return incomes;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error get incomes by category: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Incomes>> GetIncomesByUser(string userId)
        {
            try
            {
                var incomes = await _dbContext.Incomes.Where(x => x.user.Id == userId).ToListAsync();
                return incomes;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error get incomes by user: " + ex.Message);
                throw;
            }
        }

        public async Task<decimal?> GetTotalIncomeByCategory(string userId, Guid? categoryId)
        {
            var total =await _dbContext.Incomes
                .Where(x => x.user.Id == userId && x.categoty_id == categoryId)
                .Select(x => x.income_amount).SumAsync();
            return total;
        }

        public decimal? GetTotalUserIncome(string userId)
        {
            var total = GetIncomesByUser(userId).Result.Select(x => x.income_amount).Sum();
            return total;
        }

        public async Task UpdateIncome(Incomes income)
        {
            try
            {
                _dbContext.Incomes.Update(income);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update income: " + ex.Message);
                throw;
            }
        }
    }
}
