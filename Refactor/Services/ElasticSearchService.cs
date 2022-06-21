using AsyncAndSyncExample.Models;
using LanguageExt;

namespace AsyncAndSyncExample.Services
{
    public class ElasticSearchService<T>
        where T : JournalEntry
    {
        private readonly DbContext<T> _dbContext = new();
        public async Task<Option<Seq<T>>> Get()
        {
            await Task.Delay(3000);

            return _dbContext.JournalEntries;
        }

        public async Task<Option<T>> GetById(int id)
        {
            await Task.Delay(1000);

            return _dbContext.JournalEntries
                    .FindSeq(je => je.Id == id)
                    .FirstOrDefault();
        }

        public async Task<Option<Seq<T>>> GetWhereAsync(Func<T, bool> predicate)
        {
            await Task.Delay(1000);

            return _dbContext.JournalEntries
                .Filter(predicate);
        }

        public Option<Seq<T>> GetWhere(Func<T, bool> predicate)
        {
            Thread.Sleep(1000);

            return _dbContext.JournalEntries
                .Where(predicate);
        }
    }
}
