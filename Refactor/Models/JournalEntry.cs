using AsyncAndSyncExample.Interfaces;
using LanguageExt;

namespace AsyncAndSyncExample.Models
{
    public class JournalEntry : IJournalEntry
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public short Month { get; set; }

        public int Year { get; set; }

        public string? Name { get; set; }

        public decimal Cash { get; set; }

        public decimal Expense { get; set; }

        public Option<Dictionary<string, decimal>> OtherPayments { get; set; }
    }
}
