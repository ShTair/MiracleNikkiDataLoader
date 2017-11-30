using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class TopsLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "3_Tops";

        public TopsLoader() : base("https://miraclenikki.gamerch.com/%E3%83%88%E3%83%83%E3%83%97%E3%82%B9") { }
    }
}
