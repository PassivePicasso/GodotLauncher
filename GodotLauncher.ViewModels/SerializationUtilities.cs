using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GodotLauncher.ViewModels
{
    public static class SerializationUtilities
    {
        public readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        public static T Load<T>(string path) where T : new()
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var deserialized = JsonSerializer.Deserialize<T>(json, JsonOptions);
                    if (deserialized != null)
                        return deserialized;
                }
            }
            return new T();
        }
        public static void Populate<T>(string path, T instance)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var deserialized = JsonSerializer.Deserialize<T>(json, JsonOptions);
                    if (deserialized != null)
                        foreach (var property in typeof(T).GetProperties())
                            if (property.CanWrite && property.CanRead)
                                property.SetValue(instance, property.GetValue(deserialized, null), null);
                }
            }
        }

        public static void Save(string path, object data)
        {
            var dir = Path.GetDirectoryName(path);
            if (dir == null) return;
            Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(data, JsonOptions);
            File.WriteAllText(path, json);
        }
    }
}
