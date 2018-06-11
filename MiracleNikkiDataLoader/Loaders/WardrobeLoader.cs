using System.Net.Http;
using System.Threading.Tasks;
using Jint;

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

            var wardrobe = GetObject(engine, "wardrobe");
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
