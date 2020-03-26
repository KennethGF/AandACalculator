using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace AxisAndAlliesCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class GameData
    {
        public string TERRITORY;   // data type string defined here
        public int IPC;
        public string OWNER;
        public string ORIGINAL_OWNER;
        public bool OCCUPIED;

        public GameData(
        string territory
        , int ipc
        , string owner,
        string original_owner,
        bool occupied)
        {
            TERRITORY = territory;
            IPC = ipc;
            OWNER = owner;
            ORIGINAL_OWNER = original_owner;
            OCCUPIED = occupied;
        }
    }
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

    #region Fields and Properties
        private List<GameData> _gameDataList = new List<GameData>();
        private int _germanyTotal;
        private int _japanTotal;
        private int _usaTotal;
        private int _ukTotal;
        private int _ussrTotal;
        private int _gameTotal;

        public List<GameData> GameDataList // THE LIST
        {
            get
            {
                return _gameDataList;
            }
            set
            {
                if (_gameDataList != value)
                _gameDataList = value;           
            }
        }
        public int GermanyTotal // GERMANY TOTAL 
        {
            get
            {
                var gList = GameDataList.Where(s => s.OWNER == "Germany").Select(s => s.IPC).ToList().Sum();
                _germanyTotal = gList;
                return _germanyTotal;
            }
            set
            { 
                    _germanyTotal = value;
            }

        }
        public int JapanTotal
        {
            get
            {
                var jList = GameDataList.Where(s => s.OWNER == "Japan").Select(s => s.IPC).ToList().Sum();
                _japanTotal = jList;
                return _japanTotal;
            }
            set
            {
                if (_japanTotal != value)
                    _japanTotal = value;
            }
        }
        public int USATotal
        {
            get
            {
                var usList = GameDataList.Where(s => s.OWNER == "USA").Select(s => s.IPC).ToList().Sum();
                _usaTotal = usList;
                return _usaTotal;
            }
            set
            {
                if (_usaTotal != value)
                    _usaTotal = value;
            }
        }
        public int UKTotal
        {
            get
            {
                var ukList = GameDataList.Where(s => s.OWNER == "UK").Select(s => s.IPC).ToList().Sum();
                _ukTotal = ukList;
                return _ukTotal;
            }
            set
            {
                if (_ukTotal != value)
                    _ukTotal = value;
            }
        }
        public int USSRTotal
        {
            get
            {
                var rusList = GameDataList.Where(s => s.OWNER == "USSR").Select(s => s.IPC).ToList().Sum();
                _ussrTotal = rusList;
                return _ussrTotal;
            }
            set
            {
                if (_ussrTotal != value)
                    _ussrTotal = value;
            }
        }

        public int GameTotal
        {
            get
            {
                var gameList = GameDataList.Select(s => s.IPC).ToList().Sum();
                _gameTotal = gameList;
                return _gameTotal;
            }
            set
            {
                if (_gameTotal != value)
                    _gameTotal = value;
            }
        }
