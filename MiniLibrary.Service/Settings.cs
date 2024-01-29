using MiniLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniLibrary.Service
{
    public static class Settings
    {
        public static AppSettings Get()
        {
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("appsettings.json"), options);
        }
    }
}
