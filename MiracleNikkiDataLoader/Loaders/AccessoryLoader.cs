using MiracleNikkiDataLoader.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader.Loaders
{
    [Export(typeof(ILoader))]
    class AccessoryLoader : ILoader
    {
        public string Name { get; } = "7_Accessory";

        public AccessoryLoader() { }

        public async Task<IEnumerable<Item>> LoadItems(List<Item> wardrobe)
        {
            var loaders = new List<LoaderBase> { new HairAccessoryLoader(), new EarAccessoryLoader(), new NeckAccessoryLoader(), new WristAccessoryLoader(), new BelongingsLoader(), new WaistAccessoryLoader(), new OtherAccessoryLoader() };
            var tasks = loaders.Select(async loader =>
            {
                var types = loader.GetType().GetProperty("Types", BindingFlags.Static | BindingFlags.Public).GetValue(null) as string[];
                Dictionary<int, Item> wr;
                lock (wardrobe)
                {
                    wr = wardrobe.Where(t => types.Contains(t.Type)).ToDictionary(t => t.Id);
                }

                return await loader.LoadItems(wr);
            });
            return (await Task.WhenAll(tasks)).SelectMany(t => t);
        }
    }

    sealed class HairAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "ヘッドアクセ", "ヴェール", "カチューシャ", "つけ耳" };
        protected override int TypeCode { get; } = 7;

        public HairAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A0%AD", Types) { }
    }

    sealed class EarAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "耳飾り" };
        protected override int TypeCode { get; } = 7;

        public EarAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%80%B3", Types) { }
    }

    sealed class NeckAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "ネックレス", "マフラー" };
        protected override int TypeCode { get; } = 7;

        public NeckAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A6%96", Types) { }
    }

    sealed class WristAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "右手飾り", "左手飾り", "手袋" };
        protected override int TypeCode { get; } = 7;

        public WristAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%95", Types) { }
    }

    sealed class BelongingsLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "右手持ち", "左手持ち", "両手持ち" };
        protected override int TypeCode { get; } = 7;

        public BelongingsLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E6%89%8B", Types) { }
    }

    sealed class WaistAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "腰飾り" };
        protected override int TypeCode { get; } = 7;

        public WaistAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%B0", Types) { }
    }

    sealed class OtherAccessoryLoader : LoaderBase
    {
        public static string[] Types { get; } = new string[] { "フェイス", "ボディ", "タトゥー", "羽根", "しっぽ", "前景", "後景", "吊り", "床", "肌" };
        protected override int TypeCode { get; } = 7;

        public OtherAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E7%89%B9%E6%AE%8A", Types) { }
    }
}
