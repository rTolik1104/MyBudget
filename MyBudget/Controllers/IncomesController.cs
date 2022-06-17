using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Income;

namespace MyBudget.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IIncomeServices _incomeServices;
        private readonly UserManager<Users> _userManager;

        public IncomesController(IIncomeServices incomeServices, UserManager<Users> userManager)
        {
            _incomeServices = incomeServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var incomes = await _incomeServices.GetIncomesByUser(userId);
            var incomesList = incomes.Select(x => new IncomeVM
            {
                Category =_incomeServices.GetCategoryNameById(x.categoty_id),
                Amout = x.income_amount,
                Comment = x.comment,
                Date = x.income_date,
                IncomeId=x.incomeId
            });
            var totalIncome = _incomeServices.GetTotalUserIncome(userId);
            var model = new IncomesListVM
            {
                Incomes = incomesList.ToList(),
                TotalIncome = totalIncome,
            };
            return View(model);
        }

        public async Task<IActionResult> AddIncome()
        {
            var categories =await _incomeServices.GetIncomeCategories();
            ViewBag.Categories = new SelectList(categories.Categories, "categoryId", "category_name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddIncome(AddIncomeVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var income = new Incomes
                {
                    categoty_id = model.Category,
                    income_amount = model.Amount,
                    income_date = model.Date,
                    comment = model.Comment,
                    user = user
                };

                try
                {
                    await _incomeServices.AddIncome(income);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return Json(model);
                }
            }
            return Json(model);
        }

        public async Task<IActionResult> EditIncome(Guid incomeId)
        {
            var categories = await _incomeServices.GetIncomeCategories();
            ViewBag.Categories = new SelectList(categories.Categories, "categoryId", "category_name");
            var income=await _incomeServices.GetIncomeById(incomeId);
            if (income != null)
            {
                var model = new EditIncomeVM
                {
                    Category = income.categoty_id,
                    Date = income.income_date,
                    Amount = income.income_amount,
                    Comment = income.comment,
                    IncomeId=income.incomeId
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditIncome(EditIncomeVM model)
        {
            var userId = _userManager.GetUserId(User);
            var user=await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var income = new Incomes
                {
                    categoty_id = model.Category,
                    income_amount = model.Amount,
                    income_date = model.Date,
                    comment = model.Comment,
                    user = user,
                    incomeId=model.IncomeId
                };
                try
                {
                    await _incomeServices.UpdateIncome(income);
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            var income = await _incomeServices.GetIncomeById(incomeId);
            if (income != null)
            {
                var model = new DeleteIncomeVM
                {
                    Category = _incomeServices.GetCategoryNameById(income.categoty_id),
                    Date = income.income_date,
                    Amount = income.income_amount,
                    Comment = income.comment,
                    IncomeId = income.incomeId
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteIncome(DeleteIncomeVM model)
        {
            await _incomeServices.DeleteIncome(model.IncomeId);
            return RedirectToAction(nameof(Index));
        }
    }
}
