using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using MyWpfComponents.Properties;

namespace MyWpfComponents
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Default.Positions.Count == 4) {
                var t = Settings.Default.Positions;
                Top = (double) t[0];
                Left = (double) t[1];
                Height = (double) t[2];
                Width = (double) t[3];
            }
        }
        private void ChangeColors_OnClick(object sender, RoutedEventArgs e)
        {
            Clock.ChangeColor();
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackDrop.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Visible;
            MinimizeButton.Visibility = Visibility.Visible;
        }
        private void MainWindow_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BackDrop.Visibility = Visibility.Collapsed;
            CloseButton.Visibility = Visibility.Collapsed;
            MinimizeButton.Visibility = Visibility.Collapsed;
        }

        WindowCustomize customize;
        private void CustomizeColors(object sender, RoutedEventArgs e)
        {
            var src = (string)((MenuItem)sender).Tag;
            customize?.Close();
            customize = new WindowCustomize(((SolidColorBrush)Application.Current.Resources[src]).Color){ Owner = this };
            customize.Show();

            switch (src)
            {
                case "Back":
                    customize.OnColorChanging += args => {
                        Clock.ColoriseBack(args.New);
                    };
                    break;
                case "Ticks":
                    customize.OnColorChanging += args => Clock.ColoriseTicks(args.New);
                    break;
                case "Stroke":
                    customize.OnColorChanging += args => Clock.ColoriseStroke(args.New);
                    break;
                case "Fill":
                    customize.OnColorChanging += args => Clock.ColoriseFill(args.New);
                    break;
            }
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var min = Math.Min(Height, Width);
            Height = min;
            Width = min;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var t = new ArrayList() {Top, Left, Height, Width};
            Settings.Default.Positions = t;
            Settings.Default.Save();
        }
    }
}
