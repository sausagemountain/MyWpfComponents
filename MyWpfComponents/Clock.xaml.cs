using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace MyWpfComponents
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();
            var timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 25), DispatcherPriority.Normal, MoveHands, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void MoveHands(object sender, object e)
        {
            DateTime t = DateTime.Now;
            second.Angle = 6d * (t.Second + t.Millisecond / 1000d);
            minute.Angle = 6d * t.Minute + second.Angle / 60d;
            hour.Angle = 30d * (t.Hour % 12) + minute.Angle / 12d;
        }

        private byte nb => Ext.nb;

        public void ChangeColor()
        {
            Colorise(Color.FromArgb(nb, nb, nb, nb), Color.FromArgb(nb, nb, nb, nb), Color.FromArgb(nb, nb, nb, nb), Color.FromArgb(nb, nb, nb, nb));
        }

        public void Colorise(Color back, Color stroke, Color fill, Color ticks)
        {
            ColoriseBack(back);
            ColoriseStroke(stroke);
            ColoriseFill(fill);
            ColoriseTicks(ticks);
        }
        public void ColoriseTicks(Color ticks)
        {
            Application.Current.Resources.Remove("Ticks");
            Application.Current.Resources.Add("Ticks", new SolidColorBrush(ticks));
        }
        public void ColoriseFill(Color fill)
        {
            Application.Current.Resources.Remove("Fill");
            Application.Current.Resources.Add("Fill", new SolidColorBrush(fill));
        }
        public void ColoriseStroke(Color stroke)
        {
            Application.Current.Resources.Remove("Stroke");
            Application.Current.Resources.Add("Stroke", new SolidColorBrush(stroke));
        }
        public void ColoriseBack(Color back)
        {
            Application.Current.Resources.Remove("Back");
            Application.Current.Resources.Add("Back", new SolidColorBrush(back));
            Application.Current.Resources.Remove("Back2");
            Application.Current.Resources.Add("Back2", new LinearGradientBrush(back.MakeTransparent(255), back.MakeTransparent(0), new Point(1, 0.1), new Point(1, 0.55)));
        }
    }
}
