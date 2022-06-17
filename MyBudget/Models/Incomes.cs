using System.ComponentModel.DataAnnotations;

namespace MyBudget.Models
{
    public class Incomes
    {
        [Key]
        public Guid incomeId { get; set; }
        public Guid? categoty_id { get; set; }
        public decimal? income_amount { get; set; }
        public DateTime? income_date { get; set; }
        public string? comment { get; set; }

        public Users? user { get; set; }
    }
}
