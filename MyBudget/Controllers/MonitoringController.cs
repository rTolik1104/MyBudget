using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Data.Interfaces;
using MyBudget.Models;
using MyBudget.ViewModels.Monitoring;

namespace MyBudget.Controllers
{
    public class MonitoringController : Controller
    {
        private readonly IIncomeServices _incomeServices;
        private readonly IExpenseServices _expenseServices;
        private readonly UserManager<Users> _userManager;

        public MonitoringController(IIncomeServices incomeServices, IExpenseServices expenseServices, UserManager<Users> userManager)
        {
            _expenseServices = expenseServices;
            _userManager = userManager;
            _incomeServices = incomeServices;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var totalIncome = _incomeServices.GetTotalUserIncome(userId);
            var totalExpense=_expenseServices.GetTotalUserExpense(userId);
            var total = totalIncome - totalExpense;

            var model = new MonitoringVM
            {
                ResultTotal = total,
                TotalIncomeSalary = await _incomeServices.GetTotalIncomeByCategory(userId, new Guid("d193bfb8-9f8a-4f0f-9c43-28cb6383e2fb")),
                TotalIncomeRent = await _incomeServices.GetTotalIncomeByCategory(userId, new Guid("1d972406-cb78-4a5a-b37b-b152185173ca")),
                TotalIncomeOther = await _incomeServices.GetTotalIncomeByCategory(userId, new Guid("9cdb960c-25d9-4cb2-a91a-25778d40ce14")),
                TotalExpenseFood = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("ec293cc3-ce30-42ca-9048-f6425da66100")),
                TotalExpenseTransport = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("3355751f-0ec0-43ed-b791-6eac737f008b")),
                TotalExpenseHangOut = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("c7d1153f-5b05-4cf0-8707-0119b37f3e36")),
                TotalExpenseInternet = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("406e5e34-119d-4140-b2e5-f9ed51264e27")),
                TotalExpenseMobile = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("c9f5f4d5-e98b-46f9-9845-c7eb8a1cd00e")),
                TotalExpenseOther = await _expenseServices.GetTotalIncomeByCategory(userId, new Guid("efd1cc51-2a97-42ca-9d9b-a70334b0c965"))
            };
            return View(model);
        }
    }
}
