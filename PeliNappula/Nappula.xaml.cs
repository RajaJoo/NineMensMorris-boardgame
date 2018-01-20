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

//Mylly-Lautapeli
//Autohr: Joonas Raja
//14.12.2017
namespace PeliNappula
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        //Property, jolla nappula tietää olevansa valittuna
        public bool Valittu
        {
            get { return (bool)GetValue(ValittuProperty); }
            set { SetValue(ValittuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValittuProperty =
            DependencyProperty.Register("Valittu", typeof(bool), typeof(UserControl), new PropertyMetadata(false));



        //Property nappulan värin asettamiseksi
        public SolidColorBrush Vari
        {
            get { return (SolidColorBrush)GetValue(VariProperty); }
            set { SetValue(VariProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VariProperty =
            DependencyProperty.Register("Vari", typeof(SolidColorBrush), typeof(UserControl), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public bool valittu = false;


        public UserControl1()
        {
            InitializeComponent();
        }

    }
}
