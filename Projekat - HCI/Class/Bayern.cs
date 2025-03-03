using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    [Serializable]
    public class Bayern
    {
        private int sifraClanka;
        private string naslovClanka;
        private DateTime datumObjavljivanja;
        private string slika;
        private string fajl;

        public Bayern ( int sifraClanka, string naslovClanka, DateTime datumObjavljivanja, string slika, string fajl )
        {
            this.sifraClanka = sifraClanka;
            this.naslovClanka = naslovClanka;
            this.datumObjavljivanja = datumObjavljivanja;
            this.slika = slika;
            this.fajl = fajl;
        }

        public Bayern ( )
        {
        }

        public int SifraClanka
        {
            get { return sifraClanka; }
            set { sifraClanka = value; }
        }

        public string NaslovClanka
        {
            get { return naslovClanka; }
            set { naslovClanka = value; }
        }

        public DateTime DatumObjavljivanja
        {
            get { return datumObjavljivanja; }
            set { datumObjavljivanja = value; }
        }

        public string Slika
        {
            get { return slika; }
            set { slika = value; }
        }

        public string Fajl
        {
            get { return fajl; }
            set { fajl = value; }
        }
    }
}
