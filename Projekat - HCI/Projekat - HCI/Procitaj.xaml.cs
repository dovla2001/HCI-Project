using Class;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Projekat___HCI
{
    /// <summary>
    /// Interaction logic for Procitaj.xaml
    /// </summary>
    public partial class Procitaj : Window
    {
        public Procitaj ( int index )
        {
            Bayern bay = MainWindow.Bay[index];
            InitializeComponent();

            textBoxNaziv.Text = bay.NaslovClanka;
            textBoxBroj.Text = "Sifra clanka je: " + Convert.ToString(bay.SifraClanka);
            textBoxDatum.Text = "Datum objavljivanja: " + bay.DatumObjavljivanja.ToString("dd/MM/yyyy hh:mm:ss");

            Uri uri = new Uri(bay.Slika);
            imageSlika.Source = new BitmapImage(uri);

            TextRange textRange;
            System.IO.FileStream fileStream;

            if (System.IO.File.Exists(bay.Fajl))
            {
                textRange = new TextRange(richTextBoxBayern.Document.ContentStart, richTextBoxBayern.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(bay.Fajl, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }
        }

        private void btnIzlaz_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void UIPath_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            this.DragMove();

        }

        private void btnMinimize_Click ( object sender, RoutedEventArgs e )
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }
    }
}
