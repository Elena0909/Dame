using MVVMPairs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace MVVMPairs.Services
{
    class Helper
    {
        public static string JucatorCurent { get; set; }

        public static Scor GetScor()
        {
            Scor s = new Scor();
            string[] currentStats = File.ReadAllLines("scor.txt");
            s.ScorAlb = Int32.Parse(currentStats[0]);
            s.ScorNegru = Int32.Parse(currentStats[1]);
            return s;
        }
        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            int dim = 8;
            for (int linie = 0; linie < 8; linie++)
            {
                ObservableCollection<Cell> rand = new ObservableCollection<Cell>();
                for (int coloana = 0; coloana < dim; coloana++)
                {
                    rand.Add(new Cell(linie, coloana));
                }
                board.Add(rand);
            }

            for (int coloana = 0; coloana < dim; coloana++)
            {
                if (coloana % 2 == 0)
                {
                    board[1][coloana].Piesa = new Piesa(false);
                    board[5][coloana].Piesa = new Piesa(true);
                    board[7][coloana].Piesa = new Piesa(true);
                }
                else
                {
                    board[0][coloana].Piesa = new Piesa(false);
                    board[2][coloana].Piesa = new Piesa(false);
                    board[6][coloana].Piesa = new Piesa(true);
                }
            }
            return board;
        }

        public static void SerializeObjectToXML<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }
        }
        public static T DeserializeObjectToXML<T>(string FilePath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));

           
            T targetObj;
            
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fs);
                
                targetObj = (T)serializer.Deserialize(reader);

            }
            return targetObj;
            
        }
    }
}
