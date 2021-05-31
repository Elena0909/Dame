using MVVMPairs.Models;
using MVVMPairs.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MVVMPairs.Services
{
    [Serializable]
     public class GameBusinessLogic: INotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<Cell>> cells;

        [XmlArrayItem]
        public ObservableCollection<ObservableCollection<Cell>> Cells
        {
            get
            {
                return cells;
            }
            set
            {
                cells = value;
                NotifyPropertyChanged("Cells");
            }
        }
       
        private Jucator jucatorAlb;

        [XmlElement]
        public Jucator JucatorAlb
        {
            get
            {
                return jucatorAlb;
            }
            set
            {
                
                jucatorAlb = value;
                NotifyPropertyChanged("JucatorAlb");
            }
        }

       
        private Jucator jucatorNegru;

        [XmlElement]
        public Jucator JucatorNegru
        {
            get
            {
                return jucatorNegru;
            }
            set
            {
                jucatorNegru = value;
                NotifyPropertyChanged("JucatorNegru");
            }
        }

       
        private bool piesaSelectata;

       
        private Scor scor;

        public Scor Scor
        {
            get
            {
                return scor;
            }
        }

        private bool saritura;

        [XmlElement]
        public bool Saritura
        {
            set
            {
                saritura = value;
            }
            get
            {
                return saritura;
            }
        }

        private bool sarituraInCurs;

        public GameBusinessLogic(ref ObservableCollection<ObservableCollection<Cell>> cells, ref Jucator jucatorAlb,ref Jucator jucatorNegru)
        {
            this.Cells = cells;
            this.jucatorAlb = jucatorAlb;
            this.jucatorNegru = jucatorNegru;
            this.scor = Helper.GetScor();
        }

        public GameBusinessLogic()
        {
      
        }
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            jucatorAlb = new Jucator("Jucator alb", false, false);
            jucatorNegru = new Jucator("Jucator negru", true, true);
            this.cells = cells;
            piesaSelectata = false;
            JucatorCurent();
            this.Saritura = false;
            sarituraInCurs = false;
            this.scor = Helper.GetScor();
        }

        private void PozitiiMutare(Jucator jucator)
        {
            jucator.Destinatie = NE(jucator,1);
            ColoreazaMutare(jucator, jucator.Destinatie);

            jucator.Destinatie = NV(jucator,1);
            ColoreazaMutare(jucator, jucator.Destinatie);
            
            jucator.Destinatie = SE(jucator,1);
            ColoreazaMutare(jucator, jucator.Destinatie);

            jucator.Destinatie = SV(jucator,1);
            ColoreazaMutare(jucator, jucator.Destinatie);
        }
        private bool PozitiiSaritura(Jucator jucator)
        {
            jucator.Destinatie = NE(jucator, 2);
            jucator.PiesaCapturata = NE(jucator, 1);
            ColoreazaSaritura(jucator, jucator.Destinatie,jucator.PiesaCapturata);
            
            jucator.Destinatie = NV(jucator, 2);
            jucator.PiesaCapturata = NV(jucator, 1);
            ColoreazaSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            
            jucator.Destinatie = SE(jucator, 2);
            jucator.PiesaCapturata = SE(jucator, 1);
            ColoreazaSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            
            jucator.Destinatie = SV(jucator, 2);
            jucator.PiesaCapturata = SV(jucator, 1);
            ColoreazaSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            if (jucator.Mutari.Count != 0)
                return true;
            else
                return false;
        }
        private void ColoreazaMutare(Jucator jucator, Cell cell)
        {
            if (cell!=null && jucator.PiesaMutata.Mutare(cell))
            {
                jucator.Mutari.Add(cell);
                jucator.Destinatie = cell;
                jucator.Destinatie.Selectat = true;
            }
        }
        private void ColoreazaSaritura(Jucator jucator, Cell cell, Cell capturat)
        {
            if (cell != null && capturat != null && cell.Piesa == null
                       && capturat.Piesa != null && capturat.Piesa.Culoare != jucator.PiesaMutata.Piesa.Culoare)
            {
                if (jucator.PiesaMutata.Saritura(cell, capturat))
                {
                    jucator.Mutari.Add(cell);
                    jucator.Destinatie = cell;
                    jucator.Destinatie.Selectat = true;
                }
            }
        }
        private Cell NE(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X - mutare_saritura >= 0 && jucator.PiesaMutata.Y + mutare_saritura < 8)
                return cells[jucator.PiesaMutata.X - mutare_saritura][jucator.PiesaMutata.Y + mutare_saritura];
            return null;
        }
        private Cell NV(Jucator jucator,int mutare_saritura)
        {
            if (jucator.PiesaMutata.X - mutare_saritura >= 0 && jucator.PiesaMutata.Y - mutare_saritura >= 0)
                return cells[jucator.PiesaMutata.X - mutare_saritura][jucator.PiesaMutata.Y - mutare_saritura];
            return null;
        }
        private Cell SE(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X + mutare_saritura < 8 && jucator.PiesaMutata.Y + mutare_saritura < 8)
                return cells[jucator.PiesaMutata.X + mutare_saritura][jucator.PiesaMutata.Y + mutare_saritura];
            return null;
        }
        private Cell SV(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X + mutare_saritura < 8 && jucator.PiesaMutata.Y - mutare_saritura >= 0)
                return cells[jucator.PiesaMutata.X + mutare_saritura][jucator.PiesaMutata.Y - mutare_saritura];
            return null;
        }
        private bool Mutare(Jucator jucator, Cell currentCell)
        {
            if (jucator.MutareAdmisa(currentCell))
            {
                jucator.PiesaCapturata = PiesaCapturata(jucator, currentCell);
                cells[jucator.PiesaMutata.X][jucator.PiesaMutata.Y].Schimb(cells[currentCell.X][currentCell.Y]);
                if (jucator.PiesaCapturata != currentCell)
                {
                    cells[jucator.PiesaCapturata.X][jucator.PiesaCapturata.Y].Piesa.DisplayedImage = null;
                    cells[jucator.PiesaCapturata.X][jucator.PiesaCapturata.Y].Piesa = null;
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = cells[currentCell.X][currentCell.Y];
                    Castig(jucator);
                }
                else
                {
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = null;
                    piesaSelectata = false;
                }
                return true;
            }
            return false;
        }
        private Cell PiesaCapturata(Jucator jucator, Cell currentCell)
        {
            jucator.PiesaCapturata = null;
            if (jucator.PiesaMutata.X > currentCell.X)
            {
                if (jucator.PiesaMutata.Y > currentCell.Y)
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X - 1][jucator.PiesaMutata.Y - 1];
                else
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X - 1][jucator.PiesaMutata.Y + 1];
            }
            else
            {
                if (jucator.PiesaMutata.Y > currentCell.Y)
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X + 1][jucator.PiesaMutata.Y - 1];
                else
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X + 1][jucator.PiesaMutata.Y + 1];
            }
            return jucator.PiesaCapturata;
        }
        public void Move(Jucator jucator,Cell currentCell)
        {
            if (cells[currentCell.X][currentCell.Y].Piesa != null && piesaSelectata == false && cells[currentCell.X][currentCell.Y].Piesa.Culoare==jucator.Culoare)
            {
                jucator.PiesaMutata = currentCell;
                jucator.PiesaMutata.Selectat = true;
                piesaSelectata = true;
                if (!PozitiiSaritura(jucator))
                    PozitiiMutare(jucator);   
            }
            else
            if (piesaSelectata == true && currentCell!=jucator.PiesaMutata)
            {
                if (sarituraInCurs==false && cells[currentCell.X][currentCell.Y].Piesa != null && cells[currentCell.X][currentCell.Y].Piesa.Culoare == jucator.Culoare)
                {
                    jucator.PiesaMutata.Selectat = false;
                    StergeMarcaj(jucator);
                    jucator.PiesaMutata = currentCell;
                    jucator.PiesaMutata.Selectat = true;
                    if (!PozitiiSaritura(jucator))
                        PozitiiMutare(jucator);
                }
                else
                 if (Mutare(jucator, currentCell))
                {
                    StergeMarcaj(jucator);
                    VerificareRand(jucator);
                }
                
            }
        }
        private void StergeMarcaj(Jucator jucator)
        {
            for (int i = 0; i < jucator.Mutari.Count; ++i)
            {
                jucator.Mutari[i].Selectat = false;
            }
            jucator.Mutari.Clear();
        }
        private void VerificareRand(Jucator jucator)
        {
            jucator.PiesaCapturata = null;
            jucator.Destinatie = null;

            if (jucator.PiesaMutata != null && saritura == true)
            {
                jucator.PiesaMutata.Selectat = true;
                sarituraInCurs = true;
                if (!PozitiiSaritura(jucator))
                {
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = null;
                    piesaSelectata = false;
                    SchimbTura();
                    sarituraInCurs = false;
                }
            }
            else
            {
                SchimbTura();
                sarituraInCurs = false;
                jucator.PiesaMutata = null;
                piesaSelectata = false;
            }
        }

        public void ClickAction(Cell obj)
        {
            if (jucatorAlb.Tura == true)
            {
                Helper.JucatorCurent = jucatorAlb.Nume;
                Move(jucatorAlb, obj);
            }
            else
            {
                Helper.JucatorCurent = jucatorNegru.Nume;
                Move(jucatorNegru, obj);
            }
        }

        private void SchimbTura()
        {
            jucatorAlb.SchimbTura();
            jucatorNegru.SchimbTura();

            JucatorCurent();
        }
        private void JucatorCurent()
        {
            if (jucatorAlb.Tura)
                Helper.JucatorCurent = jucatorAlb.Nume;
            else
                Helper.JucatorCurent = jucatorNegru.Nume;
            NotifyPropertyChanged("JucatorLaMutare");
        }

        [XmlIgnore]
        public string JucatorLaMutare
        {
            get
            {
                if (jucatorAlb.NrPiese == 0)
                {
                    return "Jucatorul negru a castigat";
                }
                if (jucatorNegru.NrPiese == 0)
                {
                   
                    return "Jucatorul alb a castigat";
                }
                return Helper.JucatorCurent + ", este randul tau!";
            }
        }


        public void Castig(Jucator jucator)
        {
            if (jucator.Culoare)
                jucatorAlb.NrPiese--;
            else
                jucatorNegru.NrPiese--;

            if (jucatorNegru.NrPiese == 0)
            {
                scor.ScorAlb++;
                
            }
            if(jucatorAlb.NrPiese==0)
            {
                scor.ScorNegru++;
            }
            NotifyPropertyChanged("JucatorLaMutare");
        }

        public void Actulizare()
        {
            JucatorCurent();
            NotifyPropertyChanged("cells");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
     }
}