namespace PantoneColorPicker.Models
{
    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RGB() : this(0,0,0) { }
        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public RGB DeepCopy()
        {
            return new RGB(this.R, this.G, this.B);
        }

        /// <summary>
        /// Returns the distance to another color in terms of RGB distance.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private double RGBdistance(RGB otherColor)
        {
            var r = ((double)(R) - (double)(otherColor.R));
            var g = ((double)(G) - (double)(otherColor.G));
            var b = ((double)(B) - (double)(otherColor.B));

            return (r * r) + (g * g) + (b * b);
        }

        public override string ToString()
        {
            return "rgb (" + R + ", " + G + ", " + B + ")";
        }
    }
}
