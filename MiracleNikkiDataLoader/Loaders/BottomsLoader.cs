using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class BottomsLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "4_Bottoms";
        protected override int TypeCode { get; } = 4;

        public BottomsLoader() : base("https://miraclenikki.gamerch.com/%E3%83%9C%E3%83%88%E3%83%A0%E3%82%B9") { }
    }
}
