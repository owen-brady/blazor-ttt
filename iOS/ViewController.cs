using Foundation;
using System;
using System.Linq;
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

        private GameViewModel _gameViewModel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "Y");
            _gameViewModel = new GameViewModel(player1, player2);

            // Hide tie game and winner labels by default
            TieLabel.Hidden = true;
            WinnerLabel.Hidden = true;
            
            RefreshView(); // Set initial values
            
            TurnIndicatorLabel.Text = $"{_gameViewModel.Game.ActivePlayer.Name}: please select tile";
            
            for (var i = 0; i < TileButtons.Length; i++)
            {
                var button = TileButtons.First(_ => _.Tag == i);
                var i1 = i;
                button.TouchUpInside += (object sender, EventArgs e) =>
                {
                    _gameViewModel.PlayGame(i1);
                    RefreshView();
                };
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void RefreshView()
        {
            // Show/Hide Labels
            LetsPlayLabel.Hidden = _gameViewModel.HasWinner;
            TurnIndicatorLabel.Hidden = _gameViewModel.HasWinner;
            WinnerLabel.Hidden = !_gameViewModel.HasWinner;
            TieLabel.Hidden = !_gameViewModel.IsTie;
            
            // Set Label Text
            TurnIndicatorLabel.Text = $"{_gameViewModel.Game.ActivePlayer.Name}: please select tile";
            WinnerLabel.Text = $"Winner: {_gameViewModel.Winner?.Name}";
            TieLabel.Text = "Tie Game!";
            
            // Buttons
            for (var i = 0; i < TileButtons.Length; i++)
            {
                var button = TileButtons.First(_ => _.Tag == i);
                button.SetTitle(_gameViewModel.Game.Board.Tiles[i].Player?.Token ?? "", UIControlState.Normal);
            }
        }
    }
}
