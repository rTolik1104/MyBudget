using MyBudget.Models;

namespace MyBudget.ViewModels.Income
{
    public class IncomeCategoryDropdownVM
    {
        public IncomeCategoryDropdownVM()
        {
            Categories = new List<IncomeCategories>();
        }

        public List<IncomeCategories> Categories { get; set; }
    }
}
