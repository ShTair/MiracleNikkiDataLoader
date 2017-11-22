using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class ShoesLoader : LoaderBase
    {
        public ShoesLoader() : base("https://miraclenikki.gamerch.com/%E3%82%B7%E3%83%A5%E3%83%BC%E3%82%BA") { }
    }
}
