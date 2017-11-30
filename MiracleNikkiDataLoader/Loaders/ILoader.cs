using MiracleNikkiDataLoader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader.Loaders
{
    interface ILoader
    {
        string Name { get; }

        Task<IEnumerable<Item>> LoadItems();
    }
}
