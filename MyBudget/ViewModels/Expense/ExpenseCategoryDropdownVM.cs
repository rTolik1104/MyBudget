using MyBudget.Models;

namespace MyBudget.ViewModels.Expense
{
    public class ExpenseCategoryDropdownVM
    {
        public ExpenseCategoryDropdownVM()
        {
            Categories = new List<ExpenseCategories>();
        }

        public List<ExpenseCategories> Categories { get; set; }
    }
}
