﻿using System;
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
    /// Interaction logic for PlayerSelectionScreen2.xaml
    /// </summary>
    public partial class PlayerSelectionScreen2 : Window
    {
        string[] player = { };
        List<string> playerList = new List<string>();
        public PlayerSelectionScreen2()
        {
            InitializeComponent();

            try {
                player = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt");
            } catch (FileNotFoundException e) {
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", player);
            }

            List<Button> playerButtons = new List<Button>();
            foreach (string p in player) {
                Button b = new Button();
                b.Content = p;
                b.Name = "btn" + p.Replace(" ","").Replace("[", "_").Replace("]", "_").Replace("(", "-").Replace(")", "-");
                sp.Children.Add(b);

                b.Height = 100;
                b.Width = 177;
                b.FontSize = 22;
                b.Click += btnPlayer_Click;

                playerButtons.Add(b);
                playerList.Add(p);
            }
        }
        private void btnPlayer_Click(object sender, RoutedEventArgs e) {
            ((MainWindow)Application.Current.MainWindow).btnPS2.Content = sender.ToString().Substring(32);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", sender.ToString().Substring(32));
            ((MainWindow)Application.Current.MainWindow).btnTgl2.IsChecked = false;
            this.Close();
        }

        private void btnP2add_Click(object sender, RoutedEventArgs e) {
            ((MainWindow)Application.Current.MainWindow).btnPS2.Content = tfP2gamertag.Text;
            ((MainWindow)Application.Current.MainWindow).btnTgl2.IsChecked = false;
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", tfP2gamertag.Text);

            playerList.Add(tfP2gamertag.Text);
            playerList.Sort();
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", playerList.ToArray());

            this.Close();
        }

        private void tfP2gamertag_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                btnP2add_Click(sender, e);
                /*((MainWindow)Application.Current.MainWindow).btnPS2.Content = tfP2gamertag.Text;
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "player2.txt", tfP2gamertag.Text);

                playerList.Add(tfP2gamertag.Text);
                playerList.Sort();
                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "player.txt", playerList.ToArray());

                this.Close();*/
            }
        }
    }
}
