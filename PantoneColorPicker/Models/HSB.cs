using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.Models
{
    public class HSB
    {
        private int h;

        public int H
        {
            get { return h; }
            set { h = value; }
        }

        private int s;

        public int S
        {
            get { return s; }
            set { s = value; }
        }

        private int b;

        public int B
        {
            get { return b; }
            set { b = value; }
        }

        public HSB():this(0,0,0) { }
        public HSB(int h, int s, int b)
        {
            this.H = h;
            this.S = s;
            this.B = b;
        }

        public HSB DeepCopy()
        {
            return new HSB(H, S, B);
        }
    }
}
