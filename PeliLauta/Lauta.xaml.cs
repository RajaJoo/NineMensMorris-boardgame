using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
namespace PeliLauta
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public bool lisaysTila = false;     //Attribuutti joka määrää voiko nappulan lisätä
        public bool valittuTila = false;    //Attribuutti joka määrää onko nappula valittuna
        public bool siirtoTila = false;     //Attribuutti joka määrää voiko nappulan siirtää laudalla
        public SolidColorBrush nappulanVari = new SolidColorBrush(Colors.White);    //Nappulan väri
        public int nappulaLaskuri = 0;      //Nappuloiden lukumäärä
        private PeliNappula.UserControl1 Nappi = new PeliNappula.UserControl1();    //Attribuutti joka tallentaa tiedon viimeeksi lisätystä/valitusta nappulasta



        public UserControl1()
        {
            InitializeComponent();
            
        }

        //Hiiren klikkaukseen reagoiva eventti, joka suorittaa nappulaan liittyvät toiminnot
        private void Click(object sender, MouseButtonEventArgs e)
        {
            
            //Tarkistetaan, että klikattava kohde on ellipsi, eli joko pelinappula tai laudan ympyrä
            //Nappula on muotoa UserControl, jossa on control templatessa Ellipsillä varustettu CheckBox
            if (e.OriginalSource is Ellipse)
            {
                Ellipse pallura = (Ellipse)e.OriginalSource;
                DependencyObject checkboxi = VisualTreeHelper.GetParent(pallura);
                

                //Suoritetaan kun klikataan uusi nappula laudalle
                //Tarkistetaan ensin onko voidaanko nappula lisätä ja että onko ympyrän parentti Canvas tyyppiä
                if (lisaysTila == true && pallura.Parent is Canvas)
                {
                    if (pallura.Parent is UserControl) return;
                    if (!(pallura.Parent is Canvas)) return;

                    Canvas kanvas = (Canvas)pallura.Parent;
                    if (Nappi != null) Nappi.Valittu = false;
                    PeliNappula.UserControl1 peliNappula = new PeliNappula.UserControl1();
                    peliNappula.Vari = nappulanVari;
                    Grid.SetRow(peliNappula, Grid.GetRow(kanvas));
                    Grid.SetColumn(peliNappula, Grid.GetColumn(kanvas));
                    gridRuudukko.Children.Add(peliNappula);
                    lisaysTila = false;     //Palautetaan Insert nappula takaisin käytettäväksi
                    Nappi = peliNappula;
                    Nappi.Valittu = true;
                    valittuTila = true;     //Lisätty nappula on valittuna heti
                    nappulaLaskuri += 1;
                    VoittajaTarkistus();    //Tarkistetaan, että päättyykö peli
                }
                
                //Suoritetaan kun klikataan laudalla olevaa pelinappulaa
                //Tarkistetaan, että klikattava objecti on CheckBox tyyppinen
                if (checkboxi is CheckBox)
                {
                    PeliNappula.UserControl1 pelinappula = (PeliNappula.UserControl1)((CheckBox)checkboxi).Parent;      //Pakotetaan oikeaan tyyppiin
                    Nappi.Valittu = false;      //Poistetaan edellisen nappulan valinta
                    Nappi = pelinappula;
                    Nappi.Valittu = true;       //Lisätään valinta nyt klikatulle nappulalle
                    valittuTila = true;
                    lisaysTila = false;
                    e.Handled = true;           //Ilman tätä CheckBox "uncheckaa" itsensä välittömästi
                }

                //Suoritetaan kun klikataan laudalla olevaa ympyrää kun yksi nappula on jo valittu, eli toisinsanoen liikutetaan nappula
                if (siirtoTila == true && e.OriginalSource is Ellipse)
                {
                    if (pallura.Parent is UserControl) return;  //tarkistukset
                    if (!(pallura.Parent is Canvas)) return;

                    Canvas kanvas = (Canvas)pallura.Parent;
                    Grid.SetRow(Nappi, Grid.GetRow(kanvas));
                    Grid.SetColumn(Nappi, Grid.GetColumn(kanvas));
                    gridRuudukko.Children.Remove(Nappi);          //Poistetaan valittu nappula
                    gridRuudukko.Children.Add(Nappi);               //Lisätään uuteen kohtaan
                    siirtoTila = false;
                }


            }



        }

        //Metodi valitun nappulan poistamiselle
        public void PoistaNappula()
        {
            gridRuudukko.Children.Remove(Nappi);
            valittuTila = false;
            nappulaLaskuri -= 1;
        }

        //Tarkistetaan onko peli loppunut ja soitetaan voittoääni
        public void VoittajaTarkistus()
        {
            if (nappulaLaskuri > 4)
            { 
            SoundPlayer player = new SoundPlayer(@"C:\Users\J.Raja\Desktop\Harkka\winner.wav");
            player.Load();
            player.Play();
            }
        }

        //Tarpeellinen klikkauksen toimintaa varten
        private void klikkaus(object sender, MouseButtonEventArgs e)
        {
            Click(sender, e);
        }
    }

    /// <summary>
    /// Oma canvas-luokka, joka lisää pari propertya avuksi. HalfWidth ja HalfHeight antavat puolitetun todellisen leveyden ja korkeuden ja näihin voi bindata xamlista
    /// </summary>
    public class OwnCanvas : Canvas
    {
        public OwnCanvas()
        {
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            SizeChanged += OwnCanvas_SizeChanged;
        }

        void OwnCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HalfWidth = ActualWidth / 2;
            HalfHeight = ActualHeight / 2;
        }


        public double HalfWidth
        {
            get { return (double)GetValue(HalfWidthProperty); }
            set { SetValue(HalfWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HalfWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HalfWidthProperty =
            DependencyProperty.Register("HalfWidth", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));


        public double HalfHeight
        {
            get { return (double)GetValue(HalfHeightProperty); }
            set { SetValue(HalfHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HalfHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HalfHeightProperty =
            DependencyProperty.Register("HalfHeight", typeof(double), typeof(OwnCanvas), new PropertyMetadata(0.0));



    }
}
