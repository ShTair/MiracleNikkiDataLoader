using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jint;
using MiracleNikkiDataLoader.Models;

namespace MiracleNikkiDataLoader.Loaders
{
    class WardrobeLoader
    {
        public async Task Load()
        {
            var target = "http://seal.coding.me/qjnn/data/wardrobe.js";
            var js = await Load(target);

            var engine = new Engine();
            engine.Execute(js);

            var wardrobe = GetObject(engine, "wardrobe", ConvertToItem);

        }

        private Item ConvertToItem(dynamic src)
        {
            return new Item
            {
                Id = int.Parse((string)src[2]),
                Type = TranslateType((string)src[1]),
            };
        }

        private string TranslateType(string type)
        {
            switch (type)
            {
                case "发型": return "ヘアスタイル";
                case "连衣裙": return "ドレス";
                case "外套": return "コート";
                case "上装": return "トップス";
                case "下装": return "ボトムス";
                case "袜子-袜子": return "靴下";
                case "袜子-袜套": return "靴下・ガーター";
                case "鞋子": return "シューズ";

                case "饰品-头饰·发饰": return "ヘッドアクセ";
                case "饰品-头饰·头纱": return "ヴェール";
                case "饰品-头饰·发卡": return "カチューシャ";

                case "饰品-头饰·耳朵": return "つけ耳";
                case "饰品-耳饰": return "耳飾り";

                case "饰品-颈饰·围巾": return "マフラー";
                case "饰品-颈饰·项链": return "ネックレス";

                case "饰品-手饰·右": return "右手飾り";
                case "饰品-手饰·左": return "左手飾り";

                case "饰品-手饰·双": return "手袋";

                case "饰品-手持·右": return "右手持ち";
                case "饰品-手持·左": return "左手持ち";
                case "饰品-手持·双": return "両手持ち";

                case "饰品-腰饰": return "腰飾り";

                case "饰品-特殊·面饰": return "フェイス";
                case "饰品-特殊·胸饰": return "ボディ";
                case "饰品-特殊·纹身": return "タトゥー";
                case "饰品-特殊·翅膀": return "羽根";
                case "饰品-特殊·尾巴": return "しっぽ";
                case "饰品-特殊·前景": return "前景";
                case "饰品-特殊·后景": return "後景";
                case "饰品-特殊·顶饰": return "吊り";
                case "饰品-特殊·地面": return "床";

                case "饰品-皮肤": return "肌";

                case "妆容": return "メイク";

                case "萤光之灵": return "?";
            }

            return null;
            throw new Exception();
        }

        private List<T> GetObject<T>(Engine engine, string name, Func<object, T> converter)
        {
            object[] o = GetObject(engine, name);
            return o.Select(converter).ToList();
        }

        private dynamic GetObject(Engine engine, string name)
        {
            return engine.Global.GetProperty(name).Value.ToObject();
        }

        private async Task<string> Load(string target)
        {
            using (var hc = new HttpClient())
            {
                return await hc.GetStringAsync(target);
            }
        }
    }
}
