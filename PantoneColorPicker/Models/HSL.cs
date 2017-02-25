using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.Models
{
    public class HSL
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

        private int l;

        public int L
        {
            get { return l; }
            set { l = value; }
        }

        public HSL():this(0,0,0) { }
        public HSL(int h, int s, int l)
        {
            this.H = h;
            this.S = s;
            this.L = l;
        }

        public HSL DeepCopy()
        {
            return new HSL(H,S,L);
        }

    }
}
