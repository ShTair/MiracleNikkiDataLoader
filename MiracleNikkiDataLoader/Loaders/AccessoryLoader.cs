using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MiracleNikkiDataLoader.Models;
using System.Linq;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    class AccessoryLoader : ILoader
    {
        public string Name { get; } = "7_Accessory";

        public AccessoryLoader() { }

        public async Task<IEnumerable<Item>> LoadItems()
        {
            var loaders = new List<LoaderBase> { new HairAccessoryLoader(), new EarAccessoryLoader(), new NeckAccessoryLoader(), new WristAccessoryLoader(), new BelongingsLoader(), new WaistAccessoryLoader(), new OtherAccessoryLoader() };
            var tasks = loaders.Select(loader => loader.LoadItems());
            return (await Task.WhenAll(tasks)).SelectMany(t => t);
        }
    }

    sealed class HairAccessoryLoader : LoaderBase
    {
        public HairAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A0%AD", new string[] { "ヘッドアクセ", "ヴェール", "カチューシャ", "つけ耳" }) { }
    }

    sealed class EarAccessoryLoader : LoaderBase
    {
        public EarAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%80%B3", "耳飾り") { }
    }

    sealed class NeckAccessoryLoader : LoaderBase
    {
        public NeckAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A6%96", new string[] { "ネックレス", "マフラー" }) { }
    }

    sealed class WristAccessoryLoader : LoaderBase
    {
        public WristAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%95", new string[] { "右手飾り", "左手飾り", "手袋" }) { }
    }

    sealed class BelongingsLoader : LoaderBase
    {
        public BelongingsLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E6%89%8B", new string[] { "右手持ち", "左手持ち", "両手持ち" }) { }
    }

    sealed class WaistAccessoryLoader : LoaderBase
    {
        public WaistAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%B0", "腰飾り") { }
    }

    sealed class OtherAccessoryLoader : LoaderBase
    {
        public OtherAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E7%89%B9%E6%AE%8A", new string[] { "フェイス", "ボディ", "タトゥー", "羽根", "しっぽ", "前景", "後景", "吊り", "床", "肌" }) { }
    }
}
