namespace MyBudget.ViewModels.Income
{
    public class DeleteIncomeVM
    {
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public Guid IncomeId { get; set; }
    }
}
