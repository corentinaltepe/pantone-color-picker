using System.Collections.Generic;
using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace PantoneColorPicker.Services
{
    /// <summary>
    /// Read the resources and returns the list of PantoneColors from the Uncoated Catalog
    /// </summary>
    class PantoneUncoatedCatalogReader : IPantoneColorCatalog
    {
        public IEnumerable<PantoneColor> _colors;
        public IEnumerable<PantoneColor> Colors
        {
            get
            {
                // Eviter de parser le fichier de multiples fois
                if (_colors == null)
                    _colors = ReadColorCatalog();

                return _colors;
            }
        }

        private static List<PantoneColor> ReadColorCatalog()
        {
            var text = PantoneColorPicker.Properties.Resources.pantoneUncoatedJson;
            var catalog = ((JObject)JsonConvert.DeserializeObject(text)).Children().First().Children().First();

            List<PantoneColor> Colors = new List<PantoneColor>();
            foreach (var color in catalog.Children())
                Colors.Add(ParsePantoneColorJson(color));

            return Colors;
        }
        private static PantoneColor ParsePantoneColorJson(JToken json)
        {
            var color = new PantoneColor
            {
                // Parse the strings
                Name = json["name"].Value<string>(),
                Hex = json["hex"].Value<string>(),
                Websafe = json["websafe"].Value<string>()
            };

            // Parse the more complexe structures
            var rgb = json["rgb"];
            color.RGB = new RGB()
            {
                R = rgb["r"].Value<byte>(),
                G = rgb["g"].Value<byte>(),
                B = rgb["b"].Value<byte>()
            };

            var hsl = json["hsl"];
            color.HSL = new HSL()
            {
                H = hsl["h"].Value<int>(),
                S = hsl["s"].Value<int>(),
                L = hsl["l"].Value<int>()
            };

            var hsb = json["hsb"];
            color.HSB = new HSB()
            {
                H = hsb["h"].Value<int>(),
                S = hsb["s"].Value<int>(),
                B = hsb["b"].Value<int>()
            };

            var cmyk = json["cmyk"];
            color.CMYK = new CMYK()
            {
                C = cmyk["c"].Value<byte>(),
                M = cmyk["m"].Value<byte>(),
                Y = cmyk["y"].Value<byte>(),
                K = cmyk["k"].Value<byte>()
            };

            return color;
        }
    }
}
