using MyBudget.ViewModels.Expense;
using MyBudget.ViewModels.Income;

namespace MyBudget.ViewModels.Home
{
    public class HistoryVM
    {
        public List<IncomeVM>? Incomes { get; set; }
        public List<ExpenseVM>? Expenses { get; set; }
    }
}
