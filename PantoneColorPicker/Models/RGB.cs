using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.Models
{
    public class RGB
    {
        private byte r;
        /// <summary>
        /// RED value
        /// </summary>
        public byte R
        {
            get { return r; }
            set { r = value; }
        }

        private byte g;
        /// <summary>
        /// GREEN value
        /// </summary>
        public byte G
        {
            get { return g; }
            set { g = value; }
        }
        
        private byte b;
        /// <summary>
        /// BLUE value
        /// </summary>
        public byte B
        {
            get { return b; }
            set { b = value; }
        }

        public RGB() : this(0,0,0)
        { }

        public RGB(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public RGB DeepCopy()
        {
            return new RGB(this.R, this.G, this.B);
        }

        public override string ToString()
        {
            return "R" + this.R.ToString() + ", G" + this.G.ToString() + ", B" + this.B.ToString();
        }
    }
}
