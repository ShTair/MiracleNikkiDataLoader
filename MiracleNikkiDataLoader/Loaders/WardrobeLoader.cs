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
        public async Task<List<Item>> Load()
        {
            var target = "http://seal.coding.me/qjnn/data/wardrobe.js";
            var js = await Load(target);

            var engine = new Engine();
            engine.Execute(js);

            var wardrobe = GetObject(engine, "wardrobe", ConvertToItem);
            return wardrobe;
        }

        private Item ConvertToItem(dynamic src)
        {
            var item = new Item
            {
                Id = int.Parse((string)src[2]),
                Name = "？",
                Type = TranslateType((string)src[1]),
                Stars = int.Parse((string)src[3]),

                Gorgeous = (string)src[4],
                Simple = (string)src[5],
                Elegant = (string)src[6],
                Active = (string)src[7],
                Mature = (string)src[8],
                Cute = (string)src[9],
                Sexy = (string)src[10],
                Pure = (string)src[11],
                Cool = (string)src[12],
                Warm = (string)src[13],
            };

            if (item.Type != "？")
            {
                item.Extra = string.Join(" ", ((string)src[14]).Split('/').Select(TranslateExtra));
            }

            return item;
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

                case "萤光之灵": return "？";
            }

            throw new Exception();
        }

        private string TranslateExtra(string type)
        {
            switch (type)
            {
                case "": return "";
                case "中性风": return "中性風";
                case "大小姐": return "お嬢様";
                case "欧式古典": return "中世ヨーロッパ風";
                case "中式古典": return "クラシックチャイナ";
                case "舞者": return "ダンサー";
                case "波西米亚": return "ボヘミアン";
                case "摇滚风": return "ロック";
                case "和风": return "和風";
                case "医务使者": return "メディカル";
                case "OL": return "OL";
                case "英伦": return "英国風";
                case "哥特风": return "ゴシック";
                case "洛丽塔": return "ロリータ";
                case "童话系": return "メルヘン";
                case "中式现代": return "モダンチャイナ";
                case "民族风": return "エスニック";
                case "军装": return "ミリタリー";
                case "未来系": return "近未来";
                case "小动物": return "小動物";
                case "原宿系": return "原宿系";
                case "森女系列": return "森ガール";
                case "女神系": return "女神";
                case "围裙": return "エプロン";
                case "晚礼服": return "フォーマル";
                case "碎花": return "小花柄";
                case "女仆装": return "メイド";
                case "民国服饰": return "レトロチャイナ";
                case "旗袍": return "チャイナドレス";
                case "婚纱": return "ウェディング";
                case "防晒": return "UV対策";
                case "居家服": return "部屋着";
                case "牛仔布": return "デニム";
                case "睡衣": return "ナイトウェア";
                case "沐浴": return "バスタイム";
                case "冬装": return "冬服";
                case "兔女郎": return "バニー";
                case "学院系": return "学生風";
                case "泳装": return "水着";
                case "海军风": return "ネイビー";
                case "运动系": return "スポーティ";
                case "侠客联盟": return "ワル";
                case "雨季装备": return "雨の日";
                case "pop": return "POP";
                case "印度服饰": return "インド風";
            }

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
