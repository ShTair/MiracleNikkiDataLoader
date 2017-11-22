using CsvHelper;
using MiracleNikkiDataLoader.Loaders;
using MiracleNikkiDataLoader.Models;
using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args[0]).Wait();
        }

        private static async Task Run(string dir)
        {
            var sem = new SemaphoreSlim(1);

            var catalog = new ApplicationCatalog();
            var container = new CompositionContainer(catalog);
            var loaders = container.GetExportedValues<ILoader>();

            var tasks = loaders.Select(loader => Task.Run(async () =>
            {
                var items = (await loader.LoadItems()).OrderBy(t => t.Id).ToList();
                var name = loader.GetType().Name;
                var path = Path.Combine(dir, $"{name.Remove(name.Length - 6)}.csv");

                try
                {
                    await sem.WaitAsync();
                    using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
                    {
                        using (var csv = new CsvWriter(writer))
                        {
                            csv.Configuration.RegisterClassMap<Item.Map>();
                            csv.Configuration.HasHeaderRecord = false;
                            csv.WriteRecords(items);
                        }
                    }
                }
                catch (Exception exp) { Console.WriteLine(exp); }
                finally
                {
                    sem.Release();
                }
            }));

            await Task.WhenAll(tasks);
        }
    }
}
