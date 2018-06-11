using CsvHelper.Configuration;
using System;

namespace MiracleNikkiDataLoader.Models
{
    class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Stars { get; set; }

        public string Gorgeous { get; set; }
        public string Simple { get; set; }

        public string Elegant { get; set; }
        public string Active { get; set; }

        public string Mature { get; set; }
        public string Cute { get; set; }

        public string Sexy { get; set; }
        public string Pure { get; set; }

        public string Cool { get; set; }
        public string Warm { get; set; }

        public string Extra { get; set; }

        public sealed class Map : ClassMap<Item>
        {
            public Map()
            {
                Map(m => m.Id).Index(0);
                Map(m => m.Name).Index(1);
                Map(m => m.Type).Index(2);
                Map(m => m.Stars).Index(3);
                Map(m => m.Gorgeous).Index(4);
                Map(m => m.Simple).Index(5);
                Map(m => m.Elegant).Index(6);
                Map(m => m.Active).Index(7);
                Map(m => m.Mature).Index(8);
                Map(m => m.Cute).Index(9);
                Map(m => m.Sexy).Index(10);
                Map(m => m.Pure).Index(11);
                Map(m => m.Cool).Index(12);
                Map(m => m.Warm).Index(13);
                Map(m => m.Extra).Index(14);
            }
        }

        public static int GetKindId(string kind)
        {
            switch (kind)
            {
                case "ヘアスタイル": return 1;
                case "ドレス": return 2;
                case "コート": return 3;
                case "トップス": return 4;
                case "ボトムス": return 5;
                case "靴下": return 6;
                case "靴下・ガーター": return 7;
                case "シューズ": return 8;
                case "アクセサリー": return 9;

                case "ヘアアクセサリー": return 10;
                case "ヘッドアクセ": return 11;
                case "ヴェール": return 12;
                case "カチューシャ": return 13;

                case "つけ耳": return 14;
                case "耳飾り": return 15;

                case "首飾り": return 16;
                case "マフラー": return 17;
                case "ネックレス": return 18;

                case "腕飾り": return 19;
                case "右手飾り": return 20;
                case "左手飾り": return 21;
                case "手袋": return 22;

                case "手持品": return 23;
                case "右手持ち": return 24;
                case "左手持ち": return 25;
                case "両手持ち": return 26;

                case "腰飾り": return 27;

                case "特殊": return 28;
                case "フェイス": return 29;
                case "ボディ": return 30;
                case "タトゥー": return 31;
                case "羽根": return 32;
                case "しっぽ": return 33;
                case "前景": return 34;
                case "後景": return 35;
                case "吊り": return 36;
                case "床": return 37;
                case "肌": return 38;

                case "メイク": return 39;
            }

            throw new Exception();
        }

        public static double GetRate(int kindId)
        {
            switch (kindId)
            {
                case 1: return 5;

                case 2: return 20;

                case 3: return 2;

                case 4: return 10;

                case 5: return 10;

                case 6:
                case 7: return 3;

                case 8: return 4;

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38: return 2;

                case 39: return 1;
            }

            throw new Exception();
        }
    }
}
