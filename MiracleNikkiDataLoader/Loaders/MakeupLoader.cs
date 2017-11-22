using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class MakeupLoader : LoaderBase
    {
        public MakeupLoader() : base("https://miraclenikki.gamerch.com/%E3%83%A1%E3%82%A4%E3%82%AF") { }
    }
}
