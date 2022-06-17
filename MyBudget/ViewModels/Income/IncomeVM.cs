namespace MyBudget.ViewModels.Income
{
    public class IncomeVM
    {
        public string? Category { get; set; }
        public decimal? Amout { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public Guid IncomeId { get; set; }
    }
}
