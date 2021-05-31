using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace MVVMPairs.Models
{
    [Serializable]
    public class Piesa : INotifyPropertyChanged
    {
        public Piesa(bool culoare)
        {
            this.Culoare = culoare;
            this.Regina = false;
            if (culoare == true)
                this.DisplayedImage = "/MVVMPairs;component/Resources/negru.png";
            else
                this.DisplayedImage = "/MVVMPairs;component/Resources/alb.png";
        }

        public Piesa()
        {

        }
        private bool regina;

        [XmlElement]
        public bool Regina
        {
            get { return regina; }

            set
            {
                regina = value;
                if(value==true)
                    if (culoare == true)
                        this.DisplayedImage = "/MVVMPairs;component/Resources/rege.png";
                    else
                        this.DisplayedImage = "/MVVMPairs;component/Resources/regina.png";
                NotifyPropertyChanged("DisplayedImage");
            }
        }

        private bool culoare;

        [XmlElement]
        public bool Culoare
        {
            get { return culoare; }
            set
            {
                culoare = value;
            }
        }

        private string displayedImage;

        [XmlElement]
        public string DisplayedImage
        {
            get { return displayedImage; }
            set
            {
                displayedImage = value;
                NotifyPropertyChanged("DisplayedImage");
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