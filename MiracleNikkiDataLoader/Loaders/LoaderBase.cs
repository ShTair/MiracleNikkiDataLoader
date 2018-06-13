using AngleSharp;
using AngleSharp.Dom;
using MiracleNikkiDataLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader.Loaders
{
    abstract class LoaderBase
    {
        private string _url;
        private Func<int, string> _kindGetter;

        abstract protected int TypeCode { get; }

        public LoaderBase(string url) : this(url, kindGetter: null) { }

        public LoaderBase(string url, string kind) : this(url, _ => kind) { }

        public LoaderBase(string url, string[] kinds) : this(url, i => kinds[i]) { }

        public LoaderBase(string url, Func<int, string> kindGetter)
        {
            _url = url;
            _kindGetter = kindGetter;
        }

        public virtual async Task<IEnumerable<Item>> LoadItems(List<Item> wardrobe)
        {
            Dictionary<int, Item> wr;
            lock (wardrobe)
            {
                wr = wardrobe.Where(t => Item.GetTypeCode(t.Type) == TypeCode).ToDictionary(t => t.Id);
            }

            return await LoadItems(wr);
        }

        public virtual async Task<IEnumerable<Item>> LoadItems(Dictionary<int, Item> wardrobe)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var doc = await BrowsingContext.New(config).OpenAsync(_url);

            foreach (var item in GetItems(doc))
            {
                Item ti;
                if (wardrobe.TryGetValue(item.Id, out ti)) ti.Name = item.Name;
                else wardrobe.Add(item.Id, item);
            }

            return wardrobe.Values;
        }

        protected virtual IEnumerable<Item> GetItems(IDocument doc)
        {
            var tables = doc.QuerySelectorAll("section#js_async_main_column_text table");
            for (int i = 0; i < tables.Length; i++)
            {
                var trs = tables[i].QuerySelectorAll("tbody tr");
                foreach (var tr in trs)
                {
                    var datas = tr.Children.Select(t => t.TextContent).ToArray();
                    var id = int.Parse(datas[1]);
                    yield return new Item
                    {
                        Id = id,
                        Name = datas[2].Replace("（", "(").Replace("）", ")"),
                        Type = _kindGetter?.Invoke(i) ?? datas[0],
                        Stars = int.Parse(datas[3].Substring(1)),
                        Gorgeous = datas[4].ToUpper(),
                        Simple = datas[5].ToUpper(),
                        Elegant = datas[6].ToUpper(),
                        Active = datas[7].ToUpper(),
                        Mature = datas[8].ToUpper(),
                        Cute = datas[9].ToUpper(),
                        Sexy = datas[10].ToUpper(),
                        Pure = datas[11].ToUpper(),
                        Warm = datas[12].ToUpper(),
                        Cool = datas[13].ToUpper(),
                        Extra = (datas[14] + " " + datas[15]).Trim(),
                    };
                }
            }
        }
    }
}
