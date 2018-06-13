using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class MakeupLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "8_Makeup";
        protected override int TypeCode { get; } = 8;

        public MakeupLoader() : base("https://miraclenikki.gamerch.com/%E3%83%A1%E3%82%A4%E3%82%AF") { }
    }
}
