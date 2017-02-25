using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.Models
{
    public class CMYK
    {
        private byte c;

        public byte C
        {
            get { return c; }
            set { c = value; }
        }

        private byte m;

        public byte M
        {
            get { return m; }
            set { m = value; }
        }

        private byte y;

        public byte Y
        {
            get { return y; }
            set { y = value; }
        }

        private byte k;

        public byte K
        {
            get { return k; }
            set { k = value; }
        }

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


        public override string ToString()
        {
            return this.C.ToString() + "," + this.M.ToString() + "," 
                    + this.Y.ToString() + "," + this.K.ToString();
        }
    }
}
