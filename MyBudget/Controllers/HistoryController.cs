using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Expense;
using MyBudget.ViewModels.Home;
using MyBudget.ViewModels.Income;

namespace MyBudget.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IIncomeServices _incomeServices;
        private readonly IExpenseServices _expenseServices;
        private readonly UserManager<Users> _userManager;

        public HistoryController(IIncomeServices incomeServices, IExpenseServices expenseServices, UserManager<Users> userManager)
        {
            _expenseServices = expenseServices;
            _userManager = userManager;
            _incomeServices = incomeServices;
        }

        public async Task<IActionResult> Index()
        {
            var incomes = await _incomeServices.GetIncomesByUser(_userManager.GetUserId(User));
            var expenses = await _expenseServices.GetExpensesByUser(_userManager.GetUserId(User));

            var incomesList = incomes.Select(x => new IncomeVM
            {
                Category = _incomeServices.GetCategoryNameById(x.categoty_id),
                Amout = x.income_amount,
                Comment = x.comment,
                Date = x.income_date
            });
            var expensesList = expenses.Select(x => new ExpenseVM
            {
                Category = _expenseServices.GetCategoryNameById(x.categoty_id),
                Amout = x.expense_amount,
                Comment = x.comment,
                Date = x.expense_date
            });
            var model = new HistoryVM
            {
                Incomes = incomesList.ToList(),
                Expenses = expensesList.ToList(),
            };
            return View(model);
        }
    }
}
