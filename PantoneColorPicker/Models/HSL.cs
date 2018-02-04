namespace PantoneColorPicker.Models
{
    public class HSL
    {
        public int H { get; set; }
        public int S { get; set; }
        public int L { get; set; }

        public HSL() : this(0,0,0) { }
        public HSL(int h, int s, int l)
        {
            H = h;
            S = s;
            L = l;
        }

        public HSL DeepCopy()
        {
            return new HSL(H,S,L);
        }
    }
}
