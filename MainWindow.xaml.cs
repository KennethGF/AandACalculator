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
        public GameData(
        string territory
        , int ipc
        , string owner,
        string original_owner)      
        {
            TERRITORY = territory;
            IPC = ipc;
            OWNER = owner;
            ORIGINAL_OWNER = original_owner;
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

                    GameData thisGameData = new GameData(_territory, _ipc, _owner, _origOwner);
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
        public string GetTerritory(string radioGroup)
        {
            int radioInt = Int32.Parse(radioGroup);
            string territory = GameDataList[radioInt].TERRITORY;
            return territory;
        }

        #region Territory Radio Buttons
        private void click0(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
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
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
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
        private void click2(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis2.Visibility = Visibility.Visible;
                    Allies2.Visibility = Visibility.Collapsed;
                    iG2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis2.Visibility = Visibility.Collapsed;
                    Allies2.Visibility = Visibility.Visible;
                    iG2.Visibility = Visibility.Visible;
                    iUSA2.Visibility = Visibility.Collapsed;
                    iUK2.Visibility = Visibility.Collapsed;
                    iUSSR2.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG2.Visibility = Visibility.Visible;
                    iUSA2.Visibility = Visibility.Collapsed;
                    iUK2.Visibility = Visibility.Collapsed;
                    iUSSR2.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG2.Visibility = Visibility.Collapsed;
                    iUSA2.Visibility = Visibility.Visible;
                    iUK2.Visibility = Visibility.Collapsed;
                    iUSSR2.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG2.Visibility = Visibility.Collapsed;
                    iUSA2.Visibility = Visibility.Collapsed;
                    iUK2.Visibility = Visibility.Visible;
                    iUSSR2.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG2.Visibility = Visibility.Collapsed;
                    iUSA2.Visibility = Visibility.Collapsed;
                    iUK2.Visibility = Visibility.Collapsed;
                    iUSSR2.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click3(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis3.Visibility = Visibility.Visible;
                    Allies3.Visibility = Visibility.Collapsed;
                    iG3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis3.Visibility = Visibility.Collapsed;
                    Allies3.Visibility = Visibility.Visible;
                    iG3.Visibility = Visibility.Visible;
                    iUSA3.Visibility = Visibility.Collapsed;
                    iUK3.Visibility = Visibility.Collapsed;
                    iUSSR3.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG3.Visibility = Visibility.Visible;
                    iUSA3.Visibility = Visibility.Collapsed;
                    iUK3.Visibility = Visibility.Collapsed;
                    iUSSR3.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG3.Visibility = Visibility.Collapsed;
                    iUSA3.Visibility = Visibility.Visible;
                    iUK3.Visibility = Visibility.Collapsed;
                    iUSSR3.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG3.Visibility = Visibility.Collapsed;
                    iUSA3.Visibility = Visibility.Collapsed;
                    iUK3.Visibility = Visibility.Visible;
                    iUSSR3.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG3.Visibility = Visibility.Collapsed;
                    iUSA3.Visibility = Visibility.Collapsed;
                    iUK3.Visibility = Visibility.Collapsed;
                    iUSSR3.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click4(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis4.Visibility = Visibility.Visible;
                    Allies4.Visibility = Visibility.Collapsed;
                    iG4.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis4.Visibility = Visibility.Collapsed;
                    Allies4.Visibility = Visibility.Visible;
                    iG4.Visibility = Visibility.Visible;
                    iUSA4.Visibility = Visibility.Collapsed;
                    iUK4.Visibility = Visibility.Collapsed;
                    iUSSR4.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG4.Visibility = Visibility.Visible;
                    iUSA4.Visibility = Visibility.Collapsed;
                    iUK4.Visibility = Visibility.Collapsed;
                    iUSSR4.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG4.Visibility = Visibility.Collapsed;
                    iUSA4.Visibility = Visibility.Visible;
                    iUK4.Visibility = Visibility.Collapsed;
                    iUSSR4.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG4.Visibility = Visibility.Collapsed;
                    iUSA4.Visibility = Visibility.Collapsed;
                    iUK4.Visibility = Visibility.Visible;
                    iUSSR4.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG4.Visibility = Visibility.Collapsed;
                    iUSA4.Visibility = Visibility.Collapsed;
                    iUK4.Visibility = Visibility.Collapsed;
                    iUSSR4.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click5(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis5.Visibility = Visibility.Visible;
                    Allies5.Visibility = Visibility.Collapsed;
                    iG5.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis5.Visibility = Visibility.Collapsed;
                    Allies5.Visibility = Visibility.Visible;
                    iG5.Visibility = Visibility.Visible;
                    iUSA5.Visibility = Visibility.Collapsed;
                    iUK5.Visibility = Visibility.Collapsed;
                    iUSSR5.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG5.Visibility = Visibility.Visible;
                    iUSA5.Visibility = Visibility.Collapsed;
                    iUK5.Visibility = Visibility.Collapsed;
                    iUSSR5.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG5.Visibility = Visibility.Collapsed;
                    iUSA5.Visibility = Visibility.Visible;
                    iUK5.Visibility = Visibility.Collapsed;
                    iUSSR5.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG5.Visibility = Visibility.Collapsed;
                    iUSA5.Visibility = Visibility.Collapsed;
                    iUK5.Visibility = Visibility.Visible;
                    iUSSR5.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG5.Visibility = Visibility.Collapsed;
                    iUSA5.Visibility = Visibility.Collapsed;
                    iUK5.Visibility = Visibility.Collapsed;
                    iUSSR5.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click6(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis6.Visibility = Visibility.Visible;
                    Allies6.Visibility = Visibility.Collapsed;
                    iG6.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis6.Visibility = Visibility.Collapsed;
                    Allies6.Visibility = Visibility.Visible;
                    iG6.Visibility = Visibility.Visible;
                    iUSA6.Visibility = Visibility.Collapsed;
                    iUK6.Visibility = Visibility.Collapsed;
                    iUSSR6.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG6.Visibility = Visibility.Visible;
                    iUSA6.Visibility = Visibility.Collapsed;
                    iUK6.Visibility = Visibility.Collapsed;
                    iUSSR6.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG6.Visibility = Visibility.Collapsed;
                    iUSA6.Visibility = Visibility.Visible;
                    iUK6.Visibility = Visibility.Collapsed;
                    iUSSR6.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG6.Visibility = Visibility.Collapsed;
                    iUSA6.Visibility = Visibility.Collapsed;
                    iUK6.Visibility = Visibility.Visible;
                    iUSSR6.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG6.Visibility = Visibility.Collapsed;
                    iUSA6.Visibility = Visibility.Collapsed;
                    iUK6.Visibility = Visibility.Collapsed;
                    iUSSR6.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click7(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis7.Visibility = Visibility.Visible;
                    Allies7.Visibility = Visibility.Collapsed;
                    iG7.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis7.Visibility = Visibility.Collapsed;
                    Allies7.Visibility = Visibility.Visible;
                    iG7.Visibility = Visibility.Visible;
                    iUSA7.Visibility = Visibility.Collapsed;
                    iUK7.Visibility = Visibility.Collapsed;
                    iUSSR7.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG7.Visibility = Visibility.Visible;
                    iUSA7.Visibility = Visibility.Collapsed;
                    iUK7.Visibility = Visibility.Collapsed;
                    iUSSR7.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG7.Visibility = Visibility.Collapsed;
                    iUSA7.Visibility = Visibility.Visible;
                    iUK7.Visibility = Visibility.Collapsed;
                    iUSSR7.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG7.Visibility = Visibility.Collapsed;
                    iUSA7.Visibility = Visibility.Collapsed;
                    iUK7.Visibility = Visibility.Visible;
                    iUSSR7.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG7.Visibility = Visibility.Collapsed;
                    iUSA7.Visibility = Visibility.Collapsed;
                    iUK7.Visibility = Visibility.Collapsed;
                    iUSSR7.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click8(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis8.Visibility = Visibility.Visible;
                    Allies8.Visibility = Visibility.Collapsed;
                    iG8.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis8.Visibility = Visibility.Collapsed;
                    Allies8.Visibility = Visibility.Visible;
                    iG8.Visibility = Visibility.Visible;
                    iUSA8.Visibility = Visibility.Collapsed;
                    iUK8.Visibility = Visibility.Collapsed;
                    iUSSR8.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG8.Visibility = Visibility.Visible;
                    iUSA8.Visibility = Visibility.Collapsed;
                    iUK8.Visibility = Visibility.Collapsed;
                    iUSSR8.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG8.Visibility = Visibility.Collapsed;
                    iUSA8.Visibility = Visibility.Visible;
                    iUK8.Visibility = Visibility.Collapsed;
                    iUSSR8.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG8.Visibility = Visibility.Collapsed;
                    iUSA8.Visibility = Visibility.Collapsed;
                    iUK8.Visibility = Visibility.Visible;
                    iUSSR8.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG8.Visibility = Visibility.Collapsed;
                    iUSA8.Visibility = Visibility.Collapsed;
                    iUK8.Visibility = Visibility.Collapsed;
                    iUSSR8.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click9(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis9.Visibility = Visibility.Visible;
                    Allies9.Visibility = Visibility.Collapsed;
                    iG9.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis9.Visibility = Visibility.Collapsed;
                    Allies9.Visibility = Visibility.Visible;
                    iG9.Visibility = Visibility.Visible;
                    iUSA9.Visibility = Visibility.Collapsed;
                    iUK9.Visibility = Visibility.Collapsed;
                    iUSSR9.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG9.Visibility = Visibility.Visible;
                    iUSA9.Visibility = Visibility.Collapsed;
                    iUK9.Visibility = Visibility.Collapsed;
                    iUSSR9.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG9.Visibility = Visibility.Collapsed;
                    iUSA9.Visibility = Visibility.Visible;
                    iUK9.Visibility = Visibility.Collapsed;
                    iUSSR9.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG9.Visibility = Visibility.Collapsed;
                    iUSA9.Visibility = Visibility.Collapsed;
                    iUK9.Visibility = Visibility.Visible;
                    iUSSR9.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG9.Visibility = Visibility.Collapsed;
                    iUSA9.Visibility = Visibility.Collapsed;
                    iUK9.Visibility = Visibility.Collapsed;
                    iUSSR9.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click10(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis10.Visibility = Visibility.Visible;
                    Allies10.Visibility = Visibility.Collapsed;
                    iG10.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis10.Visibility = Visibility.Collapsed;
                    Allies10.Visibility = Visibility.Visible;
                    iG10.Visibility = Visibility.Visible;
                    iUSA10.Visibility = Visibility.Collapsed;
                    iUK10.Visibility = Visibility.Collapsed;
                    iUSSR10.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG10.Visibility = Visibility.Visible;
                    iUSA10.Visibility = Visibility.Collapsed;
                    iUK10.Visibility = Visibility.Collapsed;
                    iUSSR10.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG10.Visibility = Visibility.Collapsed;
                    iUSA10.Visibility = Visibility.Visible;
                    iUK10.Visibility = Visibility.Collapsed;
                    iUSSR10.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG10.Visibility = Visibility.Collapsed;
                    iUSA10.Visibility = Visibility.Collapsed;
                    iUK10.Visibility = Visibility.Visible;
                    iUSSR10.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG10.Visibility = Visibility.Collapsed;
                    iUSA10.Visibility = Visibility.Collapsed;
                    iUK10.Visibility = Visibility.Collapsed;
                    iUSSR10.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click11(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis11.Visibility = Visibility.Visible;
                    Allies11.Visibility = Visibility.Collapsed;
                    iG11.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis11.Visibility = Visibility.Collapsed;
                    Allies11.Visibility = Visibility.Visible;
                    iG11.Visibility = Visibility.Visible;
                    iUSA11.Visibility = Visibility.Collapsed;
                    iUK11.Visibility = Visibility.Collapsed;
                    iUSSR11.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG11.Visibility = Visibility.Visible;
                    iUSA11.Visibility = Visibility.Collapsed;
                    iUK11.Visibility = Visibility.Collapsed;
                    iUSSR11.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG11.Visibility = Visibility.Collapsed;
                    iUSA11.Visibility = Visibility.Visible;
                    iUK11.Visibility = Visibility.Collapsed;
                    iUSSR11.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG11.Visibility = Visibility.Collapsed;
                    iUSA11.Visibility = Visibility.Collapsed;
                    iUK11.Visibility = Visibility.Visible;
                    iUSSR11.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG11.Visibility = Visibility.Collapsed;
                    iUSA11.Visibility = Visibility.Collapsed;
                    iUK11.Visibility = Visibility.Collapsed;
                    iUSSR11.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click12(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis12.Visibility = Visibility.Visible;
                    Allies12.Visibility = Visibility.Collapsed;
                    iG12.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis12.Visibility = Visibility.Collapsed;
                    Allies12.Visibility = Visibility.Visible;
                    iG12.Visibility = Visibility.Visible;
                    iUSA12.Visibility = Visibility.Collapsed;
                    iUK12.Visibility = Visibility.Collapsed;
                    iUSSR12.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG12.Visibility = Visibility.Visible;
                    iUSA12.Visibility = Visibility.Collapsed;
                    iUK12.Visibility = Visibility.Collapsed;
                    iUSSR12.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG12.Visibility = Visibility.Collapsed;
                    iUSA12.Visibility = Visibility.Visible;
                    iUK12.Visibility = Visibility.Collapsed;
                    iUSSR12.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG12.Visibility = Visibility.Collapsed;
                    iUSA12.Visibility = Visibility.Collapsed;
                    iUK12.Visibility = Visibility.Visible;
                    iUSSR12.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG12.Visibility = Visibility.Collapsed;
                    iUSA12.Visibility = Visibility.Collapsed;
                    iUK12.Visibility = Visibility.Collapsed;
                    iUSSR12.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click13(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis13.Visibility = Visibility.Visible;
                    Allies13.Visibility = Visibility.Collapsed;
                    iG13.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis13.Visibility = Visibility.Collapsed;
                    Allies13.Visibility = Visibility.Visible;
                    iG13.Visibility = Visibility.Visible;
                    iUSA13.Visibility = Visibility.Collapsed;
                    iUK13.Visibility = Visibility.Collapsed;
                    iUSSR13.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG13.Visibility = Visibility.Visible;
                    iUSA13.Visibility = Visibility.Collapsed;
                    iUK13.Visibility = Visibility.Collapsed;
                    iUSSR13.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG13.Visibility = Visibility.Collapsed;
                    iUSA13.Visibility = Visibility.Visible;
                    iUK13.Visibility = Visibility.Collapsed;
                    iUSSR13.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG13.Visibility = Visibility.Collapsed;
                    iUSA13.Visibility = Visibility.Collapsed;
                    iUK13.Visibility = Visibility.Visible;
                    iUSSR13.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG13.Visibility = Visibility.Collapsed;
                    iUSA13.Visibility = Visibility.Collapsed;
                    iUK13.Visibility = Visibility.Collapsed;
                    iUSSR13.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click14(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis14.Visibility = Visibility.Visible;
                    Allies14.Visibility = Visibility.Collapsed;
                    iG14.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis14.Visibility = Visibility.Collapsed;
                    Allies14.Visibility = Visibility.Visible;
                    iG14.Visibility = Visibility.Visible;
                    iUSA14.Visibility = Visibility.Collapsed;
                    iUK14.Visibility = Visibility.Collapsed;
                    iUSSR14.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG14.Visibility = Visibility.Visible;
                    iUSA14.Visibility = Visibility.Collapsed;
                    iUK14.Visibility = Visibility.Collapsed;
                    iUSSR14.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG14.Visibility = Visibility.Collapsed;
                    iUSA14.Visibility = Visibility.Visible;
                    iUK14.Visibility = Visibility.Collapsed;
                    iUSSR14.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG14.Visibility = Visibility.Collapsed;
                    iUSA14.Visibility = Visibility.Collapsed;
                    iUK14.Visibility = Visibility.Visible;
                    iUSSR14.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG14.Visibility = Visibility.Collapsed;
                    iUSA14.Visibility = Visibility.Collapsed;
                    iUK14.Visibility = Visibility.Collapsed;
                    iUSSR14.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click15(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "Germany")
                {
                    Axis15.Visibility = Visibility.Visible;
                    Allies15.Visibility = Visibility.Collapsed;
                    iG15.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis15.Visibility = Visibility.Collapsed;
                    Allies15.Visibility = Visibility.Visible;
                    iG15.Visibility = Visibility.Visible;
                    iUSA15.Visibility = Visibility.Collapsed;
                    iUK15.Visibility = Visibility.Collapsed;
                    iUSSR15.Visibility = Visibility.Collapsed;
                }
                if (owner == "Germany")
                {
                    iG15.Visibility = Visibility.Visible;
                    iUSA15.Visibility = Visibility.Collapsed;
                    iUK15.Visibility = Visibility.Collapsed;
                    iUSSR15.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iG15.Visibility = Visibility.Collapsed;
                    iUSA15.Visibility = Visibility.Visible;
                    iUK15.Visibility = Visibility.Collapsed;
                    iUSSR15.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iG15.Visibility = Visibility.Collapsed;
                    iUSA15.Visibility = Visibility.Collapsed;
                    iUK15.Visibility = Visibility.Visible;
                    iUSSR15.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iG15.Visibility = Visibility.Collapsed;
                    iUSA15.Visibility = Visibility.Collapsed;
                    iUK15.Visibility = Visibility.Collapsed;
                    iUSSR15.Visibility = Visibility.Visible;
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
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
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
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
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
        private void click36(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USA")
                {
                    Axis36.Visibility = Visibility.Collapsed;
                    Allies36.Visibility = Visibility.Visible;
                    iUSA36.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis36.Visibility = Visibility.Visible;
                    Allies36.Visibility = Visibility.Collapsed;
                    iUSA36.Visibility = Visibility.Visible;
                    iG36.Visibility = Visibility.Collapsed;
                    iJ36.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iUSA36.Visibility = Visibility.Visible;
                    iG36.Visibility = Visibility.Collapsed;
                    iJ36.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSA36.Visibility = Visibility.Collapsed;
                    iG36.Visibility = Visibility.Visible;
                    iJ36.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSA36.Visibility = Visibility.Collapsed;
                    iG36.Visibility = Visibility.Collapsed;
                    iJ36.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click37(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USA")
                {
                    Axis37.Visibility = Visibility.Collapsed;
                    Allies37.Visibility = Visibility.Visible;
                    iUSA37.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis37.Visibility = Visibility.Visible;
                    Allies37.Visibility = Visibility.Collapsed;
                    iUSA37.Visibility = Visibility.Visible;
                    iG37.Visibility = Visibility.Collapsed;
                    iJ37.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iUSA37.Visibility = Visibility.Visible;
                    iG37.Visibility = Visibility.Collapsed;
                    iJ37.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSA37.Visibility = Visibility.Collapsed;
                    iG37.Visibility = Visibility.Visible;
                    iJ37.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSA37.Visibility = Visibility.Collapsed;
                    iG37.Visibility = Visibility.Collapsed;
                    iJ37.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click38(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USA")
                {
                    Axis38.Visibility = Visibility.Collapsed;
                    Allies38.Visibility = Visibility.Visible;
                    iUSA38.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis38.Visibility = Visibility.Visible;
                    Allies38.Visibility = Visibility.Collapsed;
                    iUSA38.Visibility = Visibility.Visible;
                    iG38.Visibility = Visibility.Collapsed;
                    iJ38.Visibility = Visibility.Collapsed;
                }
                if (owner == "USA")
                {
                    iUSA38.Visibility = Visibility.Visible;
                    iG38.Visibility = Visibility.Collapsed;
                    iJ38.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSA38.Visibility = Visibility.Collapsed;
                    iG38.Visibility = Visibility.Visible;
                    iJ38.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSA38.Visibility = Visibility.Collapsed;
                    iG38.Visibility = Visibility.Collapsed;
                    iJ38.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click39(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis39.Visibility = Visibility.Collapsed;
                    Allies39.Visibility = Visibility.Visible;
                    iUK39.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis39.Visibility = Visibility.Visible;
                    Allies39.Visibility = Visibility.Collapsed;
                    iUK39.Visibility = Visibility.Visible;
                    iG39.Visibility = Visibility.Collapsed;
                    iJ39.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK39.Visibility = Visibility.Visible;
                    iG39.Visibility = Visibility.Collapsed;
                    iJ39.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK39.Visibility = Visibility.Collapsed;
                    iG39.Visibility = Visibility.Visible;
                    iJ39.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK39.Visibility = Visibility.Collapsed;
                    iG39.Visibility = Visibility.Collapsed;
                    iJ39.Visibility = Visibility.Visible;
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
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
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
        private void click41(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis41.Visibility = Visibility.Collapsed;
                    Allies41.Visibility = Visibility.Visible;
                    iUK41.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis41.Visibility = Visibility.Visible;
                    Allies41.Visibility = Visibility.Collapsed;
                    iUK41.Visibility = Visibility.Visible;
                    iG41.Visibility = Visibility.Collapsed;
                    iJ41.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK41.Visibility = Visibility.Visible;
                    iG41.Visibility = Visibility.Collapsed;
                    iJ41.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK41.Visibility = Visibility.Collapsed;
                    iG41.Visibility = Visibility.Visible;
                    iJ41.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK41.Visibility = Visibility.Collapsed;
                    iG41.Visibility = Visibility.Collapsed;
                    iJ41.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
       private void click42(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis42.Visibility = Visibility.Collapsed;
                    Allies42.Visibility = Visibility.Visible;
                    iUK42.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis42.Visibility = Visibility.Visible;
                    Allies42.Visibility = Visibility.Collapsed;
                    iUK42.Visibility = Visibility.Visible;
                    iG42.Visibility = Visibility.Collapsed;
                    iJ42.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK42.Visibility = Visibility.Visible;
                    iG42.Visibility = Visibility.Collapsed;
                    iJ42.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK42.Visibility = Visibility.Collapsed;
                    iG42.Visibility = Visibility.Visible;
                    iJ42.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK42.Visibility = Visibility.Collapsed;
                    iG42.Visibility = Visibility.Collapsed;
                    iJ42.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click43(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis43.Visibility = Visibility.Collapsed;
                    Allies43.Visibility = Visibility.Visible;
                    iUK43.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis43.Visibility = Visibility.Visible;
                    Allies43.Visibility = Visibility.Collapsed;
                    iUK43.Visibility = Visibility.Visible;
                    iG43.Visibility = Visibility.Collapsed;
                    iJ43.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK43.Visibility = Visibility.Visible;
                    iG43.Visibility = Visibility.Collapsed;
                    iJ43.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK43.Visibility = Visibility.Collapsed;
                    iG43.Visibility = Visibility.Visible;
                    iJ43.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK43.Visibility = Visibility.Collapsed;
                    iG43.Visibility = Visibility.Collapsed;
                    iJ43.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click44(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis44.Visibility = Visibility.Collapsed;
                    Allies44.Visibility = Visibility.Visible;
                    iUK44.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis44.Visibility = Visibility.Visible;
                    Allies44.Visibility = Visibility.Collapsed;
                    iUK44.Visibility = Visibility.Visible;
                    iG44.Visibility = Visibility.Collapsed;
                    iJ44.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK44.Visibility = Visibility.Visible;
                    iG44.Visibility = Visibility.Collapsed;
                    iJ44.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK44.Visibility = Visibility.Collapsed;
                    iG44.Visibility = Visibility.Visible;
                    iJ44.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK44.Visibility = Visibility.Collapsed;
                    iG44.Visibility = Visibility.Collapsed;
                    iJ44.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click45(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis45.Visibility = Visibility.Collapsed;
                    Allies45.Visibility = Visibility.Visible;
                    iUK45.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis45.Visibility = Visibility.Visible;
                    Allies45.Visibility = Visibility.Collapsed;
                    iUK45.Visibility = Visibility.Visible;
                    iG45.Visibility = Visibility.Collapsed;
                    iJ45.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK45.Visibility = Visibility.Visible;
                    iG45.Visibility = Visibility.Collapsed;
                    iJ45.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK45.Visibility = Visibility.Collapsed;
                    iG45.Visibility = Visibility.Visible;
                    iJ45.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK45.Visibility = Visibility.Collapsed;
                    iG45.Visibility = Visibility.Collapsed;
                    iJ45.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click46(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis46.Visibility = Visibility.Collapsed;
                    Allies46.Visibility = Visibility.Visible;
                    iUK46.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis46.Visibility = Visibility.Visible;
                    Allies46.Visibility = Visibility.Collapsed;
                    iUK46.Visibility = Visibility.Visible;
                    iG46.Visibility = Visibility.Collapsed;
                    iJ46.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK46.Visibility = Visibility.Visible;
                    iG46.Visibility = Visibility.Collapsed;
                    iJ46.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK46.Visibility = Visibility.Collapsed;
                    iG46.Visibility = Visibility.Visible;
                    iJ46.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK46.Visibility = Visibility.Collapsed;
                    iG46.Visibility = Visibility.Collapsed;
                    iJ46.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click47(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis47.Visibility = Visibility.Collapsed;
                    Allies47.Visibility = Visibility.Visible;
                    iUK47.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis47.Visibility = Visibility.Visible;
                    Allies47.Visibility = Visibility.Collapsed;
                    iUK47.Visibility = Visibility.Visible;
                    iG47.Visibility = Visibility.Collapsed;
                    iJ47.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK47.Visibility = Visibility.Visible;
                    iG47.Visibility = Visibility.Collapsed;
                    iJ47.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK47.Visibility = Visibility.Collapsed;
                    iG47.Visibility = Visibility.Visible;
                    iJ47.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK47.Visibility = Visibility.Collapsed;
                    iG47.Visibility = Visibility.Collapsed;
                    iJ47.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click48(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis48.Visibility = Visibility.Collapsed;
                    Allies48.Visibility = Visibility.Visible;
                    iUK48.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis48.Visibility = Visibility.Visible;
                    Allies48.Visibility = Visibility.Collapsed;
                    iUK48.Visibility = Visibility.Visible;
                    iG48.Visibility = Visibility.Collapsed;
                    iJ48.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK48.Visibility = Visibility.Visible;
                    iG48.Visibility = Visibility.Collapsed;
                    iJ48.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK48.Visibility = Visibility.Collapsed;
                    iG48.Visibility = Visibility.Visible;
                    iJ48.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK48.Visibility = Visibility.Collapsed;
                    iG48.Visibility = Visibility.Collapsed;
                    iJ48.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click49(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis49.Visibility = Visibility.Collapsed;
                    Allies49.Visibility = Visibility.Visible;
                    iUK49.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis49.Visibility = Visibility.Visible;
                    Allies49.Visibility = Visibility.Collapsed;
                    iUK49.Visibility = Visibility.Visible;
                    iG49.Visibility = Visibility.Collapsed;
                    iJ49.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK49.Visibility = Visibility.Visible;
                    iG49.Visibility = Visibility.Collapsed;
                    iJ49.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK49.Visibility = Visibility.Collapsed;
                    iG49.Visibility = Visibility.Visible;
                    iJ49.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK49.Visibility = Visibility.Collapsed;
                    iG49.Visibility = Visibility.Collapsed;
                    iJ49.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click50(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis50.Visibility = Visibility.Collapsed;
                    Allies50.Visibility = Visibility.Visible;
                    iUK50.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis50.Visibility = Visibility.Visible;
                    Allies50.Visibility = Visibility.Collapsed;
                    iUK50.Visibility = Visibility.Visible;
                    iG50.Visibility = Visibility.Collapsed;
                    iJ50.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK50.Visibility = Visibility.Visible;
                    iG50.Visibility = Visibility.Collapsed;
                    iJ50.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK50.Visibility = Visibility.Collapsed;
                    iG50.Visibility = Visibility.Visible;
                    iJ50.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK50.Visibility = Visibility.Collapsed;
                    iG50.Visibility = Visibility.Collapsed;
                    iJ50.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click51(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis51.Visibility = Visibility.Collapsed;
                    Allies51.Visibility = Visibility.Visible;
                    iUK51.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis51.Visibility = Visibility.Visible;
                    Allies51.Visibility = Visibility.Collapsed;
                    iUK51.Visibility = Visibility.Visible;
                    iG51.Visibility = Visibility.Collapsed;
                    iJ51.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK51.Visibility = Visibility.Visible;
                    iG51.Visibility = Visibility.Collapsed;
                    iJ51.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK51.Visibility = Visibility.Collapsed;
                    iG51.Visibility = Visibility.Visible;
                    iJ51.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK51.Visibility = Visibility.Collapsed;
                    iG51.Visibility = Visibility.Collapsed;
                    iJ51.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click52(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis52.Visibility = Visibility.Collapsed;
                    Allies52.Visibility = Visibility.Visible;
                    iUK52.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis52.Visibility = Visibility.Visible;
                    Allies52.Visibility = Visibility.Collapsed;
                    iUK52.Visibility = Visibility.Visible;
                    iG52.Visibility = Visibility.Collapsed;
                    iJ52.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK52.Visibility = Visibility.Visible;
                    iG52.Visibility = Visibility.Collapsed;
                    iJ52.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK52.Visibility = Visibility.Collapsed;
                    iG52.Visibility = Visibility.Visible;
                    iJ52.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK52.Visibility = Visibility.Collapsed;
                    iG52.Visibility = Visibility.Collapsed;
                    iJ52.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click53(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis53.Visibility = Visibility.Collapsed;
                    Allies53.Visibility = Visibility.Visible;
                    iUK53.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis53.Visibility = Visibility.Visible;
                    Allies53.Visibility = Visibility.Collapsed;
                    iUK53.Visibility = Visibility.Visible;
                    iG53.Visibility = Visibility.Collapsed;
                    iJ53.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK53.Visibility = Visibility.Visible;
                    iG53.Visibility = Visibility.Collapsed;
                    iJ53.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK53.Visibility = Visibility.Collapsed;
                    iG53.Visibility = Visibility.Visible;
                    iJ53.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK53.Visibility = Visibility.Collapsed;
                    iG53.Visibility = Visibility.Collapsed;
                    iJ53.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click54(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis54.Visibility = Visibility.Collapsed;
                    Allies54.Visibility = Visibility.Visible;
                    iUK54.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis54.Visibility = Visibility.Visible;
                    Allies54.Visibility = Visibility.Collapsed;
                    iUK54.Visibility = Visibility.Visible;
                    iG54.Visibility = Visibility.Collapsed;
                    iJ54.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK54.Visibility = Visibility.Visible;
                    iG54.Visibility = Visibility.Collapsed;
                    iJ54.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK54.Visibility = Visibility.Collapsed;
                    iG54.Visibility = Visibility.Visible;
                    iJ54.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK54.Visibility = Visibility.Collapsed;
                    iG54.Visibility = Visibility.Collapsed;
                    iJ54.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click55(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "UK")
                {
                    Axis55.Visibility = Visibility.Collapsed;
                    Allies55.Visibility = Visibility.Visible;
                    iUK55.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis55.Visibility = Visibility.Visible;
                    Allies55.Visibility = Visibility.Collapsed;
                    iUK55.Visibility = Visibility.Visible;
                    iG55.Visibility = Visibility.Collapsed;
                    iJ55.Visibility = Visibility.Collapsed;
                }
                if (owner == "UK")
                {
                    iUK55.Visibility = Visibility.Visible;
                    iG55.Visibility = Visibility.Collapsed;
                    iJ55.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUK55.Visibility = Visibility.Collapsed;
                    iG55.Visibility = Visibility.Visible;
                    iJ55.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUK55.Visibility = Visibility.Collapsed;
                    iG55.Visibility = Visibility.Collapsed;
                    iJ55.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click57(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis57.Visibility = Visibility.Collapsed;
                    Allies57.Visibility = Visibility.Visible;
                    iUSSR57.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis57.Visibility = Visibility.Visible;
                    Allies57.Visibility = Visibility.Collapsed;
                    iUSSR57.Visibility = Visibility.Visible;
                    iG57.Visibility = Visibility.Collapsed;
                    iJ57.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR57.Visibility = Visibility.Visible;
                    iG57.Visibility = Visibility.Collapsed;
                    iJ57.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR57.Visibility = Visibility.Collapsed;
                    iG57.Visibility = Visibility.Visible;
                    iJ57.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR57.Visibility = Visibility.Collapsed;
                    iG57.Visibility = Visibility.Collapsed;
                    iJ57.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click58(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis58.Visibility = Visibility.Collapsed;
                    Allies58.Visibility = Visibility.Visible;
                    iUSSR58.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis58.Visibility = Visibility.Visible;
                    Allies58.Visibility = Visibility.Collapsed;
                    iUSSR58.Visibility = Visibility.Visible;
                    iG58.Visibility = Visibility.Collapsed;
                    iJ58.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR58.Visibility = Visibility.Visible;
                    iG58.Visibility = Visibility.Collapsed;
                    iJ58.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR58.Visibility = Visibility.Collapsed;
                    iG58.Visibility = Visibility.Visible;
                    iJ58.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR58.Visibility = Visibility.Collapsed;
                    iG58.Visibility = Visibility.Collapsed;
                    iJ58.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click59(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis59.Visibility = Visibility.Collapsed;
                    Allies59.Visibility = Visibility.Visible;
                    iUSSR59.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis59.Visibility = Visibility.Visible;
                    Allies59.Visibility = Visibility.Collapsed;
                    iUSSR59.Visibility = Visibility.Visible;
                    iG59.Visibility = Visibility.Collapsed;
                    iJ59.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR59.Visibility = Visibility.Visible;
                    iG59.Visibility = Visibility.Collapsed;
                    iJ59.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR59.Visibility = Visibility.Collapsed;
                    iG59.Visibility = Visibility.Visible;
                    iJ59.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR59.Visibility = Visibility.Collapsed;
                    iG59.Visibility = Visibility.Collapsed;
                    iJ59.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click60(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis60.Visibility = Visibility.Collapsed;
                    Allies60.Visibility = Visibility.Visible;
                    iUSSR60.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis60.Visibility = Visibility.Visible;
                    Allies60.Visibility = Visibility.Collapsed;
                    iUSSR60.Visibility = Visibility.Visible;
                    iG60.Visibility = Visibility.Collapsed;
                    iJ60.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR60.Visibility = Visibility.Visible;
                    iG60.Visibility = Visibility.Collapsed;
                    iJ60.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR60.Visibility = Visibility.Collapsed;
                    iG60.Visibility = Visibility.Visible;
                    iJ60.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR60.Visibility = Visibility.Collapsed;
                    iG60.Visibility = Visibility.Collapsed;
                    iJ60.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click61(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis61.Visibility = Visibility.Collapsed;
                    Allies61.Visibility = Visibility.Visible;
                    iUSSR61.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis61.Visibility = Visibility.Visible;
                    Allies61.Visibility = Visibility.Collapsed;
                    iUSSR61.Visibility = Visibility.Visible;
                    iG61.Visibility = Visibility.Collapsed;
                    iJ61.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR61.Visibility = Visibility.Visible;
                    iG61.Visibility = Visibility.Collapsed;
                    iJ61.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR61.Visibility = Visibility.Collapsed;
                    iG61.Visibility = Visibility.Visible;
                    iJ61.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR61.Visibility = Visibility.Collapsed;
                    iG61.Visibility = Visibility.Collapsed;
                    iJ61.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click62(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis62.Visibility = Visibility.Collapsed;
                    Allies62.Visibility = Visibility.Visible;
                    iUSSR62.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis62.Visibility = Visibility.Visible;
                    Allies62.Visibility = Visibility.Collapsed;
                    iUSSR62.Visibility = Visibility.Visible;
                    iG62.Visibility = Visibility.Collapsed;
                    iJ62.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR62.Visibility = Visibility.Visible;
                    iG62.Visibility = Visibility.Collapsed;
                    iJ62.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR62.Visibility = Visibility.Collapsed;
                    iG62.Visibility = Visibility.Visible;
                    iJ62.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR62.Visibility = Visibility.Collapsed;
                    iG62.Visibility = Visibility.Collapsed;
                    iJ62.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click63(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis63.Visibility = Visibility.Collapsed;
                    Allies63.Visibility = Visibility.Visible;
                    iUSSR63.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis63.Visibility = Visibility.Visible;
                    Allies63.Visibility = Visibility.Collapsed;
                    iUSSR63.Visibility = Visibility.Visible;
                    iG63.Visibility = Visibility.Collapsed;
                    iJ63.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR63.Visibility = Visibility.Visible;
                    iG63.Visibility = Visibility.Collapsed;
                    iJ63.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR63.Visibility = Visibility.Collapsed;
                    iG63.Visibility = Visibility.Visible;
                    iJ63.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR63.Visibility = Visibility.Collapsed;
                    iG63.Visibility = Visibility.Collapsed;
                    iJ63.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click64(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis64.Visibility = Visibility.Collapsed;
                    Allies64.Visibility = Visibility.Visible;
                    iUSSR64.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis64.Visibility = Visibility.Visible;
                    Allies64.Visibility = Visibility.Collapsed;
                    iUSSR64.Visibility = Visibility.Visible;
                    iG64.Visibility = Visibility.Collapsed;
                    iJ64.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR64.Visibility = Visibility.Visible;
                    iG64.Visibility = Visibility.Collapsed;
                    iJ64.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR64.Visibility = Visibility.Collapsed;
                    iG64.Visibility = Visibility.Visible;
                    iJ64.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR64.Visibility = Visibility.Collapsed;
                    iG64.Visibility = Visibility.Collapsed;
                    iJ64.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click65(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis65.Visibility = Visibility.Collapsed;
                    Allies65.Visibility = Visibility.Visible;
                    iUSSR65.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis65.Visibility = Visibility.Visible;
                    Allies65.Visibility = Visibility.Collapsed;
                    iUSSR65.Visibility = Visibility.Visible;
                    iG65.Visibility = Visibility.Collapsed;
                    iJ65.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR65.Visibility = Visibility.Visible;
                    iG65.Visibility = Visibility.Collapsed;
                    iJ65.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR65.Visibility = Visibility.Collapsed;
                    iG65.Visibility = Visibility.Visible;
                    iJ65.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR65.Visibility = Visibility.Collapsed;
                    iG65.Visibility = Visibility.Collapsed;
                    iJ65.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click66(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis66.Visibility = Visibility.Collapsed;
                    Allies66.Visibility = Visibility.Visible;
                    iUSSR66.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis66.Visibility = Visibility.Visible;
                    Allies66.Visibility = Visibility.Collapsed;
                    iUSSR66.Visibility = Visibility.Visible;
                    iG66.Visibility = Visibility.Collapsed;
                    iJ66.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR66.Visibility = Visibility.Visible;
                    iG66.Visibility = Visibility.Collapsed;
                    iJ66.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR66.Visibility = Visibility.Collapsed;
                    iG66.Visibility = Visibility.Visible;
                    iJ66.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR66.Visibility = Visibility.Collapsed;
                    iG66.Visibility = Visibility.Collapsed;
                    iJ66.Visibility = Visibility.Visible;
                }
                UpdateScreen();
            }
        }
        private void click67(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var owner = (string)radioButton.Content;
                string territory = GetTerritory((string)radioButton.GroupName);
                ChangeOwner(owner, territory); // button group name as string of int to territory name
                if (owner != "USSR")
                {
                    Axis67.Visibility = Visibility.Collapsed;
                    Allies67.Visibility = Visibility.Visible;
                    iUSSR67.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Axis67.Visibility = Visibility.Visible;
                    Allies67.Visibility = Visibility.Collapsed;
                    iUSSR67.Visibility = Visibility.Visible;
                    iG67.Visibility = Visibility.Collapsed;
                    iJ67.Visibility = Visibility.Collapsed;
                }
                if (owner == "USSR")
                {
                    iUSSR67.Visibility = Visibility.Visible;
                    iG67.Visibility = Visibility.Collapsed;
                    iJ67.Visibility = Visibility.Collapsed;

                }
                if (owner == "Germany")
                {
                    iUSSR67.Visibility = Visibility.Collapsed;
                    iG67.Visibility = Visibility.Visible;
                    iJ67.Visibility = Visibility.Collapsed;
                }
                if (owner == "Japan")
                {
                    iUSSR67.Visibility = Visibility.Collapsed;
                    iG67.Visibility = Visibility.Collapsed;
                    iJ67.Visibility = Visibility.Visible;
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
