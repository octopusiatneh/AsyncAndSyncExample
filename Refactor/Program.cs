using AsyncAndSyncExample.Extensions;
using AsyncAndSyncExample.Models;
using AsyncAndSyncExample.Services;
using LanguageExt;
using System.Diagnostics;
using static LanguageExt.Prelude;
using static System.Console;

var elasticSearchService = new ElasticSearchService<JournalEntry>();
var periods = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
#region Asynchronous
WriteLine("Asynchronous performance elapsed...");
var sw = new Stopwatch();
sw.Start();
var journalEntriesAsyncResult = await Task.WhenAll(
    periods
        .Select(month => elasticSearchService
            .GetWhereAsync(je => je.Month == month && je.ClientId == 1)
        )
    );

journalEntriesAsyncResult
    .SelectMany(taskResponse => taskResponse)
    .SelectMany(data => data)
    .Dump();

sw.Stop();
WriteLine();
WriteLine($"Time: {sw.Elapsed}");
WriteLine();
#endregion

WriteLine(string.Empty.PadLeft(65, '-'));

#region Synchronous
WriteLine("Synchronous performance elapsed...");
sw.Start();
var journalEntriesSyncResult = periods.Aggregate(new Seq<JournalEntry>(), (accumulated, month) =>
{
    Func<JournalEntry, bool> predicate = je => je.Month == month && je.ClientId == 1;
    accumulated = elasticSearchService
        .GetWhere(predicate)
        .Match(
            Some: jeOfCurrentMonth => accumulated
                    .ConcatFast(jeOfCurrentMonth)
                    .ToSeq(),
            None: () => accumulated
        );

    return accumulated;
});

journalEntriesSyncResult.Dump();

sw.Stop();
WriteLine();
WriteLine($"Time: {sw.Elapsed}");
WriteLine();
#endregion

WriteLine(string.Empty.PadLeft(65, '-'));
Write("Press any key to exit...");
Console.ReadLine();