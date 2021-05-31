using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace MVVMPairs.Models
{
    [Serializable]
    public class Jucator : INotifyPropertyChanged
    {
        public Jucator(string nume, bool tura, bool culoare)
        {
            this.Nume = nume;
            nrPiese = 12;
            this.Tura = tura;
            mutari = new List<Cell>();
            this.PiesaCapturata = null;
            this.culoare = culoare;
        }

        public Jucator()
        {
            mutari = new List<Cell>();
        }
        private bool culoare;

        [XmlElement]
        public bool Culoare
        {
            get
            {
                return culoare;
            }
            set
            {
                culoare = value;
            }
        }

        private bool tura;

        [XmlElement]
        public bool Tura
        {
            set
            {
                tura = value;
            }
            get
            {
                return tura;
            }
        }

        private string nume;
        [XmlElement]
        public string Nume
        {
            set
            {
                nume = value;
            }
            get
            {
                return nume;
            }
        }

        private int nrPiese;
        [XmlElement]
        public int NrPiese
        {
            set
            {
                nrPiese = value;
            }
            get
            {
                return nrPiese;
            }
        }

        private Cell piesaMutata;
        [XmlElement]
        public Cell PiesaMutata
        {
            set
            {
                piesaMutata = value;
            }
            get
            {
                return piesaMutata;
            }
        }

        private Cell piesaCapturata;
        [XmlElement]
        public Cell PiesaCapturata
        {
            get
            {
                return piesaCapturata;
            }
            set
            {
                piesaCapturata = value;
            }
        }


        private Cell destinatie;
        [XmlElement]
        public Cell Destinatie
        {
            set
            {
                destinatie = value;
            }
            get
            {
                return destinatie;
            }
        }

        private List<Cell> mutari;
        [XmlElement]
        public List<Cell> Mutari
        {
            set
            {
                mutari = value;
            }
            get
            {
                return mutari;
            }
        }

        public bool MutareAdmisa(Cell cell)
        {
            for (int index=0; index<this.Mutari.Count; ++index)
            {
                if (cell== this.Mutari[index])
                    return true;
            }
            return false;
        }

        public void SchimbTura()
        {
            this.Tura = !this.Tura;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}