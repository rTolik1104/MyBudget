using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Expense;

namespace MyBudget.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseServices _expenseServices;
        private readonly UserManager<Users> _userManager;

        public ExpensesController(IExpenseServices expenseServices, UserManager<Users> userManager)
        {
            _expenseServices = expenseServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var expenses = await _expenseServices.GetExpensesByUser(userId);
            var expensesList = expenses.Select(x => new ExpenseVM
            {
                Category = _expenseServices.GetCategoryNameById(x.categoty_id),
                Amout = x.expense_amount,
                Comment = x.comment,
                Date = x.expense_date,
                ExpenseId = x.expenseId
            });
            var totalexpense = _expenseServices.GetTotalUserExpense(userId);
            var model = new ExpensesListVM
            {
                Expenses = expensesList.ToList(),
                Total = totalexpense,
            };
            return View(model);
        }

        public async Task<IActionResult> AddExpense()
        {
            var categories = await _expenseServices.GetExpensesCategories();
            ViewBag.Categories = new SelectList(categories.Categories, "categoryId", "category_name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExpense(AddExpenseVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var expense = new Expenses
                {
                    categoty_id = model.Category,
                    expense_amount = model.Amount,
                    expense_date = model.Date,
                    comment = model.Comment,
                    user = user
                };

                try
                {
                    await _expenseServices.AddExpense(expense);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return Json(model);
                }
            }
            return Json(model);
        }

        public async Task<IActionResult> EditExpense(Guid expenseId)
        {
            var categories = await _expenseServices.GetExpensesCategories();
            ViewBag.Categories = new SelectList(categories.Categories, "categoryId", "category_name");
            var income = await _expenseServices.GetExpenseById(expenseId);
            if (income != null)
            {
                var model = new EditExpenseVM
                {
                    Category = income.categoty_id,
                    Date = income.expense_date,
                    Amount = income.expense_amount,
                    Comment = income.comment,
                    ExpenseId = income.expenseId
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditExpense(EditExpenseVM model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var expense = new Expenses
                {
                    categoty_id = model.Category,
                    expense_amount = model.Amount,
                    expense_date = model.Date,
                    comment = model.Comment,
                    user = user,
                    expenseId = model.ExpenseId
                };
                try
                {
                    await _expenseServices.UpdateExpense(expense);
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteExpense(Guid expenseId)
        {
            var expense = await _expenseServices.GetExpenseById(expenseId);
            if (expense != null)
            {
                var model = new DeleteExpenseVM
                {
                    Category = _expenseServices.GetCategoryNameById(expense.categoty_id),
                    Date = expense.expense_date,
                    Amount = expense.expense_amount,
                    Comment = expense.comment,
                    ExpenseId = expense.expenseId
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteExpense(DeleteExpenseVM model)
        {
            await _expenseServices.DeleteExpense(model.ExpenseId);
            return RedirectToAction(nameof(Index));
        }
    }
}
