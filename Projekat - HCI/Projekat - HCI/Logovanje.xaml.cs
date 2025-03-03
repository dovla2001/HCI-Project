using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

public static class Globals
{
    private static string ulogovan;
    private static readonly string posetilacUN = "posetilac2023";
    private static readonly string adminUN = "admin2023";

    public static string Ulogovan { get => ulogovan; set => ulogovan = value; }

    public static string PosetilacUN => posetilacUN;

    public static string AdminUN => adminUN;
}

namespace Projekat___HCI
{
    /// <summary>
    /// Interaction logic for Logovanje.xaml
    /// </summary>
    public partial class Logovanje : Window
    {

        public static string ime, sifra;
        public Logovanje ( )
        {
            InitializeComponent();
        }

        private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click ( object sender, RoutedEventArgs e )
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click ( object sender, RoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }
    
        private void btnPrijava_Click ( object sender, RoutedEventArgs e )
        {
            if (validate())
            {
                if (textBoxIme.Text.Trim().Equals("admin2023") && passwordBoxSifra.Password.Equals("123"))
                {
                    Globals.Ulogovan = textBoxIme.Text;
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
                else if (textBoxIme.Text.Trim().Equals("posetilac2023") && passwordBoxSifra.Password.Equals("123"))
                {
                    Globals.Ulogovan = textBoxIme.Text;
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
                else
                {
                    textBoxIme.Text = "";
                    passwordBoxSifra.Password = "";
                    MessageBox.Show("Netačni podaci za prijavu!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnIzlaz_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private bool validate ( )
        {
            bool result = true;

            if (textBoxIme.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("unesite ime"))
            {
                result = false;
                labelImeGreska.Content = "Polje korisničko ime mora biti popunjeno!";
                textBoxIme.BorderBrush = Brushes.Blue;
            }
            else
            {
                labelImeGreska.Content = "";
                textBoxIme.BorderBrush = Brushes.Red;
            }
         
            if (passwordBoxSifra.Password.Equals(""))
            {
                result = false;
                labelSifraGreska.Content = "Polje šifra mora biti popunjeno!";
                passwordBoxSifra.BorderBrush = Brushes.Blue;
            }
            else
            {
                labelSifraGreska.Content = "";
                passwordBoxSifra.BorderBrush = Brushes.Red;
            }
            return result;
        }
    }
}
