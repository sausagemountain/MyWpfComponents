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
using System.Windows.Shapes;

namespace MyWpfComponents
{
    /// <summary>
    /// Interaction logic for WindowCustomize.xaml
    /// </summary>
    public partial class WindowCustomize : Window
    {
        public WindowCustomize()
        {
            InitializeComponent();
        }
        public WindowCustomize(Color color) : this()
        {
            Picker.Color = color;
        }

        public WindowCustomize This
        {
            get => this;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public event ColorChanged OnColorChanging
        {
            add => Picker.OnColorChanging += value;
            remove => Picker.OnColorChanging -= value;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
