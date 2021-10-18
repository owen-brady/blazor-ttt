using Foundation;
using System;
using TicTacToe.Models;
using TicTacToe.ViewModels;
using UIKit;

namespace iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            var gameViewModel = new GameViewModel(player1, player2);

            // Hide tie game and winner labels by default
            TieLabel.Hidden = true;
            WinnerLabel.Hidden = true;
            
            TurnIndicatorLabel.Text = $"${gameViewModel.Player1.Name}: please select tile";
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
