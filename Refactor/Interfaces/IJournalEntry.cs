using LanguageExt;

namespace AsyncAndSyncExample.Interfaces
{
    public interface IJournalEntry
    {
        int Id { get; set; }

        public short Month { get; set; }

        public int Year { get; set; }

        string Name { get; set; }

        decimal Cash { get; set; }

        decimal Expense { get; set; }

        Option<Dictionary<string, decimal>> OtherPayments { get; set; }
    }
}
