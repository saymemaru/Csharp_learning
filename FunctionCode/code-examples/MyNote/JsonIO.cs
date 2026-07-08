using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote
{
    public static class JsonIO
    {
        public static void Store<T>(IEnumerable<T> list, string savePath)
        {
            if (list == null) return;

            var settings = new JsonSerializerSettings
            {
                // 在 JSON 中嵌入类型信息
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.None
            };
            string json = JsonConvert.SerializeObject(list, settings);
            File.WriteAllText(savePath, json);
        }

        public static ImmutableList<T> Load<T>(string savePath)
        {
            if (!File.Exists(savePath)) return ImmutableList<T>.Empty;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            string json = File.ReadAllText(savePath);
            ImmutableList<T> output = JsonConvert.DeserializeObject<ImmutableList<T>>(json, settings) ?? ImmutableList<T>.Empty;

            if (output == null) return ImmutableList<T>.Empty;

            return output;

        }
    }
}
