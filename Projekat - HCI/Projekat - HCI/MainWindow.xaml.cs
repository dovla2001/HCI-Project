using Class;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Projekat___HCI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BindingList<Bayern> Bay { get; set; }

        public static readonly Class.DataIO serializer = new Class.DataIO();

        private static BindingList<Bayern> brisanje = new BindingList<Bayern>();
        public static BindingList<Bayern> Brisanje { get => brisanje; set => brisanje = value; }

        private static bool cbOznacen = false;
        public MainWindow ( )
        {
            Bay = serializer.DeSerializeObject<BindingList<Bayern>>("clanak.xml");

            if (Bay == null)
            {
                Bay = new BindingList<Bayern>();
            }
            DataContext = this;
            InitializeComponent();

            if (Globals.Ulogovan.Equals(Globals.PosetilacUN))
            {
                btnObrisiClanak.IsEnabled = false;
                btnDodajClanak.IsEnabled = false;
            }
        }

        private void Window_MouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {
            this.DragMove();
        }

        private void btnDodajClanak_Click ( object sender, RoutedEventArgs e )
        {
            Dodaj dodaj = new Dodaj();
            dodaj.ShowDialog();
        }

        private void btnObrisiClanak_Click ( object sender, RoutedEventArgs e )
        {
            for (int i = 0; i < brisanje.Count; i++)
            {
                Bay.Remove(brisanje[i]);
            }

            for (int i = 0; i < brisanje.Count; i++)
            {
                if (brisanje[i] != null)
                {
                    string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, brisanje[i].Fajl);
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (IOException exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }

        private void btnIzlaz_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void Hyperlink_Click ( object sender, RoutedEventArgs e )
        {
            if (Globals.Ulogovan.Equals(Globals.PosetilacUN))
            {
                Procitaj procitaj = new Procitaj(dataGridBayern.SelectedIndex);
                procitaj.ShowDialog();
            }
            else
            {
                Izmeni izmeni = new Izmeni(dataGridBayern.SelectedIndex);
                izmeni.ShowDialog();
            }
        }

        private void dataGridBayern_SelectionChanged ( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            if (cbOznacen == true)
            {
                brisanje.Add((Bayern)dataGridBayern.SelectedItem);
            }
            cbOznacen = false;
        }

        private void CheckBox_MouseEnter ( object sender, MouseEventArgs e )
        {
            cbOznacen = true;
        }

        private void Window_Closing ( object sender, CancelEventArgs e )
        {
            serializer.SerializeObject<BindingList<Class.Bayern>>(Bay, "clanak.xml");
        }

        private void btnClose_Click ( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void btnMinimize_Click ( object sender, RoutedEventArgs e )
        {
            WindowState = WindowState.Minimized;
        }
    }
}
