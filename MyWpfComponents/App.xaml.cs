using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MyWpfComponents.Properties;

namespace MyWpfComponents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.Resources.Remove("Back");
            Current.Resources.Add("Back", new SolidColorBrush(Settings.Default.Back));
            Current.Resources.Remove("Back2");
            var back2 = Settings.Default.Back.MakeTransparent(255);
            Current.Resources.Add("Back2", new LinearGradientBrush(back2, back2.MakeTransparent(0), new Point(1, 0.1), new Point(1, 0.55)));
            Current.Resources.Remove("Stroke");
            Current.Resources.Add("Stroke", new SolidColorBrush(Settings.Default.Stroke));
            Current.Resources.Remove("Fill");
            Current.Resources.Add("Fill", new SolidColorBrush(Settings.Default.Fill));
            Current.Resources.Remove("Ticks");
            Current.Resources.Add("Ticks", new SolidColorBrush(Settings.Default.Ticks));
        }

        private void App_OnExit(object sender, ExitEventArgs args)
        {
            Settings.Default.Back = ((SolidColorBrush) Current.Resources["Back"]).Color;
            Settings.Default.Fill = ((SolidColorBrush) Current.Resources["Fill"]).Color;
            Settings.Default.Stroke = ((SolidColorBrush) Current.Resources["Stroke"]).Color;
            Settings.Default.Ticks = ((SolidColorBrush) Current.Resources["Ticks"]).Color;
            Settings.Default.Save();
        }
    }
}
