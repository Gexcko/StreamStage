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
using System.Windows.Shapes;

namespace StreamStage {
    /// <summary>
    /// Interaction logic for PlayerSelectionScreen.xaml
    /// </summary>
    public partial class PlayerSelectionScreen : Window {

        string[] player = { };
        List<string> playerList = new List<string>();
        public PlayerSelectionScreen() {
            InitializeComponent();
            string[] player = { };
            try {
                player = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", player);
            }

            List<Button> playerButtons = new List<Button>();
            foreach (string p in player) {
                Button b = new Button();
                b.Content = p;
                b.Name = "btn" + p.Replace(" ","");
                sp.Children.Add(b);
                
                b.Height = 100;
                b.Width = 177;
                b.FontSize = 22;
                b.Click += btnPlayer_Click;

                playerButtons.Add(b);
                playerList.Add(p);
            }

        }

        private void sp_Loaded(object sender, RoutedEventArgs e) {

        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e) {
            ((MainWindow)Application.Current.MainWindow).btnPS1.Content = sender.ToString().Substring(32);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", sender.ToString().Substring(32));
            this.Close();
        }

        private void btnP1add_Click(object sender, RoutedEventArgs e) {
            ((MainWindow)Application.Current.MainWindow).btnPS1.Content = tfP1gamertag.Text;
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", tfP1gamertag.Text);

            playerList.Add(tfP1gamertag.Text);
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", playerList.ToArray());

            this.Close();
        }

        private void tfP1gamertag_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ((MainWindow)Application.Current.MainWindow).btnPS2.Content = tfP1gamertag.Text;
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player1.txt", tfP1gamertag.Text);

                playerList.Add(tfP1gamertag.Text);
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", playerList.ToArray());

                this.Close();
            }
        }
    }
}
