using System.ComponentModel.DataAnnotations;

namespace MyBudget.Models
{
    public class Expenses
    {
        [Key]
        public Guid expenseId { get; set; }
        public Guid? categoty_id { get; set; }
        public decimal? expense_amount { get; set; }
        public DateTime? expense_date { get; set; }
        public string? comment { get; set; }

        public Users? user { get; set; }
    }
}
