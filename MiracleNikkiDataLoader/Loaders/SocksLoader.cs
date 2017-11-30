using System.ComponentModel.Composition;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class SocksLoader : LoaderBase, ILoader
    {
        public string Name { get; } = "5_Socks";

        public SocksLoader() : base("https://miraclenikki.gamerch.com/%E9%9D%B4%E4%B8%8B", new string[] { "靴下", "靴下・ガーター" }) { }
    }
}
