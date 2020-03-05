using System;
using System.Collections.Generic;
using System.IO;
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

namespace StreamStage {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        int standingP1 = 0;
        int standingP2 = 0;

        public MainWindow() {
            InitializeComponent();
            init();
            string[] stage = { };
            string[] stageSTD = {
                "Friendlies",
                "Pool",
                "--------------------",
                "WB Round 1",
                "WB Round 2",
                "WB Round 3",
                "WB Round 4",
                "WB Final",
                "--------------------",
                "LB Round 1",
                "LB Round 2",
                "LB Round 3",
                "LB Round 4",
                "LB Final",
                "--------------------",
                "Grand Final"
            };
            try {
                stage = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "stage.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "stage.txt", stageSTD);
                stage = stageSTD;
            }
            
            foreach(string s in stage) {
                cbxStage.Items.Add(s);
            }



            lblStanding.Content = standingP1 + ":" + standingP2;
        }

        private void btnPS1_Click(object sender, RoutedEventArgs e) {
            Window p1 = new PlayerSelectionScreen();
            p1.Show();
        }

        private void btnPS2_Click(object sender, RoutedEventArgs e) {
            Window p2 = new PlayerSelectionScreen2();
            p2.Show();
        }

        private void btnP1plus_Click(object sender, RoutedEventArgs e) {
            standingP1++;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP1minus_Click(object sender, RoutedEventArgs e) {
            standingP1--;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP2plus_Click(object sender, RoutedEventArgs e) {
            standingP2++;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP2minus_Click(object sender, RoutedEventArgs e) {
            standingP2--;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void safeStandingData() {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt", standingP1 + "");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt", standingP2 + "");
        }

        private void cbxStage_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "stageSelected.txt", cbxStage.SelectedItem + "");
        }

        private void init() {
            try {
                standingP1 = Int32.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt"));
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt", "");
            }

            try {
                standingP2 = Int32.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt"));
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt", "");
            }

            try {
                btnPS1.Content = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player.txt", "");
            }

            try {
                btnPS2.Content = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", "");
            }



        }
    }
}
