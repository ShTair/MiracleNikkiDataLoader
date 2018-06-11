using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiracleNikkiDataLoader
{
    class JintEngine
    {
        private Engine _engine;

        public JintEngine()
        {
            _engine = new Engine();
        }

        public async Task Load(string target)
        {
            var js = await LoadString(target);
            _engine.Execute(js);
        }

        private async Task<string> LoadString(string target)
        {
            using (var hc = new HttpClient())
            {
                return await hc.GetStringAsync(target);
            }
        }

        public List<T> GetObject<T>(string name, Func<object, T> converter)
        {
            object[] o = GetObject(name);
            return o.Select(converter).ToList();
        }

        public dynamic GetObject(string name)
        {
            return _engine.Global.GetProperty(name).Value.ToObject();
        }
    }
}
