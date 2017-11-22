using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class DressLoader : LoaderBase
    {
        public DressLoader() : base("https://miraclenikki.gamerch.com/%E3%83%89%E3%83%AC%E3%82%B9") { }
    }
}
