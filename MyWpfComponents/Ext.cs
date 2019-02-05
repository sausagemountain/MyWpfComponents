using System;
using System.Windows.Media;

namespace MyWpfComponents
{
    public static class Ext
    {
        public static Random rnd = new Random();
        public static byte nb => (byte)rnd.Next(255);

        public static Color Invert(this Color c)
        {
            return Color.FromArgb(c.A, (byte)(255 - c.R), (byte)(255 - c.G), (byte)(255 - c.B));
        }
        public static Color MakeTransparent(this Color c, byte transparency)
        {
            return Color.FromArgb(transparency, c.R, c.G, c.B);
        }
    }
}