using MVVMPairs.Models;
using MVVMPairs.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMPairs.Commands;
using System.Windows.Input;

namespace MVVMPairs.ViewModels
{
    class GameVM : BaseNotification
    {
        public ObjectToSerialize Obj { get; set; }

        private ObservableCollection<ObservableCollection<CellVM>> gameBoard;

        public ObservableCollection<ObservableCollection<CellVM>> GameBoard
        {
            set
            {
                gameBoard = value;
                NotifyPropertyChanged("GameBoard");
            }
            get
            {
                return gameBoard;
            }
        }
        public GameVM()
        {

            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();
            
            bl = new GameBusinessLogic(board);
            GameBoard = CellBoardToCellVMBoard(ref board);
        }

        private GameBusinessLogic bl;

        public GameBusinessLogic BL
        {
            get
            {
                return bl;
            }
            set
            {
                bl = value;
                NotifyPropertyChanged("BL");
            }
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ref ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        public void SaveToXML()
        {
            var board = CellVMBoardToCellBoard();
            Helper.SerializeObjectToXML<ObservableCollection<ObservableCollection<Cell>>>(board, @"board.xml");
            Helper.SerializeObjectToXML<Jucator>(bl.JucatorAlb, @"jucatorA.xml");
            Helper.SerializeObjectToXML<Jucator>(bl.JucatorNegru, @"jucatorN.xml");
           
        }

        public void ReadFromXML()
        {
            var b = Helper.DeserializeObjectToXML<ObservableCollection<ObservableCollection<Cell>>>(@"board.xml");
            var a = Helper.DeserializeObjectToXML<Jucator>(@"jucatorA.xml");
            var n = Helper.DeserializeObjectToXML<Jucator>(@"jucatorN.xml");
            this.BL = new GameBusinessLogic(ref b,ref a,ref n);
           
            GameBoard = CellBoardToCellVMBoard(ref b);
        }

        public ICommand Saritura
        {
            set
            {
                bl.Saritura = true;
            }
        }



        private ICommand saveGame;
        public ICommand SaveGame
        {
            get
            {
                if (saveGame == null)
                {
                    saveGame = new RelayCommand<Object>(o => SaveToXML());
                }
                return saveGame;
            }
        }

        private ICommand opneGame;
        public ICommand OpenGame
        {
            get
            {
                if (opneGame == null)
                {
                    opneGame = new RelayCommand<Object>(o => ReadFromXML());
                }
                return opneGame;
            }
        }

        public bool SarituraM
        {
            get
            {
                if (bl.Saritura)
                    return false;
                else
                    return true;
            }
        }
        private ObservableCollection<ObservableCollection<Cell>> CellVMBoardToCellBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> result = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < gameBoard.Count; i++)
            {
                ObservableCollection<Cell> line = new ObservableCollection<Cell>();
                for (int j = 0; j < gameBoard[i].Count; j++)
                {
                    CellVM c = gameBoard[i][j];
                    line.Add(c.SimpleCell);
                }
                result.Add(line);
            }
            return result;
        }

        
    }
}
