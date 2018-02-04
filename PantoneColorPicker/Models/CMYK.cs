using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.Models
{
    public class CMYK
    {
        public byte C { get; set; }
        public byte M { get; set; }
        public byte Y { get; set; }
        public byte K { get; set; }

        public CMYK():this(0,0,0,0) { }
        public CMYK(byte c, byte m, byte y, byte k)
        {
            this.C = c;
            this.M = m;
            this.Y = y;
            this.K = k;
        }
        public CMYK DeepCopy()
        {
            return new CMYK(C, M, Y, K);
        }

        public double Distance(CMYK cmyk)
        {
            var c = ((double)(C) - (double)(cmyk.C));
            var m = ((double)(M) - (double)(cmyk.M));
            var y = ((double)(Y) - (double)(cmyk.Y));
            var k = ((double)(K) - (double)(cmyk.K));

            return (c * c) + (m * m) + (y * y) + (k * k);
        }

        public override string ToString()
        {
            return "cmyk (" + C + ", " + M + ", " + Y + ", " + K + ")";
        }
    }
}
