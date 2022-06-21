using Newtonsoft.Json;

namespace AsyncAndSyncExample.Extensions
{
    public static class Extensions
    {
        public static void Dump(this object obj)
        {
            Console.WriteLine(obj.DumpString());
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in enumerable)
                handler(item, idx++);
        }

        private static string DumpString(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