#endregion

        public MainWindow() // this is a comment
        {
            DataContext = this;
            string _path = "C:/user";
            _path = Environment.CurrentDirectory;
            string filename = "\\BasicCountryList.txt";

            LoadBasicCountryListFromHDD(_path + filename); 

            InitializeComponent();

            //this.DataContext = new SimpleViewModel();
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            IsVisibleChanged += OnIsVisibleChanged;      
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string TotalPropetyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(TotalPropetyName));
        }
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            RadioButton rb = sender as RadioButton;

            if (rb != null)
                if (IsVisible)
                {
                    //rb.Visibility = Visibility.Collapsed;               
                    int num = Int32.Parse(rb.GroupName);
                }

        }

        #region Load Territories from List
        public void LoadBasicCountryListFromHDD(string filename)
        {
            var file = new FileStream(
                filename,
                FileMode.Open,
                FileAccess.Read);

            using (var reader = new StreamReader(file))
            {
                //Console.WriteLine("---------------  reading ListDontCheckBirth_H_W (from file)");
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;
                    //Console.WriteLine(" DontCheckBirth_H_W (from file): {0}", line);

                    var lineColl = line.Split(';');  // splits the line from file into 4 columns

                    string _territory = lineColl[0].Trim(); //.value ;
                    string _ipcString = lineColl[1].Trim();
                    int _ipc = Convert.ToInt32(_ipcString);
                    string _owner = lineColl[2].Trim();
                    string _origOwner = lineColl[3].Trim();
                    string _occupiedString = lineColl[4].Trim();
                    bool _occupied = false;
                    if (_occupiedString == "true" || _occupiedString == "True")
                         _occupied = true;
                    else if (_occupiedString != "false" || _occupiedString != "False")
                        Console.WriteLine(" check _occupied entry from file, not true and not false, Territory = {0}", _territory);

                    GameData thisGameData = new GameData(_territory, _ipc, _owner, _origOwner, _occupied);
                    _gameDataList.Add(thisGameData);
                }
            }

        }
        #endregion
        public void UpdateScreen()
        {
            _germanyTotal = GameDataList.Where(s => s.OWNER == "Germany").Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("GermanyTotal");
            _japanTotal = GameDataList.Where(s => s.OWNER == "Japan").Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("JapanTotal");
            _usaTotal = GameDataList.Where(s => s.OWNER == "USA").Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("USATotal");
            _ukTotal = GameDataList.Where(s => s.OWNER == "UK").Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("UKTotal");
            _ussrTotal = GameDataList.Where(s => s.OWNER == "USSR").Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("USSRTotal");
            _gameTotal = GameDataList.Select(s => s.IPC).ToList().Sum();
            OnPropertyChanged("GameTotal");
        }
        public void ChangeOwner(string nowOwning, string territory) // radio button group name sending name of Territory
        {
            string _territoryString = territory.Trim();
            var newOwner = "not set";
            newOwner = nowOwning.Trim();

            var territoryINT = GameDataList.FindIndex(item => item.TERRITORY == _territoryString);
            if (territoryINT >= 0)
            {
                GameDataList[territoryINT].OWNER = newOwner;
                Console.WriteLine("...new Owner of {0} = {1}", _territoryString, newOwner);
            }
        }

        #region Territory Radio Buttons
        private void click0(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                ChangeOwner(owner, (string)radioButton.GroupName); // button group name as territory name
                if (owner != "Germany")
                {
                    Axis0.Visibility = Visibility.Visible;
                    Allies0.Visibility = Visibility.Collapsed;
                    iG0.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis0.Visibility = Visibility.Collapsed;
                    Allies0.Visibility = Visibility.Visible;
                    iG0.Visibility = Visibility.Visible;
                    iUSA0.Visibility = Visibility.Collapsed;
                    iUK0.Visibility = Visibility.Collapsed;
                    iUSSR0.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG0.Visibility = Visibility.Visible;
                    iUSA0.Visibility = Visibility.Collapsed;
                    iUK0.Visibility = Visibility.Collapsed;
                    iUSSR0.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG0.Visibility = Visibility.Collapsed;
                    iUSA0.Visibility = Visibility.Visible;
                    iUK0.Visibility = Visibility.Collapsed;
                    iUSSR0.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG0.Visibility = Visibility.Collapsed;
                    iUSA0.Visibility = Visibility.Collapsed;
                    iUK0.Visibility = Visibility.Visible;
                    iUSSR0.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG0.Visibility = Visibility.Collapsed;
                    iUSA0.Visibility = Visibility.Collapsed;
                    iUK0.Visibility = Visibility.Collapsed;
                    iUSSR0.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click1(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                ChangeOwner(owner, (string)radioButton.GroupName); // button group name as territory name
                if (owner != "Germany")
                {
                    Axis1.Visibility = Visibility.Visible;
                    Allies1.Visibility = Visibility.Collapsed;
                    iG1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis1.Visibility = Visibility.Collapsed;
                    Allies1.Visibility = Visibility.Visible;
                    iG1.Visibility = Visibility.Visible;
                    iUSA1.Visibility = Visibility.Collapsed;
                    iUK1.Visibility = Visibility.Collapsed;
                    iUSSR1.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG1.Visibility = Visibility.Visible;
                    iUSA1.Visibility = Visibility.Collapsed;
                    iUK1.Visibility = Visibility.Collapsed;
                    iUSSR1.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG1.Visibility = Visibility.Collapsed;
                    iUSA1.Visibility = Visibility.Visible;
                    iUK1.Visibility = Visibility.Collapsed;
                    iUSSR1.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG1.Visibility = Visibility.Collapsed;
                    iUSA1.Visibility = Visibility.Collapsed;
                    iUK1.Visibility = Visibility.Visible;
                    iUSSR1.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG1.Visibility = Visibility.Collapsed;
                    iUSA1.Visibility = Visibility.Collapsed;
                    iUK1.Visibility = Visibility.Collapsed;
                    iUSSR1.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
 
        private void click26(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                ChangeOwner(owner, (string)radioButton.GroupName); // button group name as territory name
                if (owner != "USA")
                {
                    Axis26.Visibility = Visibility.Collapsed;
                    Allies26.Visibility = Visibility.Visible;
                    iUSA26.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis26.Visibility = Visibility.Visible;
                    Allies26.Visibility = Visibility.Collapsed;
                    iUSA26.Visibility = Visibility.Visible;
                    iG26.Visibility = Visibility.Collapsed;
                    iJ26.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iUSA26.Visibility = Visibility.Visible;
                    iG26.Visibility = Visibility.Collapsed;
                    iJ26.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSA26.Visibility = Visibility.Collapsed;
                    iG26.Visibility = Visibility.Visible;
                    iJ26.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSA26.Visibility = Visibility.Collapsed;
                    iG26.Visibility = Visibility.Collapsed;
                    iJ26.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click27(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                ChangeOwner(owner, (string)radioButton.GroupName); // button group name as territory name
                if (owner != "USA")
                {
                    Axis27.Visibility = Visibility.Collapsed;
                    Allies27.Visibility = Visibility.Visible;
                    iUSA27.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis27.Visibility = Visibility.Visible;
                    Allies27.Visibility = Visibility.Collapsed;
                    iUSA27.Visibility = Visibility.Visible;
                    iG27.Visibility = Visibility.Collapsed;
                    iJ27.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iUSA27.Visibility = Visibility.Visible;
                    iG27.Visibility = Visibility.Collapsed;
                    iJ27.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSA27.Visibility = Visibility.Collapsed;
                    iG27.Visibility = Visibility.Visible;
                    iJ27.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSA27.Visibility = Visibility.Collapsed;
                    iG27.Visibility = Visibility.Collapsed;
                    iJ27.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click40(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                ChangeOwner(owner, (string)radioButton.GroupName); // button group name as territory name
                if (owner != "UK")
                {
                    Axis40.Visibility = Visibility.Collapsed;
                    Allies40.Visibility = Visibility.Visible;
                    iUK40.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis40.Visibility = Visibility.Visible;
                    Allies40.Visibility = Visibility.Collapsed;
                    iUK40.Visibility = Visibility.Visible;
                    iG40.Visibility = Visibility.Collapsed;
                    iJ40.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK40.Visibility = Visibility.Visible;
                    iG40.Visibility = Visibility.Collapsed;
                    iJ40.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK40.Visibility = Visibility.Collapsed;
                    iG40.Visibility = Visibility.Visible;
                    iJ40.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK40.Visibility = Visibility.Collapsed;
                    iG40.Visibility = Visibility.Collapsed;
                    iJ40.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        #endregion
        private void ImageBrush_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
