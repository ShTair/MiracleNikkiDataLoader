using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class CoatLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "2_Coat";

        public CoatLoader() : base("https://miraclenikki.gamerch.com/%E3%82%B3%E3%83%BC%E3%83%88") { }
    }
}
