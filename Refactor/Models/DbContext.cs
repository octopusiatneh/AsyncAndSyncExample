using Bogus;
using LanguageExt;

namespace AsyncAndSyncExample.Models
{
    public class DbContext<T>
        where T : JournalEntry
    {
        public DbContext()
        {
            JournalEntries = InitDatabase();
        }

        public DbContext(Seq<T> journalEntries)
        {
            JournalEntries = journalEntries;
        }

        public Seq<T> JournalEntries { get; private set; }

        private Seq<T> InitDatabase()
        {
            Randomizer.Seed = new Random(100); // For generate same dataset

            var count = 12;
            var faker = new Faker();

            var names = Enumerable.Range(0, count)
                .Select(_ => faker.Commerce.ProductAdjective())
                .Distinct();

            var values = Enumerable.Range(0, count)
                .Select(_ => faker.Random.Decimal(100, 200_000))
                .Distinct();

            List<Dictionary<string, decimal>> keyValuePairs = new()
            {
                new Dictionary<string, decimal>() { { "KMS-Other", 1000 } },
                new Dictionary<string, decimal>() { { "LQ-Other", 2000 } },
                new Dictionary<string, decimal>() { { "Sisu-Other", 3000 } },
                new Dictionary<string, decimal>() { { "Seal-Other", 4000 } },
                new Dictionary<string, decimal>() { { "CJE-Other", 5000 } },
                new Dictionary<string, decimal>() { { "Party parrot-Other", 6000 } },
            };

            var otherPayments = names                .Zip(values, (name, value) => KeyValuePair.Create(name, value))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var clientIds = new[] { 1, 2 };

            var journalEntries = new Faker<T>()
                .RuleFor(je => je.Id, f => f.IndexFaker + 1)
                .RuleFor(je => je.ClientId, f => f.PickRandom(clientIds))
                .RuleForType(typeof(decimal), f => f.Random.Decimal(10_000, 500_000))
                .RuleFor(je => je.Month, f => f.Random.Short(1, 12))
                .RuleFor(je => je.Year, f => 2019)
                .RuleFor(je => je.Name, f => $"{f.Company.CompanySuffix()} - {f.Company.CompanyName()}")
                .RuleFor(je => je.OtherPayments, f => f.PickRandom(keyValuePairs).OrNull(f, 0.9f))
                .Generate(12)
                .ToSeq();

            return journalEntries;
        }
    }
}
