﻿using AngleSharp;
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

        public LoaderBase(string url) : this(url, kindGetter: null) { }

        public LoaderBase(string url, string kind) : this(url, _ => kind) { }

        public LoaderBase(string url, string[] kinds) : this(url, i => kinds[i]) { }

        public LoaderBase(string url, Func<int, string> kindGetter)
        {
            _url = url;
            _kindGetter = kindGetter;
        }

        public virtual async Task<IEnumerable<Item>> LoadItems()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var doc = await BrowsingContext.New(config).OpenAsync(_url);
            return GetItems(doc);
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
                        Kind = _kindGetter?.Invoke(i) ?? datas[0],
                        Rarity = datas[3].Substring(1),
                        P11 = datas[4].ToUpper(),
                        P12 = datas[5].ToUpper(),
                        P21 = datas[6].ToUpper(),
                        P22 = datas[7].ToUpper(),
                        P31 = datas[8].ToUpper(),
                        P32 = datas[9].ToUpper(),
                        P41 = datas[10].ToUpper(),
                        P42 = datas[11].ToUpper(),
                        P51 = datas[12].ToUpper(),
                        P52 = datas[13].ToUpper(),
                        Tags = (datas[14] + " " + datas[15]).Trim(),
                    };
                }
            }
        }
    }
}
