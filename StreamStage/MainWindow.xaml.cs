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
        string player1 = "";
        string player2 = "";

        string selectedStage = "";
        int preSelectedStageIndex = -1;

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
                "WB Quarter-Final",
                "WB Semi-Final",
                "WB Final",
                "--------------------",
                "LB Round 1",
                "LB Round 2",
                "LB Round 3",
                "LB Round 4",
                "LB Quarter-Final",
                "LB Semi-Final",
                "LB Final",
                "--------------------",
                "Grand Final",
                "Grand Final Reset"
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

            for(int i = 0; i<stage.Length; i++) {
                if (stage[i].Equals(selectedStage)) {
                    cbxStage.SelectedIndex = i;
                }
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
            if (standingP1 > 9)
                standingP1 = 9;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP1minus_Click(object sender, RoutedEventArgs e) {
            standingP1--;
            if (standingP1 < 0)
                standingP1 = 0;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP2plus_Click(object sender, RoutedEventArgs e) {
            standingP2++;
            if (standingP2 > 9)
                standingP2 = 9;
            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void btnP2minus_Click(object sender, RoutedEventArgs e) {
            standingP2--;
            if (standingP2 < 0)
                standingP2 = 0;

            lblStanding.Content = standingP1 + ":" + standingP2;
            safeStandingData();
        }

        private void safeStandingData() {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt", standingP1 + "");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt", standingP2 + "");
        }

        private void cbxStage_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (!cbxStage.SelectedItem.Equals("--------------------")) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "stageSelected.txt", cbxStage.SelectedItem + "");
                preSelectedStageIndex = cbxStage.SelectedIndex;
            } else {
                cbxStage.SelectedIndex = preSelectedStageIndex;
            }
            
        }

        private void init() {
            try {
                standingP1 = Int32.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt"));
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt", "0");
            }

            try {
                standingP2 = Int32.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt"));
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt", "0");
            }

            try {
                player1 = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt");
                btnPS1.Content = player1;
                if(player1.EndsWith(" [L]")) {
                    btnTgl1.IsChecked = true;
                }
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", "");
            }

            try {
                player2 = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt");
                btnPS2.Content = player2;
                if (player2.EndsWith(" [L]")) {
                    btnTgl2.IsChecked = true;
                }
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", "");
            }

            try {
                selectedStage = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "stageSelected.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "stageSelected.txt", "");
            }

        }

        private void btnSwap_Click(object sender, RoutedEventArgs e) {
            if (!btnPS1.Content.Equals(player1)) {
                player1 = btnPS1.Content.ToString();
            }

            if (!btnPS2.Content.Equals(player1)) {
                player2 = btnPS2.Content.ToString();
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", player2 + "");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", player1 + "");
            btnPS2.Content = player1;
            btnPS1.Content = player2;

            string temp = player1;
            player1 = player2;
            player2 = temp;

            if (player1.EndsWith(" [L]")) {
                btnTgl1.IsChecked = true;
            } else {
                btnTgl1.IsChecked = false;
            }

            if (player2.EndsWith(" [L]")) {
                btnTgl2.IsChecked = true;
            } else {
                btnTgl2.IsChecked = false;
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP1.txt", standingP2 + "");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "standingP2.txt", standingP1 + "");
            int temp2 = standingP1;
            standingP1 = standingP2;
            standingP2 = temp2;
            lblStanding.Content = standingP1 + ":" + standingP2;
        }

        private void btnTgl1_Click(object sender, RoutedEventArgs e) {
            if (!btnPS1.Content.Equals(player1)) {
                player1 = btnPS1.Content.ToString();
            }

            if (!player1.EndsWith(" [L]") && btnTgl1.IsChecked==true) {
                player1 = player1 + " [L]";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", player1 + "");
            } else if(player1.EndsWith(" [L]") && btnTgl1.IsChecked == false) {
                player1 = player1.Substring(0, player1.IndexOf("[")-1);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", player1 + "");
            }
            btnPS1.Content = player1;

        }

        private void btnTgl2_Click(object sender, RoutedEventArgs e) {
            if (!btnPS2.Content.Equals(player1)) {
                player2 = btnPS2.Content.ToString();
            }

            if (!player2.EndsWith(" [L]") && btnTgl2.IsChecked == true) {
                player2 = player2 + " [L]";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", player2 + "");
            } else if (player2.EndsWith(" [L]") && btnTgl2.IsChecked == false) {
                player2 = player2.Substring(0, player2.IndexOf("[") - 1);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", player2 + "");
            }
            btnPS2.Content = player2;
        }
    }
}
