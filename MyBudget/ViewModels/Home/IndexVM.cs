namespace MyBudget.ViewModels.Home
{
    public class IndexVM
    {
        public decimal? TotalIncome { get; set; }
        public decimal? TotalOutcome { get; set; }
        public HistoryVM? LastTransactions { get; set; }
    }
}
