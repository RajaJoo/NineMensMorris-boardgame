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

namespace Mylly
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        //public Brush Laudanvari = new SolidColorBrush();
        //public SolidColorBrush nappulanvari = new SolidColorBrush();



        public SolidColorBrush Nappulanvari
        {
            get { return (SolidColorBrush)GetValue(NappulanvariProperty); }
            set { SetValue(NappulanvariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for nappulanvari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NappulanvariProperty =
            DependencyProperty.Register("Nappulanvari", typeof(SolidColorBrush), typeof(SettingsWindow), new PropertyMetadata(new SolidColorBrush(Colors.White)));



        public Brush Laudanvari
        {
            get { return (Brush)GetValue(LaudanvariProperty); }
            set { SetValue(LaudanvariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Laudanvari.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaudanvariProperty =
            DependencyProperty.Register("Laudanvari", typeof(Brush), typeof(SettingsWindow));





        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfcolor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                var brush = new SolidColorBrush(wpfcolor);
                Laudanvari = brush;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfcolor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                var brush = new SolidColorBrush(wpfcolor);
                Nappulanvari = brush;
            }
        }
    }
}
