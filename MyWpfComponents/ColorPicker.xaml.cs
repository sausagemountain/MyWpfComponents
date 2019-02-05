using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWpfComponents
{
    public class ColorChangedEventArgs
    {
        public ColorChangedEventArgs(Color Old, Color New)
        {
            this.Old = Old;
            this.New = New;
        }
        public Color New { get; set; }
        public Color Old { get; protected set; }
    }

    public delegate void ColorChanged(ColorChangedEventArgs e);

    public partial class ColorPicker : UserControl
    {
        private Color _color = Colors.Black;
        private bool locked = false;

        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                if (locked)
                    return;
                locked = true;

                var args = new ColorChangedEventArgs(Color, value);
                OnColorChanging?.Invoke(args);
                _color = args.New;

                if (A.Number != _color.A)
                    A.Number = _color.A;

                if (R.Number != _color.R)
                    R.Number = _color.R;

                if (G.Number != _color.G)
                    G.Number = _color.G;

                if (B.Number != _color.B)
                    B.Number = _color.B;

                locked = false;
                {
                    Display.Background = new SolidColorBrush(_color);

                    ALabel.Foreground = new SolidColorBrush(_color.MakeTransparent(255));
                    RedLabel.Foreground = ALabel.Foreground;
                    GreenLabel.Foreground = ALabel.Foreground;
                    BlueLabel.Foreground = ALabel.Foreground;

                    DGrid.Background = new SolidColorBrush(_color.Invert().MakeTransparent(191));
                }
            }
        }

        public event ColorChanged OnColorChanging;

        public ColorPicker() : this(Colors.Black)
        {
        }

        public ColorPicker(Color color)
        {
            InitializeComponent();
            Color = color;
        }

        private void Decimal_OnChecked(object sender, RoutedEventArgs e)
        {
            A.Base = 10;
            R.Base = 10;
            G.Base = 10;
            B.Base = 10;
        }

        private void Hexadecimal_OnChecked(object sender, RoutedEventArgs e)
        {
            A.Base = 16;
            R.Base = 16;
            G.Base = 16;
            B.Base = 16;
        }

        private void DecTextChanged(object sender, TextChangedEventArgs e)
        {
            Color = Color.FromArgb((byte)A.Number, (byte)R.Number, (byte)G.Number, (byte)B.Number);
        }

        private void Invert(object sender, RoutedEventArgs e)
        {
            Color = Color.Invert();
        }

        private byte nb => Ext.nb;

        private void Randomize(object sender, RoutedEventArgs e)
        {
            Color = Color.FromArgb(nb, nb, nb, nb);
        }
    }
}
