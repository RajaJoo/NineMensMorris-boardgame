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
using System.ComponentModel;
using Microsoft.Win32;

//Mylly-Lautapeli
//Autohr: Joonas Raja
//14.12.2017
namespace Mylly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PeliLauta.UserControl1 pelinLauta = new PeliLauta.UserControl1(); //Luodaan objecti, etteivät metodit herjaa sen puuttumisesta

        //Alustetaan pelilauta
        public MainWindow()
        {
            InitializeComponent();
            UusiPeli();
        }

        //Executed-komento printtausta varten
        void PrintCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            PrintDialog printti = new PrintDialog();
            if (printti.ShowDialog() == true)
            {
                printti.PrintVisual(pelinLauta, "Tulostusalue");
            }
        }

        void ExitCmdCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Executed-komento ohjelman sulkemista varten
        void ExitCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        void NewGameCmdCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Executed-komento uuden pelin aloittamista varten
        void NewGameCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            UusiPeli();
        }

        //Alustetaan uusi pelilauta
        private void UusiPeli()
        {
            pelinLauta = new PeliLauta.UserControl1();
            pelinLauta.Height = 467;
            pelinLauta.Width = 476;
            pelinLauta.HorizontalAlignment = HorizontalAlignment.Left;
            pelinLauta.VerticalAlignment = VerticalAlignment.Top;
            Thickness m = new Thickness(10, 25, 0, 0);
            pelinLauta.Margin = m;
            viewi.Child = pelinLauta;
        }

        //Executed-komento pelinappulan lisäämistä varten
        void InsertExecuted(object target, ExecutedRoutedEventArgs e)
        {
            pelinLauta.lisaysTila = true;
        }


        void InsertCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            if (pelinLauta != null)
            {
                if (pelinLauta.lisaysTila == false) e.CanExecute = true;
            }
            
        }

        //Executed-komento pelinappulan liikuttamista varten
        void MoveExecuted(object target, ExecutedRoutedEventArgs e)
        {
            pelinLauta.siirtoTila = true;
        }

        void MoveCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            if (pelinLauta != null)
            {
                if (pelinLauta.valittuTila == true) e.CanExecute = true;
            }
            
        }

        //Executed-komento pelinappulan poistamista varten
        void RemoveExecuted(object target, ExecutedRoutedEventArgs e)
        {
            pelinLauta.PoistaNappula();
        }

        void RemoveCanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            if (pelinLauta != null)
            {
                if(pelinLauta.valittuTila == true) e.CanExecute = true;
            }
        }

        //Executed-komento Settings-dialogia varten
        void SettingsExecuted(object target, ExecutedRoutedEventArgs e)
        {
            SettingsWindow ikkuna = new SettingsWindow();
            ikkuna.Nappulanvari = pelinLauta.nappulanVari;
            ikkuna.Laudanvari = pelinLauta.Background;
            ikkuna.ShowDialog();
            pelinLauta.Background = ikkuna.Laudanvari;
            pelinLauta.nappulanVari = ikkuna.Nappulanvari;
            
        }

        //About-dialogin avaus
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        //How to Play-dialogin avaus
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            HelpWindow helppi = new HelpWindow();
            helppi.ShowDialog();
        }
    }

    //Omat komennot
    public static class OwnCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
        (
                "Exit",
                "Exit",
                typeof(OwnCommands)
        );

        public static readonly RoutedUICommand NewGame = new RoutedUICommand
            (
                "New Game",
                "New Game",
                typeof(OwnCommands)
            );

        public static readonly RoutedUICommand Insert = new RoutedUICommand
            (
                "Insert",
                "Insert",
                typeof(OwnCommands)
            );

        public static readonly RoutedUICommand Move = new RoutedUICommand
    (
        "Move",
        "Move",
        typeof(OwnCommands)
    );

        public static readonly RoutedUICommand Remove = new RoutedUICommand
    (
        "Remove",
        "Remove",
        typeof(OwnCommands)
    );

        public static readonly RoutedUICommand Settings = new RoutedUICommand
            (
            "Settings",
            "Settings",
            typeof(OwnCommands)
            );
    }


}
