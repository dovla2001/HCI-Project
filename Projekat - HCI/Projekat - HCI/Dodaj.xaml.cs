using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Projekat___HCI
{
    /// <summary>
    /// Interaction logic for Dodaj.xaml
    /// </summary>
    public partial class Dodaj : Window
    {
        private string pomocna = "";
        public Dodaj ( )
        {
            InitializeComponent();
            textBoxNaziv.Text = "Unesite naslov clanka";
            textBoxNaziv.Foreground = Brushes.Thistle;

            textBoxBroj.Text = "Unesite sifru clanka";
            textBoxBroj.Foreground = Brushes.Thistle;

            ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            ComboBoxFamily.SelectedIndex = 2;
            ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
            ComboBoxColor.SelectedIndex = 0;
        }

        private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void btnDodaj_Click ( object sender, RoutedEventArgs e )
        {
            if (validate())
            {
                if (btnDodaj.Content.Equals("Dodaj"))
                {
                    string naziv = "";
                    naziv = textBoxNaziv.Text + ".rtf";


                    TextRange textRange;
                    FileStream fileStream;
                    textRange = new TextRange(RichTextBoxBayern.Document.ContentStart, RichTextBoxBayern.Document.ContentEnd);
                    fileStream = new FileStream(naziv, FileMode.Create);
                    textRange.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                    this.Close();

                    MainWindow.Bay.Add(new Class.Bayern(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, pomocna, naziv));
                }
            }
            else
            {
                MessageBox.Show("Popunite sva polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnIzlaz_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void textBoxNaziv_LostFocus ( object sender, RoutedEventArgs e )
        {
            if (textBoxNaziv.Text.Trim().Equals(string.Empty))
            {
                textBoxNaziv.Text = "Unesite naslov clanka";
                textBoxNaziv.Foreground = Brushes.Thistle;
            }
        }

        private void textBoxNaziv_GotFocus ( object sender, RoutedEventArgs e )
        {
            if (textBoxNaziv.Text.Trim().Equals("Unesite naslov clanka"))
            {
                textBoxNaziv.Text = "";
                textBoxNaziv.Foreground = Brushes.Black;
            }
        }

        private void textBoxBroj_LostFocus ( object sender, RoutedEventArgs e )
        {
            if (textBoxBroj.Text.Trim().Equals(string.Empty))
            {
                textBoxBroj.Text = "Unesite sifru clanka";
                textBoxBroj.Foreground = Brushes.Thistle;
            }
        }

        private void textBoxBroj_GotFocus ( object sender, RoutedEventArgs e )
        {
            if (textBoxBroj.Text.Trim().Equals("Unesite sifru clanka"))
            {
                textBoxBroj.Text = "";
                textBoxBroj.Foreground = Brushes.Black;
            }
        }

        private void RichTextBoxBayern_LostFocus ( object sender, RoutedEventArgs e )
        {
            if (runText.Text.Trim().Equals(string.Empty))
            {
                runText.Text = "Unesite sadrzaj clanka";
                runText.Foreground = Brushes.Thistle;
            }
        }

        private void RichTextBoxBayern_GotFocus ( object sender, RoutedEventArgs e )
        {
            if (runText.Text.Trim().Equals("Unesite sadrzaj clanka"))
            {
                runText.Text = "";
                runText.Foreground = Brushes.Black;
            }
        }

        private void btnBrowse_Click ( object sender, RoutedEventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                pomocna = openFileDialog.FileName;
                Uri uri = new Uri(pomocna);
                imageSlika.Source = new BitmapImage(uri);
                textBoxSlika.Text = "";
            }
        }

        private bool validate ( )
        {
            bool result = true;

            if (textBoxNaziv.Text.Trim().Equals("") || textBoxNaziv.Text.Trim().Equals("Unesite naslov clanka"))
            {
                result = false;
                textBoxNaziv.Foreground = Brushes.Red;
                textBoxNaziv.BorderBrush = Brushes.Red;
                textBoxNaziv.BorderThickness = new Thickness(1);
                //btnDodaj.Background = Brushes.Red;

            }
            else
            {
                textBoxNaziv.Foreground = Brushes.Black;
                textBoxNaziv.BorderBrush = Brushes.Green;
                textBoxNaziv.BorderThickness = new Thickness(1);
            }

            if (textBoxBroj.Text.Trim().Equals("") || textBoxBroj.Text.Trim().Equals("Unesite sifru clanka"))
            {
                result = false;
                textBoxBroj.Foreground = Brushes.Red;
                textBoxBroj.BorderBrush = Brushes.Red;
                textBoxBroj.BorderThickness = new Thickness(1);
                textBoxGreskaBroj.Foreground = Brushes.Red;
                //btnDodaj.Background = Brushes.Red;

            }
            else
            {

                bool isNumeric = int.TryParse(textBoxBroj.Text, out _);
                if (isNumeric)
                {
                    if (Int32.Parse(textBoxBroj.Text) > 0)
                    {
                        textBoxBroj.Foreground = Brushes.Black;
                        textBoxBroj.BorderBrush = Brushes.Green;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "";
                    }
                    else
                    {
                        result = false;
                        textBoxBroj.Foreground = Brushes.Red;
                        textBoxBroj.BorderBrush = Brushes.Red;
                        textBoxBroj.BorderThickness = new Thickness(1);
                        textBoxGreskaBroj.Text = "Unesite pozitivan broj!";
                    }

                }
                else
                {
                    result = false;
                    textBoxBroj.Foreground = Brushes.Red;
                    textBoxBroj.BorderBrush = Brushes.Red;
                    textBoxBroj.BorderThickness = new Thickness(1);
                    textBoxGreskaBroj.Text = "Unesite broj!";
                }
            }

            if (textBoxSlika.Text.Trim().Equals("Slika"))
            {
                result = false;
                borderSlika.BorderBrush = Brushes.Red;
                borderSlika.BorderThickness = new Thickness(1);
                labelaGreskaSlika.Content = "Obavezno popuniti!";
                //labelaGreskaSlika.Background = Brushes.Red;
                labelaGreskaSlika.Foreground = Brushes.Red;
                labelaGreskaSlika.BorderThickness = new Thickness(1);
            }
            else
            {
                borderSlika.BorderBrush = Brushes.Green;
                borderSlika.BorderThickness = new Thickness(0);
                labelaGreskaSlika.Content = "";
                labelaGreskaSlika.BorderThickness = new Thickness(0);
                textBoxSlika.Text = "";
            }

            if (DateTime.Now == null)
            {
                result = false;
                labelaGreskaDatum.FontSize = 12;
                labelaGreskaDatum.Content = "Obavezno polje!";
                labelaGreskaDatum.Foreground = Brushes.Red;
                labelaGreskaDatum.BorderBrush = Brushes.Red;
                labelaGreskaDatum.BorderThickness = new Thickness(1);

            }
            else
            {
                labelaGreskaDatum.Content = "";
                labelaGreskaDatum.BorderThickness = new Thickness(0);
            }
            return result;
        }

        private void RichTextBoxBayern_SelectionChanged ( object sender, RoutedEventArgs e )
        {
            object temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.FontStyleProperty);
            tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.FontWeightProperty);
            tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            ComboBoxFamily.SelectedItem = temp;

            temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.FontSizeProperty);
            ComboBoxSize.Text = temp.ToString();

            temp = RichTextBoxBayern.Selection.GetPropertyValue(Inline.ForegroundProperty);
        }

        private void ComboBoxFamily_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if (ComboBoxFamily.SelectedItem != null && !RichTextBoxBayern.Selection.IsEmpty)
            {
                RichTextBoxBayern.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
            }
        }

        private void ComboBoxSize_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if (ComboBoxSize.SelectedValue != null && !RichTextBoxBayern.Selection.IsEmpty)
            {
                RichTextBoxBayern.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
            }
        }

        private void ComboBoxColor_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if (ComboBoxColor.SelectedValue != null && !RichTextBoxBayern.Selection.IsEmpty)
            {
                RichTextBoxBayern.Selection.ApplyPropertyValue(Inline.ForegroundProperty, ComboBoxColor.SelectedValue);
            }
        }

        private void CountWordsInRTB ( )
        {
            int brojReci = 0;
            int index = 0;
            string richText = new TextRange(RichTextBoxBayern.Document.ContentStart, RichTextBoxBayern.Document.ContentEnd).Text;

            while (index < richText.Length && char.IsWhiteSpace(richText[index]))
            {
                index++;
            }

            while (index < richText.Length)
            {
                while (index < richText.Length && !char.IsWhiteSpace(richText[index]))
                {
                    index++;
                }

                brojReci++;

                while (index < richText.Length && char.IsWhiteSpace(richText[index]))
                {
                    index++;
                }
            }

            tbBrojReci.Text = brojReci.ToString();
        }

        private void RichTextBoxBayern_TextChanged ( object sender, System.Windows.Controls.TextChangedEventArgs e )
        {
            CountWordsInRTB();
        }
    }
}
