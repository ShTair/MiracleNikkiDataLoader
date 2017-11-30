using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class HairstyleLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "0_Hairstyle";

        public HairstyleLoader() : base("https://miraclenikki.gamerch.com/%E3%83%98%E3%82%A2%E3%82%B9%E3%82%BF%E3%82%A4%E3%83%AB") { }
    }
}
