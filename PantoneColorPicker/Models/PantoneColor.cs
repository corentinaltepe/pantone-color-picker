using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PantoneColorPicker.Models
{
    public class PantoneColor
    {
        /// <summary>
        /// List containing all PantoneColors of the catalog
        /// </summary>
        public static readonly List<PantoneColor> PantoneCatalog;

        #region Properties
        private string name;
        /// <summary>
        /// Pantone color code name (or reference)
        /// </summary>
        public string Name
        {
            get { return name.ToUpper(); }
            set { name = value.ToUpper(); }
        }

        private string hex;
        /// <summary>
        /// Hex value as a string, when available
        /// </summary>
        public string Hex
        {
            get { return hex; }
            set { hex = value; }
        }

        private string websafe;
        /// <summary>
        /// Websafe value, when available
        /// </summary>
        public string Websafe
        {
            get { return websafe; }
            set { websafe = value; }
        }

        private RGB rgb;
        /// <summary>
        /// RGB value, when available
        /// </summary>
        public RGB RGB
        {
            get { return rgb; }
            set { rgb = value; }
        }

        private HSL hsl;

        public HSL HSL
        {
            get { return hsl; }
            set { hsl = value; }
        }

        private HSB hsb;

        public HSB HSB
        {
            get { return hsb; }
            set { hsb = value; }
        }

        private CMYK cmyk;

        public CMYK CMYK
        {
            get { return cmyk; }
            set { cmyk = value; }
        }

        #endregion

        #region Constructors
        private PantoneColor() 
        { }

        /// <summary>
        /// Pulls the color from the known catalog based on the "selection" text.
        /// First looks for reference name. If not found, tries with RGB and closest with different combinaitions.
        /// Returns null if no match found.
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static PantoneColor FindColor(string selection)
        {
            // Tries to pull from catalog using reference name
            var res =  PantoneCatalog.Where(u => u.Name.Replace(" ","") == selection.Replace(" ", "").ToUpper()).FirstOrDefault();
            if(res != null) return res.DeepCopy();

            // If not found parse text as RGB input
            RGB rgb = null;
            CMYK cmyk = null;
            var input = selection.Split(',');   // with comma separator
            if(input.Count() >= 4)
            {
                try
                {
                    // Create CYMK
                    cmyk = new CMYK((byte)Int32.Parse(input[0]),
                                            (byte)(Int32.Parse(input[1])),
                                            (byte)(Int32.Parse(input[2])),
                                            (byte)(Int32.Parse(input[3])));
                }
                catch { }
            }
            else if (input.Count() == 3)
            {
                try
                {
                    // Create RGB
                    rgb = new RGB((byte)(Int32.Parse(input[0])),
                                            (byte)(Int32.Parse(input[1])),
                                        (byte)(Int32.Parse(input[2])));
                }
                catch { }
            }

            if(cmyk == null)
            {
                try
                {
                    input = selection.Split(' ');   // with space separator
                    if (input.Count() >= 4)
                    {
                        // Create CYMK
                        cmyk = new CMYK((byte)Int32.Parse(input[0]),
                                                (byte)(Int32.Parse(input[1])),
                                                (byte)(Int32.Parse(input[2])),
                                                (byte)(Int32.Parse(input[3])));
                    }
                }
                catch { }
            }
            else if(rgb == null)
            try
            {
                input = selection.Split(' ');   // with space separator
                if (input.Count() == 3)
                {
                    // Create RGB
                    rgb = new RGB((byte)(Int32.Parse(input[0])),
                                            (byte)(Int32.Parse(input[1])),
                                            (byte)(Int32.Parse(input[2])));
                }
            }
            catch { }

            // Try to pull the closest
            if(cmyk != null)
            {
                res = FindClosestColorByCMYK(cmyk);
                if (res != null) return res.DeepCopy();
            }
            else if (rgb != null)
            {
                res = FindClosestColorByRGB(rgb);
                if (res != null) return res.DeepCopy();
            }

            // Nothing worked, return null
            return null;
        }

        public static PantoneColor FindColor(double r, double g, double b)
        {
            // Tries to pull from catalog using reference name
            var res = FindClosestColorByRGB(r, g, b);
            if (res != null) return res.DeepCopy();
            return null;
        }

        public PantoneColor DeepCopy()
        {
            return new PantoneColor()
            {
                Name = String.Copy(this.Name),
                Hex = String.Copy(this.Hex),
                Websafe = String.Copy(this.Websafe),

                RGB = this.RGB.DeepCopy(),
                HSB = this.HSB.DeepCopy(),
                HSL = this.HSL.DeepCopy(),
                CMYK = this.CMYK.DeepCopy()
            };
        }
        #endregion

        #region Methods
        /// <summary>
        /// When the current color is a not a standard,
        /// find in the catalog the closest color in RGB values
        /// </summary>
        /// <returns></returns>
        private static PantoneColor FindClosestColorByRGB(RGB rgb)
        {
            var res = PantoneCatalog.OrderBy(u => u.RGBdistance(rgb)).ToList().FirstOrDefault();
            return res;
        }
        private static PantoneColor FindClosestColorByRGB(double r, double g, double b)
        {
            var res = PantoneCatalog.OrderBy(u => u.RGBdistance(r,g,b)).ToList().FirstOrDefault();
            return res;
        }

        private static PantoneColor FindClosestColorByCMYK(CMYK cmyk)
        {
            var res = PantoneCatalog.OrderBy(u => u.CMYKdistance(cmyk)).ToList().FirstOrDefault();
            return res;
        }

        /// <summary>
        /// Returns the distance to another color in terms of RGB distance.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private double RGBdistance(RGB rgb)
        {
            var r = ((double)(this.RGB.R) - (double)(rgb.R));
            var g = ((double)(this.RGB.G) - (double)(rgb.G));
            var b = ((double)(this.RGB.B) - (double)(rgb.B));

            return Math.Sqrt((r * r) + (g * g) + (b * b));
        }
        private double RGBdistance(double r, double g, double b)
        {
            var rr = ((double)(this.RGB.R) - r);
            var gg = ((double)(this.RGB.G) - g);
            var bb = ((double)(this.RGB.B) - b);

            return Math.Sqrt((rr * rr) + (gg * gg) + (bb * bb));
        }

        private double CMYKdistance(CMYK cmyk)
        {
            var c = ((double)(this.CMYK.C) - (double)(cmyk.C));
            var m = ((double)(this.CMYK.M) - (double)(cmyk.M));
            var y = ((double)(this.CMYK.Y) - (double)(cmyk.Y));
            var k = ((double)(this.CMYK.K) - (double)(cmyk.K));

            return Math.Sqrt((c * c) + (m * m) + (y * y) + (k * k));
        }

        #endregion

        #region Static Loading Catalog
        static PantoneColor()
        {
            // Initialize the static catalog (once)
            PantoneCatalog = ReadColorCatalog();
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
            PantoneColor color = new PantoneColor();

            // Parse the strings
            color.Name = json["name"].Value<string>();
            color.Hex = json["hex"].Value<string>();
            color.Websafe = json["websafe"].Value<string>();

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
        #endregion

        public override string ToString()
        {
            return Name.ToUpper() + ": " + RGB + " / " + CMYK;
        }
    }
}
