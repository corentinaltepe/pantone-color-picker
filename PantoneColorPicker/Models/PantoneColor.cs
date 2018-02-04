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
        
        private string name;
        /// <summary>
        /// Pantone color code name (or reference)
        /// </summary>
        public string Name
        {
            get { return name.ToUpper(); }
            set { name = value.ToUpper(); }
        }
        
        // Colors expressed in different standards
        public string Hex { get; set; }
        public string Websafe { get; set; }
        public RGB RGB { get; set; }
        public HSL HSL { get; set; }
        public HSB HSB { get; set; }
        public CMYK CMYK { get; set; }
        
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
            if (input.Count() == 3)
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

            if(rgb == null)
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

        /// <summary>
        /// Distance with another PantoneColor, used to quantify "closeness" 
        /// between colors.
        /// </summary>
        /// <param name="otherColor"></param>
        /// <returns></returns>
        public double Distance(PantoneColor otherColor)
        {
            // Return the distance on RGBs (Gaussian distance)
            return RGB.Distance(otherColor.RGB);
        }
        
        public override string ToString()
        {
            return Name.ToUpper() + ": " + RGB + " / " + CMYK;
        }
    }
}
