namespace MyBudget.ViewModels.Expense
{
    public class ExpensesListVM
    {
        public List<ExpenseVM>? Expenses { get; set; }
        public decimal? Total { get; set; }
    }
}
