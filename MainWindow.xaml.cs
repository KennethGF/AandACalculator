using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AxisAndAlliesCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private List<GameData> _gameDataList = new List<GameData>();
        //private int _germanyTotal = 0;
        //private int _japanTotal = 0;
        //private int _usaTotal = 0;
        //private int _ukTotal = 0;
        //private int _ussrTotal = 0;

        public int GermanyTotal
        {
            get
            {
                var gList = _gameDataList.Where(s => s.OWNER == "Germany").Select(s => s.IPC).ToList().Sum();
                return 24; //gList;
            }
        }
        public int JapanTotal
        {
            get
            {
                var jList = _gameDataList.Where(s => s.OWNER == "Japan").Select(s => s.IPC).ToList().Sum();
                return 25; // jList;
            }
        }
        public int USATotal
        {
            get
            {
                var usList = _gameDataList.Where(s => s.OWNER == "USA").Select(s => s.IPC).ToList().Sum();
                return 26; // usList;
            }
        }
        public int UKTotal
        {
            get
            {
                var ukList = _gameDataList.Where(s => s.OWNER == "UK").Select(s => s.IPC).ToList().Sum();
                return 27; // ukList;
            }
        }
        public int USSRTotal
        {
            get
            {
                var rusList = _gameDataList.Where(s => s.OWNER == "USSR").Select(s => s.IPC).ToList().Sum();
                return 18; // rusList;
            }
        }
        //public string DisplayedImage
        //{
        //    get { return @"~\..\Germany.png";}
        //}
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            //this.DataContext = new SimpleViewModel();
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            LoadMapData();
            IsVisibleChanged += OnIsVisibleChanged;      
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void radioButton0_Click(object sender, RoutedEventArgs e)
        {
            // add, subtract, visible collapsed
            //Console.WriteLine(string.Format("You clicked on the {0}. button.", (sender as RadioButton).Tag));
        }
        //public bool CollapsedAxis(string nation, int num)
        //{
        //    for (int i = 0; i < 68; i++)
        //    {
        //        if (i == num && (nation == "Germany" || nation == "Japan"))
        //            return true;
        //    }
        //    return false;
        //}
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            RadioButton rb = sender as RadioButton;

            if (rb != null)
                if (IsVisible)
                {
                    //rb.Visibility = Visibility.Collapsed;               
                    int num = Int32.Parse(rb.GroupName);
                }

        }   //if (CollapsedAxis(rb.Content.ToString(), num))
            //{
            //    switch (num)
            //    {
            //        case 40:
            //            Axis40.Visibility = Visibility.Collapsed;
            //            break;

        //            // add other cases here
        //    }
        //}
        //else
        //{
        //    switch (num)
        //    {
        //        case 40:
        //            Allies40.Visibility = Visibility.Collapsed;
        //            break;
        //            // add other cases here
        //    }
        //}
        //        }
        //}
        //public static bool Alined(string nation1, string nation2)
        //{
        //    bool answer = false;
        //    switch (nation1)
        //    {
        //        case "Germany":
        //            if (nation2 == "Japan")
        //                answer = true;
        //            break;
        //        case "Japan":
        //            if (nation2 == "Germany")
        //                answer = true;
        //            break;
        //        case "USA":
        //            if (nation2 == "UK" || nation2 == "USSR")
        //                answer = true;
        //            break;
        //        case "UK":
        //            if (nation2 == "USA" || nation2 == "USSR")
        //                answer = true;
        //            break;
        //        case "USSR":
        //            if (nation2 == "UK" || nation2 == "USA")
        //                answer = true;
        //            break;
        //    }
        //    return answer;
        //}
        #region Territory Button Clicks for Menu Items
        //private void easternUSA_Click(object sender, RoutedEventArgs e)
        //{
        //    (sender as Button).ContextMenu.IsEnabled = true;
        //    (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
        //    (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        //    (sender as Button).ContextMenu.IsOpen = true;

        //}
        //private void centralUSA_Click(object sender, RoutedEventArgs e)
        //{
        //    (sender as Button).ContextMenu.IsEnabled = true;
        //    (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
        //    (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        //    (sender as Button).ContextMenu.IsOpen = true;
        //}
        //private void easternCanada_Click(object sender, RoutedEventArgs e)
        //{
        //    (sender as Button).ContextMenu.IsEnabled = true;
        //    (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
        //    (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        //    (sender as Button).ContextMenu.IsOpen = true;
        //    var ipc = gameDataList[40];

        //}
        //public void SubtractCurrentOwner(GameData theNation)
        //{
        //    //string nation = theNation.OWNER;
        //    switch (theNation.OWNER)
        //    {
        //        case "Germany":
        //            _germanyTotal -= theNation.IPC;
        //            break;
        //        case "Japan":
        //            _japanTotal -= theNation.IPC;
        //            break;
        //        case "USA":
        //            _usaTotal -= theNation.IPC;
        //            break;
        //        case "UK":
        //            _ukTotal -= theNation.IPC;
        //            break;
        //        case "USSR":
        //            _ussrTotal -= theNation.IPC;
        //            break;
        //    }
        //}
        #endregion
        #region Menu Item (nation) Clicks
        private void click0(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
            }
        }
        private void click40(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //if (openFileDialog.ShowDialog() == true)
                //{
                //    Uri fileUri = new Uri(openFileDialog.FileName);
                //    imgDynamic.Source = new BitmapImage(fileUri);
                //}

                ////Axis(rb);
                ////int num = Int32.Parse(rb.GroupName);
                //if (!gameDataList[Int32.Parse(rb.GroupName)].OCCUPIED)
                //    Axis(rb);
                //else
                //    Allies(rb);
                //string G = "Germany" + num.ToString();
                //string J = "Japan" + num.ToString();
                //string US = "USA" + num.ToString();
                //string UK = "UK" + num.ToString();
                //string R = "USSR" + num.ToString();
                //if (rb.Name == G) //(Germany40.IsChecked == true)
                //{
                //    if (Alined(gameDataList[num].ORIGINAL_OWNER, rb.Content.ToString()))
                //    {
                //        _japanTotal += gameDataList[num].IPC;
                //        SubtractCurrentOwner(gameDataList[num]);
                //        gameDataList[num].OWNER = "Japan";
                //    }
                //    else
                //    {
                //        _germanyTotal += gameDataList[num].IPC;
                //        SubtractCurrentOwner(gameDataList[num]);
                //        gameDataList[num].OWNER = "Germany";
                //    }
                //}
                //if (rb.Name == J)
                //{
                //    if (gameDataList[num].ORIGINAL_OWNER == rb.Content.ToString())
                //    {
                //        _japanTotal += gameDataList[num].IPC;
                //        SubtractCurrentOwner(gameDataList[num]);
                //        gameDataList[num].OWNER = "Japan";
                //    }
                //    else if (Alined(gameDataList[num].ORIGINAL_OWNER, rb.Content.ToString()))
                //    {
                //        _japanTotal += gameDataList[num].IPC;
                //        SubtractCurrentOwner(gameDataList[num]);
                //        gameDataList[num].OWNER = "Japan";
                //    }
                //}
                //if (USA40.IsChecked == true)
                //{
                //    _usaTotal += gameDataList[40].IPC;
                //}
                //if (UK40.IsChecked == true)
                //{
                //    _ukTotal += gameDataList[40].IPC;
                //}
                //if (USSR40.IsChecked == true)
                //{
                //    _ussrTotal += gameDataList[40].IPC;
                //}
            }
        }
        //public bool OriginalOwner(string originalOwner, string invader)
        //{
        //    return true;
        //}
        //private void addGermany(object sender, RoutedEventArgs e) // test this on eastern canada
        //{

        //    //if (repeatTerretory == (sender as GameData).TERRITORY)
        //    //    already = "no";
        //    // need to check if Germany liberated this so Japan gets the IPC
        //    //if (already != "Germany")

        //    {
        //        if ((sender as GameData).ORIGINAL_OWNER == "Japan")
        //        {
        //            _japanTotal += (sender as GameData).IPC;
        //            (sender as GameData).OWNER = "Japan";
        //            // ToDo turn on japan map marker 
        //        }
        //        else if (already != "Germany")
        //        {
        //            _germanyTotal += (sender as GameData).IPC;
        //            // ToDo turn on germany map maker
        //            (sender as GameData).OWNER = "Germany";
        //            already = "Germany";
        //        }
        //        // else do nothing??

        //        switch ((sender as GameData).ORIGINAL_OWNER)
        //        {
        //            case "USA":
        //                _usaTotal -= (sender as GameData).IPC;
        //                break;
        //            case "UK":
        //                _ukTotal -= (sender as GameData).IPC;
        //                break;
        //            case "USSR":
        //                _ussrTotal -= (sender as GameData).IPC;
        //                break;
        //        }
        //       // repeatTerretory = "Germany"; // need the attacker
        //    }
        //}
        #endregion
        // ToDO: need a change of ownership icon on the map

        public void LoadMapData()
        {
            //################################
            ////populuate gamedate objects for income producing territories and load list of gamedata objects
            /// load insignias
            /// 

            GameData gameData0 = new GameData("Germany", 10, "Germany", "Germany", false);
            USA0.Visibility = Visibility.Visible; UK0.Visibility = Visibility.Visible; USSR0.Visibility = Visibility.Visible;

            GameData gameData1 = new GameData("France", 6, "Germany", "Germany", false);
            // USA1.Visibility = Visibility.Visible; UK1.Visibility = Visibility.Visible; USSR1.Visibility = Visibility.Visible;

            GameData gameData2 = new GameData("NorthWesternEurope", 2, "Germany", "Germany", false);
            //	USA2.Visibility = Visibility.Visible; UK2.Visibility = Visibility.Visible; USSR2.Visibility = Visibility.Visible;

            GameData gameData3 = new GameData("Norway", 2, "Germany", "Germany", false);
            //	USA3.Visibility = Visibility.Visible; UK3.Visibility = Visibility.Visible; USSR3.Visibility = Visibility.Visible;

            GameData gameData4 = new GameData("Finland", 1, "Germany", "Germany", false);
            //	USA4.Visibility = Visibility.Visible;	UK4.Visibility = Visibility.Visible;	 USSR4.Visibility = Visibility.Visible;

            GameData gameData5 = new GameData("BalticStates", 2, "Germany", "Germany", false);
            //	USA5.Visibility = Visibility.Visible;	UK5.Visibility = Visibility.Visible;	 USSR5.Visibility = Visibility.Visible;

            GameData gameData6 = new GameData("Poland", 2, "Germany", "Germany", false);
            //	USA6.Visibility = Visibility.Visible;	UK6.Visibility = Visibility.Visible;	 USSR6.Visibility = Visibility.Visible;

            GameData gameData7 = new GameData("BulgariaRomania", 2, "Germany", "Germany", false);
            //	USA7.Visibility = Visibility.Visible;	UK7.Visibility = Visibility.Visible;	 USSR7.Visibility = Visibility.Visible;

            GameData gameData8 = new GameData("Italy", 3, "Germany", "Germany", false);
            //	USA8.Visibility = Visibility.Visible;	UK8.Visibility = Visibility.Visible;	 USSR8.Visibility = Visibility.Visible;

            GameData gameData9 = new GameData("SouthernEurope", 2, "Germany", "Germany", false);
            //	USA9.Visibility = Visibility.Visible;	UK9.Visibility = Visibility.Visible;	 USSR9.Visibility = Visibility.Visible;

            GameData gameData10 = new GameData("Ukraine", 2, "Germany", "Germany", false);
            //	USA10.Visibility = Visibility.Visible;	UK10.Visibility = Visibility.Visible;	 USSR10.Visibility = Visibility.Visible;

            GameData gameData11 = new GameData("Belorussia", 2, "Germany", "Germany", false);
            //	USA11.Visibility = Visibility.Visible;	UK11.Visibility = Visibility.Visible;	 USSR11.Visibility = Visibility.Visible;

            GameData gameData12 = new GameData("WestRussia", 2, "Germany", "Germany", false);
            //	USA12.Visibility = Visibility.Visible;	UK12.Visibility = Visibility.Visible;	 USSR12.Visibility = Visibility.Visible;

            GameData gameData13 = new GameData("Morocco", 1, "Germany", "Germany", false);
            //	USA13.Visibility = Visibility.Visible;	UK13.Visibility = Visibility.Visible;	 USSR13.Visibility = Visibility.Visible;

            GameData gameData14 = new GameData("Algeria", 1, "Germany", "Germany", false);
            //	USA14.Visibility = Visibility.Visible;	UK14.Visibility = Visibility.Visible;	 USSR14.Visibility = Visibility.Visible;

            GameData gameData15 = new GameData("Libya", 1, "Germany", "Germany", false);
            //	USA15.Visibility = Visibility.Visible;	UK15.Visibility = Visibility.Visible;	 USSR15.Visibility = Visibility.Visible;

            GameData gameData16 = new GameData("Japan", 8, "Japan", "Japan", false);
            //	USA16.Visibility = Visibility.Visible;	UK16.Visibility = Visibility.Visible;	 USSR16.Visibility = Visibility.Visible;

            GameData gameData17 = new GameData("Manchuria", 3, "Japan", "Japan", false);
            //	USA17.Visibility = Visibility.Visible;	UK17.Visibility = Visibility.Visible;	 USSR17.Visibility = Visibility.Visible;

            GameData gameData18 = new GameData("Kiangsu", 2, "Japan", "Japan", false);
            //	USA18.Visibility = Visibility.Visible;	UK18.Visibility = Visibility.Visible;	 USSR18.Visibility = Visibility.Visible;

            GameData gameData19 = new GameData("Kwangtung", 2, "Japan", "Japan", false);
            //	USA19.Visibility = Visibility.Visible;	UK19.Visibility = Visibility.Visible;	 USSR19.Visibility = Visibility.Visible;

            GameData gameData20 = new GameData("FrenchIndoChina", 2, "Japan", "Japan", false);
            //	USA20.Visibility = Visibility.Visible;	UK20.Visibility = Visibility.Visible;	 USSR20.Visibility = Visibility.Visible;

            GameData gameData21 = new GameData("Malaya", 1, "Japan", "Japan", false);
            //	USA21.Visibility = Visibility.Visible;	UK21.Visibility = Visibility.Visible;	 USSR21.Visibility = Visibility.Visible;

            GameData gameData22 = new GameData("EastIndies", 4, "Japan", "Japan", false);
            //	USA22.Visibility = Visibility.Visible;	UK22.Visibility = Visibility.Visible;	 USSR22.Visibility = Visibility.Visible;

            GameData gameData23 = new GameData("Borneo", 4, "Japan", "Japan", false);
            //	USA23.Visibility = Visibility.Visible;	UK23.Visibility = Visibility.Visible;	 USSR23.Visibility = Visibility.Visible;

            GameData gameData24 = new GameData("NewGuinea", 1, "Japan", "Japan", false);
            //	USA24.Visibility = Visibility.Visible;	UK24.Visibility = Visibility.Visible;	 USSR24.Visibility = Visibility.Visible;

            GameData gameData25 = new GameData("PhilippineIslands", 3, "Japan", "Japan", false);
            //	USA25.Visibility = Visibility.Visible;	UK25.Visibility = Visibility.Visible;	 USSR25.Visibility = Visibility.Visible;



            GameData gameData26 = new GameData("EasternUSA", 12, "USA", "USA", false);
            //	Germany26.Visibility = Visibility.Visible;	Japan26.Visibility = Visibility.Visible;

            GameData gameData27 = new GameData("CentralUSA", 6, "USA", "USA", false);
            //	Germany27.Visibility = Visibility.Visible;	Japan27.Visibility = Visibility.Visible;

            GameData gameData28 = new GameData("Alaska", 2, "USA", "USA", false);
            //	Germany28.Visibility = Visibility.Visible;	Japan28.Visibility = Visibility.Visible;

            GameData gameData29 = new GameData("WesternUSA", 10, "USA", "USA", false);
            //	Germany29.Visibility = Visibility.Visible;	Japan29.Visibility = Visibility.Visible;

            GameData gameData30 = new GameData("Hawaiian", 1, "USA", "USA", false);
            //	Germany30.Visibility = Visibility.Visible;	Japan30.Visibility = Visibility.Visible;

            GameData gameData31 = new GameData("Sinkiang", 1, "USA", "USA", false);
            //	Germany31.Visibility = Visibility.Visible;	Japan31.Visibility = Visibility.Visible;

            GameData gameData32 = new GameData("Yunnan", 1, "USA", "USA", false);
            //	Germany32.Visibility = Visibility.Visible;	Japan32.Visibility = Visibility.Visible;

            GameData gameData33 = new GameData("Szechwan", 1, "USA", "USA", false);
            //	Germany33.Visibility = Visibility.Visible;	Japan33.Visibility = Visibility.Visible;

            GameData gameData34 = new GameData("Anhwei", 1, "USA", "USA", false);
            //	Germany34.Visibility = Visibility.Visible;	Japan34.Visibility = Visibility.Visible;

            GameData gameData35 = new GameData("Mexico", 2, "USA", "USA", false);
            //	Germany35.Visibility = Visibility.Visible;	Japan35.Visibility = Visibility.Visible;

            GameData gameData36 = new GameData("CentralAmerica", 1, "USA", "USA", false);
            //	Germany36.Visibility = Visibility.Visible;	Japan36.Visibility = Visibility.Visible;

            GameData gameData37 = new GameData("WestIndies", 1, "USA", "USA", false);
            //	Germany37.Visibility = Visibility.Visible;	Japan37.Visibility = Visibility.Visible;

            GameData gameData38 = new GameData("Brazil", 3, "USA", "USA", false);
            //	Germany38.Visibility = Visibility.Visible;	Japan38.Visibility = Visibility.Visible;

            GameData gameData39 = new GameData("UnitedKingdom", 8, "UK", "UK", false);
            //	Germany39.Visibility = Visibility.Visible;	Japan39.Visibility = Visibility.Visible;

            GameData gameData40 = new GameData("EasternCanada", 3, "UK", "UK", false);
            Germany40.Visibility = Visibility.Visible;
            Japan40.Visibility = Visibility.Visible;

            GameData gameData41 = new GameData("Egypt", 2, "UK", "UK", false);
            //	Germany41.Visibility = Visibility.Visible;	Japan41.Visibility = Visibility.Visible;

            GameData gameData42 = new GameData("SouthAfrica", 2, "UK", "UK", false);
            //	Germany42.Visibility = Visibility.Visible;	Japan42.Visibility = Visibility.Visible;

            GameData gameData43 = new GameData("Jordan", 1, "UK", "UK", false);
            //	Germany43.Visibility = Visibility.Visible;	Japan43.Visibility = Visibility.Visible;

            GameData gameData44 = new GameData("Persia", 1, "UK", "UK", false);
            //	Germany44.Visibility = Visibility.Visible;	Japan44.Visibility = Visibility.Visible;

            GameData gameData45 = new GameData("India", 3, "UK", "UK", false);
            //	Germany45.Visibility = Visibility.Visible;	Japan45.Visibility = Visibility.Visible;

            GameData gameData46 = new GameData("Burma", 1, "UK", "UK", false);
            //	Germany46.Visibility = Visibility.Visible;	Japan46.Visibility = Visibility.Visible;

            GameData gameData47 = new GameData("EasternAustralia", 1, "UK", "UK", false);
            //	Germany47.Visibility = Visibility.Visible;	Japan47.Visibility = Visibility.Visible;

            GameData gameData48 = new GameData("WesternAustralia", 1, "UK", "UK", false);
            //	Germany48.Visibility = Visibility.Visible;	Japan48.Visibility = Visibility.Visible;

            GameData gameData49 = new GameData("NewZealand", 1, "UK", "UK", false);
            //	Germany49.Visibility = Visibility.Visible;	Japan49.Visibility = Visibility.Visible;

            GameData gameData50 = new GameData("NewGuinea", 1, "UK", "UK", false);
            //	Germany50.Visibility = Visibility.Visible;	Japan50.Visibility = Visibility.Visible;

            GameData gameData51 = new GameData("FrenchWestAfrica", 1, "UK", "UK", false);
            //	Germany51.Visibility = Visibility.Visible;	Japan51.Visibility = Visibility.Visible;

            GameData gameData52 = new GameData("FrechEquatorialAfrica", 1, "UK", "UK", false);
            //	Germany52.Visibility = Visibility.Visible;	Japan52.Visibility = Visibility.Visible;

            GameData gameData53 = new GameData("ItalianEastAfrica", 1, "UK", "UK", false);
            //	Germany53.Visibility = Visibility.Visible;	Japan53.Visibility = Visibility.Visible;

            GameData gameData54 = new GameData("BelgianCongo", 1, "UK", "UK", false);
            //	Germany54.Visibility = Visibility.Visible;	Japan54.Visibility = Visibility.Visible;

            GameData gameData55 = new GameData("Rhodesia", 1, "UK", "UK", false);
            //	Germany55.Visibility = Visibility.Visible;	Japan55.Visibility = Visibility.Visible;

            GameData gameData56 = new GameData("FrechMadagascar", 1, "UK", "UK", false);
            //	Germany56.Visibility = Visibility.Visible;	Japan56.Visibility = Visibility.Visible;

            GameData gameData57 = new GameData("Russia", 8, "USSR", "USSR", false);
            //	Germany57.Visibility = Visibility.Visible;	Japan57.Visibility = Visibility.Visible;

            GameData gameData58 = new GameData("Karelia", 2, "USSR", "USSR", false);
            //	Germany58.Visibility = Visibility.Visible;	Japan58.Visibility = Visibility.Visible;

            GameData gameData59 = new GameData("Archangel", 1, "USSR", "USSR", false);
            //	Germany59.Visibility = Visibility.Visible;	Japan59	Visibility = Visibility.Visible;

            GameData gameData60 = new GameData("Novosibirsk", 1, "USSR", "USSR", false);
            //	Germany60.Visibility = Visibility.Visible;	Japan60.Visibility = Visibility.Visible;

            GameData gameData61 = new GameData("Caucasus", 4, "USSR", "USSR", false);
            //	Germany61.Visibility = Visibility.Visible;	Japan61.Visibility = Visibility.Visible;

            GameData gameData62 = new GameData("Kazakh", 2, "USSR", "USSR", false);
            //	Germany62.Visibility = Visibility.Visible;	Japan62.Visibility = Visibility.Visible;

            GameData gameData63 = new GameData("Vologda", 2, "USSR", "USSR", false);
            //	Germany63.Visibility = Visibility.Visible;	Japan63.Visibility = Visibility.Visible;

            GameData gameData64 = new GameData("EvenkiNationalOkrug", 1, "USSR", "USSR", false);
            //	Germany64.Visibility = Visibility.Visible;	Japan64.Visibility = Visibility.Visible;

            GameData gameData65 = new GameData("Yakut", 1, "USSR", "USSR", false);
            //	Germany65.Visibility = Visibility.Visible;	Japan65.Visibility = Visibility.Visible;

            GameData gameData66 = new GameData("Buryatia", 1, "USSR", "USSR", false);
            //	Germany66.Visibility = Visibility.Visible;	Japan66.Visibility = Visibility.Visible;

            GameData gameData67 = new GameData("SovietFarEast", 1, "USSR", "USSR", false);
            //	Germany67.Visibility = Visibility.Visible;	Japan67.Visibility = Visibility.Visible;


            _gameDataList.Add(gameData0);
            _gameDataList.Add(gameData1);
            _gameDataList.Add(gameData2);
            _gameDataList.Add(gameData3);
            _gameDataList.Add(gameData4);
            _gameDataList.Add(gameData5);
            _gameDataList.Add(gameData6);
            _gameDataList.Add(gameData7);
            _gameDataList.Add(gameData8);
            _gameDataList.Add(gameData9);
            _gameDataList.Add(gameData10);
            _gameDataList.Add(gameData11);
            _gameDataList.Add(gameData12);
            _gameDataList.Add(gameData13);
            _gameDataList.Add(gameData14);
            _gameDataList.Add(gameData15);
            _gameDataList.Add(gameData16);
            _gameDataList.Add(gameData17);
            _gameDataList.Add(gameData18);
            _gameDataList.Add(gameData19);
            _gameDataList.Add(gameData20);
            _gameDataList.Add(gameData21);
            _gameDataList.Add(gameData22);
            _gameDataList.Add(gameData23);
            _gameDataList.Add(gameData24);
            _gameDataList.Add(gameData25);
            _gameDataList.Add(gameData26);
            _gameDataList.Add(gameData27);
            _gameDataList.Add(gameData28);
            _gameDataList.Add(gameData29);
            _gameDataList.Add(gameData30);
            _gameDataList.Add(gameData31);
            _gameDataList.Add(gameData32);
            _gameDataList.Add(gameData33);
            _gameDataList.Add(gameData34);
            _gameDataList.Add(gameData35);
            _gameDataList.Add(gameData36);
            _gameDataList.Add(gameData37);
            _gameDataList.Add(gameData38);
            _gameDataList.Add(gameData39);
            _gameDataList.Add(gameData40);
            _gameDataList.Add(gameData41);
            _gameDataList.Add(gameData42);
            _gameDataList.Add(gameData43);
            _gameDataList.Add(gameData44);
            _gameDataList.Add(gameData45);
            _gameDataList.Add(gameData46);
            _gameDataList.Add(gameData47);
            _gameDataList.Add(gameData48);
            _gameDataList.Add(gameData49);
            _gameDataList.Add(gameData50);
            _gameDataList.Add(gameData51);
            _gameDataList.Add(gameData52);
            _gameDataList.Add(gameData53);
            _gameDataList.Add(gameData54);
            _gameDataList.Add(gameData55);
            _gameDataList.Add(gameData56);
            _gameDataList.Add(gameData57);
            _gameDataList.Add(gameData58);
            _gameDataList.Add(gameData59);
            _gameDataList.Add(gameData60);
            _gameDataList.Add(gameData61);
            _gameDataList.Add(gameData62);
            _gameDataList.Add(gameData63);
            _gameDataList.Add(gameData64);
            _gameDataList.Add(gameData65);
            _gameDataList.Add(gameData66);
            _gameDataList.Add(gameData67);
        }
        //public void GetTotals()
        //{ 
        //    foreach (var gameData in _gameDataList)
        //    {
        //        switch (gameData.OWNER)
        //        {
        //            case "Germany":
        //                _germanyTotal += gameData.IPC;
        //                break;
        //        }
        //        switch (gameData.OWNER)
        //        {
        //            case "Japan":
        //                _japanTotal += gameData.IPC;
        //                break;
        //        }
        //        switch (gameData.OWNER)
        //        {
        //            case "USA":
        //                _ukTotal += gameData.IPC;
        //                break;
        //        }
        //        switch (gameData.OWNER)
        //        {
        //            case "UK":
        //                _ukTotal += gameData.IPC;
        //                break;
        //        }
        //        switch (gameData.OWNER)
        //        {
        //            case "USSR":
        //                _ussrTotal += gameData.IPC;
        //                break;
        //        }

        //    }
        //}
        //////  Element of Fam
        //public void Axis(RadioButton rb)
        //{
        //    int num = Int32.Parse(rb.GroupName);
        //    string G = "Germany" + num.ToString();
        //    string J = "Japan" + num.ToString();
        //    string US = "USA" + num.ToString();
        //    string UK = "UK" + num.ToString();
        //    string R = "USSR" + num.ToString();
        //    if (rb.Name == G) //(Germany40.IsChecked == true)
        //    {
        //        if (Alined(gameDataList[num].ORIGINAL_OWNER, rb.Content.ToString()))
        //        {
        //            _japanTotal += gameDataList[num].IPC;
        //            SubtractCurrentOwner(gameDataList[num]);
        //            gameDataList[num].OWNER = "Japan";
        //            gameDataList[num].OCCUPIED = false;
        //        }
        //        else
        //        {
        //            _germanyTotal += gameDataList[num].IPC;
        //            SubtractCurrentOwner(gameDataList[num]);
        //            gameDataList[num].OWNER = "Germany";
        //            gameDataList[num].OCCUPIED = true;
        //        }
        //    }
        //    if (rb.Name == J)
        //    {
        //        if (gameDataList[num].ORIGINAL_OWNER == rb.Content.ToString())
        //        {
        //            _japanTotal += gameDataList[num].IPC;
        //            SubtractCurrentOwner(gameDataList[num]);
        //            gameDataList[num].OWNER = "Japan";
        //            gameDataList[num].OCCUPIED = false;
        //        }
        //        else if (Alined(gameDataList[num].ORIGINAL_OWNER, rb.Content.ToString()))
        //        {
        //            _japanTotal += gameDataList[num].IPC;
        //            SubtractCurrentOwner(gameDataList[num]);
        //            gameDataList[num].OWNER = "Germany";
        //            gameDataList[num].OCCUPIED = true;
        //        }
        //    }
        //}
        //public void Allies(RadioButton rb)
        //{
        //    if (USA40.IsChecked == true)
        //    {
        //        _usaTotal += _gameDataList[40].IPC;
        //    }
        //    if (UK40.IsChecked == true)
        //    {
        //        _ukTotal += _gameDataList[40].IPC;
        //    }
        //    if (USSR40.IsChecked == true)
        //    {
        //        _ussrTotal += _gameDataList[40].IPC;
        //    }
        //}
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

        private void ImageBrush_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
    //public class SimpleViewModel
    //{
    //    public string Location { get; set; }

    //    public SimpleViewModel()
    //    {
    //        Location = System.AppDomain.CurrentDomain.BaseDirectory + @"Images/German.png";
    //    }
    //}
}
