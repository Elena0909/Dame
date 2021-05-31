using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using MVVMPairs.Services;

namespace MVVMPairs.Models
{
    public class Scor : INotifyPropertyChanged
    {
        
        public Scor()
        {

        }

        private int scorAlb;
        public int ScorAlb
        {
            get
            {
                return scorAlb;
            }
            set
            {
                scorAlb = value;
                StreamWriter sw = new StreamWriter("scor.txt");
                string text;
                text = scorAlb.ToString() + "\n" + scorNegru.ToString();
                sw.WriteLine(text);
                sw.Close();

                NotifyPropertyChanged("SA");
            }
        }

        private int scorNegru;

        public int ScorNegru
        {
            get
            {
                return scorNegru;
            }
            set
            {
                scorNegru = value;
                StreamWriter sw = new StreamWriter("scor.txt");
                string text;
                text = scorAlb.ToString() + "\n" + scorNegru.ToString();
                sw.WriteLine(text);
                sw.Close();

                NotifyPropertyChanged("SN");
            }
        }

        public string SA
        {
            get
            {
               
                return scorAlb.ToString();
            }
        }

        public string SN
        {
            get
            {
                
                return scorNegru.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
