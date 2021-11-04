using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }
        
        private void PlayHuman_Clicked(object sender, EventArgs e)
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            var gameViewModel = new GameViewModel(player1, player2);

            Navigation.PushAsync(new BoardPage(gameViewModel));
            // Shell.Current.GoToAsync(new BoardPage(gameViewModel));
        }

        private void PlayRobot_Clicked(object sender, EventArgs e)
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Robot, "Player 2", "O");
            var gameViewModel = new GameViewModel(player1, player2);

            Navigation.PushAsync(new BoardPage(gameViewModel));
        }
    }
}