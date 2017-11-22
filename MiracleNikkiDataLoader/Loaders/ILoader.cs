using MiracleNikkiDataLoader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader.Loaders
{
    interface ILoader
    {
        Task<IEnumerable<Item>> LoadItems();
    }
}
