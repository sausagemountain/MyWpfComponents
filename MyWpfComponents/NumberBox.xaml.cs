using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWpfComponents
{
    [ContentProperty(nameof(Text))]
    public partial class NumberBox : UserControl
    {
        public static int convert(string s, int Base)
        {
            try {
                return Convert.ToInt32(s, Base);
            }
            catch {
                return 0;
            }
        }

        public TextAlignment TextAlignment
        {
            get => TextBox.TextAlignment;
            set => TextBox.TextAlignment = value;
        }
        public new VerticalAlignment VerticalContentAlignment
        {
            get => TextBox.VerticalContentAlignment;
            set => TextBox.VerticalContentAlignment = value;
        }
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get => TextBox.HorizontalContentAlignment;
            set => TextBox.HorizontalContentAlignment = value;
        }

        public string Text
        {
            get => TextBox.Text;
            set => TextBox.Text = value;
        }

        public int Base
        {
            get => _base;
            set
            {
                int n = Number;
                _base = value;
                Number = n;
            }
        }

        public int MaxNumber { get; set; } = 255;
        public int Number
        {
            get => convert(Text, Base);
            set => Text = Convert.ToString(value, Base);
        }

        public event TextChangedEventHandler TextChanged
        {
            add => TextBox.TextChanged += value;
            remove => TextBox.TextChanged -= value;
        }

        private static readonly Regex _regex10 = new Regex("[^0-9]+");
        private static readonly Regex _regex16 = new Regex("[^0-9a-fA-F]+");
        private int _base = 10;

        public NumberBox()
        {
            InitializeComponent();
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var res = TextBox.Text.Substring(0, TextBox.SelectionStart) + 
                      e.Text + 
                      TextBox.Text.Substring(TextBox.SelectionStart + TextBox.SelectionLength);

            switch (Base) {
                case 10:
                    if (_regex10.IsMatch(res)) {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 16:
                    if (_regex16.IsMatch(res)) {
                        e.Handled = true;
                        return;
                    }
                    break;
            }

            if (convert(res, Base) > MaxNumber) {
                Number = MaxNumber;
                e.Handled = true;
            }
        }

        private void TextBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            switch (Base)
            {
                case 10:
                    if (_regex10.IsMatch(Text))
                        Text = _regex10.Replace(Text, string.Empty);
                    break;
                case 16:
                    if (_regex16.IsMatch(Text))
                        Text = _regex16.Replace(Text, string.Empty);
                    break;
            }
        }
    }
}
