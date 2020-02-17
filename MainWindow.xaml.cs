using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        private List<GameData> gameDataList = new List<GameData>();
        private int _germanyTotal = 0;
        private int _japanTotal = 0;
        private int _usaTotal = 0;
        private int _ukTotal = 0;
        private int _ussrTotal = 0;

        //public string DisplayedImage
        //{
        //    get { return @"~\..\Germany.png";}
        //}
        public MainWindow()
        {
            InitializeComponent();

            //this.DataContext = new SimpleViewModel();
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            LoadMapData();
            IsVisibleChanged += OnIsVisibleChanged;
      
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


            //BitmapImage insigniaGermany = new BitmapImage();
            //var uriGermany = new Uri("Germany.png");
            //insigniaGermany.BeginInit();
            //insigniaGermany.UriSource = uriGermany;
            //insigniaGermany.EndInit();

            //Image German = new Image();
            //German.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("Germany.png");
            GameData gameData0 = new GameData("Germany", 10, "Germany", "Germany", false);
            USA0.Visibility = Visibility.Visible;
            UK0.Visibility = Visibility.Visible;
            USSR0.Visibility = Visibility.Visible;
            GameData gameData1 = new GameData("France", 6, "Germany", "Germany", false);
            GameData gameData2 = new GameData("NorthWesternEurope", 2, "Germany", "Germany", false);
            GameData gameData3 = new GameData("Norway", 2, "Germany", "Germany", false);
            GameData gameData4 = new GameData("Finland", 1, "Germany", "Germany", false);
            GameData gameData5 = new GameData("BalticStates", 2, "Germany", "Germany", false);
            GameData gameData6 = new GameData("Poland", 2, "Germany", "Germany", false);
            GameData gameData7 = new GameData("BulgariaRomania", 2, "Germany", "Germany", false);
            GameData gameData8 = new GameData("Italy", 3, "Germany", "Germany", false);
            GameData gameData9 = new GameData("SouthernEurope", 2, "Germany", "Germany", false);
            GameData gameData10 = new GameData("Ukraine", 2, "Germany", "Germany", false);
            GameData gameData11 = new GameData("Belorussia", 2, "Germany", "Germany", false);
            GameData gameData12 = new GameData("WestRussia", 2, "Germany", "Germany", false);
            GameData gameData13 = new GameData("Morocco", 1, "Germany", "Germany", false);
            GameData gameData14 = new GameData("Algeria", 1, "Germany", "Germany", false);
            GameData gameData15 = new GameData("Libya", 1, "Germany", "Germany", false);
            GameData gameData16 = new GameData("Japan", 8, "Japan", "Japan", false);
            GameData gameData17 = new GameData("Manchuria", 3, "Japan", "Japan", false);
            GameData gameData18 = new GameData("Kiangsu", 2, "Japan", "Japan", false);
            GameData gameData19 = new GameData("Kwangtung", 2, "Japan", "Japan", false);
            GameData gameData20 = new GameData("FrenchIndoChina", 2, "Japan", "Japan", false);
            GameData gameData21 = new GameData("Malaya", 1, "Japan", "Japan", false);
            GameData gameData22 = new GameData("EastIndies", 4, "Japan", "Japan", false);
            GameData gameData23 = new GameData("Borneo", 4, "Japan", "Japan", false);
            GameData gameData24 = new GameData("NewGuinea", 1, "Japan", "Japan", false);
            GameData gameData25 = new GameData("PhilippineIslands", 3, "Japan", "Japan", false);
            GameData gameData26 = new GameData("EasternUSA", 12, "USA", "USA", false);
            GameData gameData27 = new GameData("CentralUSA", 6, "USA", "USA", false);
            GameData gameData28 = new GameData("Alaska", 2, "USA", "USA", false);
            GameData gameData29 = new GameData("WesternUSA", 10, "USA", "USA", false);
            GameData gameData30 = new GameData("Hawaiian", 1, "USA", "USA", false);
            GameData gameData31 = new GameData("Sinkiang", 1, "USA", "USA", false);
            GameData gameData32 = new GameData("Yunnan", 1, "USA", "USA", false);
            GameData gameData33 = new GameData("Szechwan", 1, "USA", "USA", false);
            GameData gameData34 = new GameData("Anhwei", 1, "USA", "USA", false);
            GameData gameData35 = new GameData("Mexico", 2, "USA", "USA", false);
            GameData gameData36 = new GameData("CentralAmerica", 1, "USA", "USA", false);
            GameData gameData37 = new GameData("WestIndies", 1, "USA", "USA", false);
            GameData gameData38 = new GameData("Brazil", 3, "USA", "USA", false);
            GameData gameData39 = new GameData("UnitedKingdom", 8, "UK", "UK", false);
            GameData gameData40 = new GameData("EasternCanada", 3, "UK", "UK", false);
            Germany40.Visibility = Visibility.Visible;
            Japan40.Visibility = Visibility.Visible;
            GameData gameData41 = new GameData("Egypt", 2, "UK", "UK", false);
            GameData gameData42 = new GameData("SouthAfrica", 2, "UK", "UK", false);
            GameData gameData43 = new GameData("Jordan", 1, "UK", "UK", false);
            GameData gameData44 = new GameData("Persia", 1, "UK", "UK", false);
            GameData gameData45 = new GameData("India", 3, "UK", "UK", false);
            GameData gameData46 = new GameData("Burma", 1, "UK", "UK", false);
            GameData gameData47 = new GameData("EasternAustralia", 1, "UK", "UK", false);
            GameData gameData48 = new GameData("WesternAustralia", 1, "UK", "UK", false);
            GameData gameData49 = new GameData("NewZealand", 1, "UK", "UK", false);
            GameData gameData50 = new GameData("NewGuinea", 1, "UK", "UK", false);
            GameData gameData51 = new GameData("FrenchWestAfrica", 1, "UK", "UK", false);
            GameData gameData52 = new GameData("FrechEquatorialAfrica", 1, "UK", "UK", false);
            GameData gameData53 = new GameData("ItalianEastAfrica", 1, "UK", "UK", false);
            GameData gameData54 = new GameData("BelgianCongo", 1, "UK", "UK", false);
            GameData gameData55 = new GameData("Rhodesia", 1, "UK", "UK", false);
            GameData gameData56 = new GameData("FrechMadagascar", 1, "UK", "UK", false);
            GameData gameData57 = new GameData("Russia", 8, "USSR", "USSR", false);
            GameData gameData58 = new GameData("Karelia", 2, "USSR", "USSR", false);
            GameData gameData59 = new GameData("Archangel", 1, "USSR", "USSR", false);
            GameData gameData60 = new GameData("Novosibirsk", 1, "USSR", "USSR", false);
            GameData gameData61 = new GameData("Caucasus", 4, "USSR", "USSR", false);
            GameData gameData62 = new GameData("Kazakh", 2, "USSR", "USSR", false);
            GameData gameData63 = new GameData("Vologda", 2, "USSR", "USSR", false);
            GameData gameData64 = new GameData("EvenkiNationalOkrug", 1, "USSR", "USSR", false);
            GameData gameData65 = new GameData("Yakut", 1, "USSR", "USSR", false);
            GameData gameData66 = new GameData("Buryatia", 1, "USSR", "USSR", false);
            GameData gameData67 = new GameData("SovietFarEast", 1, "USSR", "USSR", false);


            foreach (var gameData in gameDataList)
            {
                switch (gameData.OWNER)
                {
                    case "Germany":
                        _germanyTotal += gameData.IPC;
                        break;
                }
                switch (gameData.OWNER)
                {
                    case "Japan":
                        _japanTotal += gameData.IPC;
                        break;
                }
                switch (gameData.OWNER)
                {
                    case "USA":
                        _ukTotal += gameData.IPC;
                        break;
                }
                switch (gameData.OWNER)
                {
                    case "UK":
                        _ukTotal += gameData.IPC;
                        break;
                }
                switch (gameData.OWNER)
                {
                    case "USSR":
                        _ussrTotal += gameData.IPC;
                        break;
                }

            }
        }
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
        public void Allies(RadioButton rb)
        {
            if (USA40.IsChecked == true)
            {
                _usaTotal += gameDataList[40].IPC;
            }
            if (UK40.IsChecked == true)
            {
                _ukTotal += gameDataList[40].IPC;
            }
            if (USSR40.IsChecked == true)
            {
                _ussrTotal += gameDataList[40].IPC;
            }
        }
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
