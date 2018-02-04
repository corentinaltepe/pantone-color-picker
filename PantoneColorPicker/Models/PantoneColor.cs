﻿using Newtonsoft.Json;
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
        /// TODO: handle color conversions and implement a real distance calculator
        /// See: https://www.pantone.com/help/?t=L*a*b-values-for-PANTONE-Coated-colors
        /// https://stackoverflow.com/questions/1313/followup-finding-an-accurate-distance-between-colors
        /// https://en.wikipedia.org/wiki/Lab_color_space
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
